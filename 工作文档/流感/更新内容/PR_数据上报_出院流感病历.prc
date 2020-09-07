CREATE OR REPLACE PROCEDURE PR_数据上报_出院流感病历(STR_参数          IN VARCHAR2,
                                           CUR_导出_列表信息 OUT SYS_REFCURSOR) IS
  STR_SQL VARCHAR2(1000);

  STR_机构编码     VARCHAR2(50) := FU_通用_截取字符串值(STR_参数, '|', 1);
  DAT_出院时间起始 DATE := TO_DATE(FU_通用_截取字符串值(STR_参数, '|', 2),
                             'yyyy-MM-dd hh24:mi:ss');
  DAT_出院时间截止 DATE := TO_DATE(FU_通用_截取字符串值(STR_参数, '|', 3),
                             'yyyy-MM-dd hh24:mi:ss');
  STR_过滤数据     VARCHAR2(50) := FU_通用_截取字符串值(STR_参数, '|', 4);

  --获取诊断记录CURSOR
  CURSOR CUR_诊断记录 IS
    SELECT *
      FROM (SELECT ROW_NUMBER() OVER(PARTITION BY T.住院病历号, T.诊断类型 ORDER BY T.诊断类型, T.是否主诊断 DESC) RN,
                   T.住院病历号,
                   (CASE
                     WHEN T.ICD码 = T.疾病编码 THEN
                      (SELECT B.ICD码
                         FROM 基础项目_疾病字典 B
                        WHERE B.疾病编码 = T.疾病编码)
                     ELSE
                      T.ICD码
                   END) ICD码,
                   T.疾病名称,
                   T.入院病情,
                   T.出院情况,
                   T.病理号,
                   T.入院时情况,
                   T.入院后确诊日期,
                   T.诊断类型
              FROM 住院管理_在院病人诊断 T,
                   住院管理_出院病人信息 TT,
                   住院管理_病案首页     TTT
             WHERE T.住院病历号 = TT.住院病历号
               AND T.住院病历号 = TTT.住院病历号
               AND T.机构编码 = STR_机构编码
               AND TTT.治疗类别 = '3' --西医
               AND TTT.归档人编码 IS NOT NULL
               AND TT.出院时间 BETWEEN DAT_出院时间起始 AND DAT_出院时间截止
               AND T.诊断分类 = '1'
               AND T.诊断类型 IN ('入院诊断',
                              '出院诊断',
                              '其他诊断',
                              '损伤和中毒外部原因',
                              '病理诊断')
               AND T.创建时间 >= TT.入院时间
               AND (TT.住院病历号 LIKE '%' || STR_过滤数据 || '%' OR
                   TT.病案号 LIKE '%' || STR_过滤数据 || '%' OR
                   TT.病人姓名 LIKE '%' || STR_过滤数据 || '%')) G
     WHERE (G.诊断类型 = '其他诊断' AND G.RN <= 10)
        OR (G.诊断类型 in ('损伤和中毒外部原因', '病理诊断') AND G.RN = 3)
        OR (G.诊断类型 in ('入院诊断', '出院诊断') AND G.RN = 1);
  ROW_诊断记录 CUR_诊断记录%ROWTYPE;

  --获取手术记录CURSOR
  CURSOR CUR_手术记录 IS
    SELECT *
      FROM (SELECT ROW_NUMBER() OVER(PARTITION BY T.住院病历号 ORDER BY T.流水码 DESC) RN,
                   T.住院病历号,
                   T.国标码,
                   TO_CHAR(T.手术操作日期, 'yyyy-MM-dd hh24:mi:ss') 手术操作日期,
                   T.手术级别编码,
                   T.手术操作名称,
                   --手术操作部分
                   --手术持续时间
                   (SELECT A.人员姓名
                      FROM 基础项目_人员资料 A
                     WHERE A.机构编码 = T.机构编码
                       AND A.删除标志 = '0'
                       AND A.人员编码 = T.术者) AS 术者,
                   (SELECT A.人员姓名
                      FROM 基础项目_人员资料 A
                     WHERE A.机构编码 = T.机构编码
                       AND A.删除标志 = '0'
                       AND A.人员编码 = T.I助) AS I助,
                   (SELECT A.人员姓名
                      FROM 基础项目_人员资料 A
                     WHERE A.机构编码 = T.机构编码
                       AND A.删除标志 = '0'
                       AND A.人员编码 = T.II助) AS II助,
                   T.麻醉方式编码,
                   T.麻醉分级编码,
                   T.切口愈合等级编码,
                   (SELECT A.人员姓名
                      FROM 基础项目_人员资料 A
                     WHERE A.机构编码 = T.机构编码
                       AND A.删除标志 = '0'
                       AND A.人员编码 = T.麻醉医师) AS 麻醉医师
              FROM 住院管理_病案首页手术表 T,
                   住院管理_出院病人信息   TT,
                   住院管理_病案首页       TTT
             WHERE T.住院病历号 = TT.住院病历号
               AND T.住院病历号 = TTT.住院病历号
               AND T.机构编码 = STR_机构编码
               AND TTT.治疗类别 = '3' --西医
               AND TTT.归档人编码 IS NOT NULL
               AND TT.出院时间 BETWEEN DAT_出院时间起始 AND DAT_出院时间截止
               AND T.创建时间 >= TT.入院时间
               AND (TT.住院病历号 LIKE '%' || STR_过滤数据 || '%' OR
                   TT.病案号 LIKE '%' || STR_过滤数据 || '%' OR
                   TT.病人姓名 LIKE '%' || STR_过滤数据 || '%')) G
     WHERE G.RN <= 7;

  ROW_手术记录 CUR_手术记录%ROWTYPE;

  --获取费用记录CURSOR
  CURSOR CUR_费用记录 IS
    SELECT T.机构编码,
           T.住院病历号,
           SUM(T.归类金额) AS 总金额,
           (SELECT (A.实收金额 - A.总补偿金额)
              FROM 住院管理_出院病人发票登记 A
             WHERE A.机构编码 = STR_机构编码
               AND A.住院病历号 = T.住院病历号
               AND A.召回标志 = '否') AS 自付金额,
           NVL(SUM(CASE T.归类编码
                     WHEN '10001' THEN
                      T.归类金额
                   END),
               0) AS 一般医疗服务费总,
           NVL(SUM(CASE T.归类编码
                     WHEN '10002' THEN
                      T.归类金额
                   END),
               0) AS 一般治疗操作费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10003' THEN
                      T.归类金额
                   END),
               0) AS 护理费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10004' THEN
                      T.归类金额
                   END),
               0) AS 综合医疗服务类其他费用,
           NVL(SUM(CASE T.归类编码
                     WHEN '10005' THEN
                      T.归类金额
                   END),
               0) AS 病理诊断费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10006' THEN
                      T.归类金额
                   END),
               0) AS 实验室诊断费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10007' THEN
                      T.归类金额
                   END),
               0) AS 影像学诊断费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10008' THEN
                      T.归类金额
                   END),
               0) AS 临床诊断项目费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10009' THEN
                      T.归类金额
                     WHEN '10010' THEN
                      T.归类金额
                   END),
               0) AS 非手术治疗项目费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10010' THEN
                      T.归类金额
                   END),
               0) AS 其中_临床物理治疗费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10011' THEN
                      T.归类金额
                     WHEN '10012' THEN
                      T.归类金额
                     WHEN '10013' THEN
                      T.归类金额
                   END),
               0) AS 手术治疗费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10012' THEN
                      T.归类金额
                   END),
               0) AS 其中_麻醉费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10013' THEN
                      T.归类金额
                   END),
               0) AS 其中_手术费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10014' THEN
                      T.归类金额
                   END),
               0) AS 康复费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10015' THEN
                      T.归类金额
                   END),
               0) AS 中医治疗费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10016' THEN
                      T.归类金额
                     WHEN '10017' THEN
                      T.归类金额
                   END),
               0) AS 西药费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10017' THEN
                      T.归类金额
                   END),
               0) AS 其中_抗菌药物费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10018' THEN
                      T.归类金额
                   END),
               0) AS 中成药费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10019' THEN
                      T.归类金额
                   END),
               0) AS 中草药费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10020' THEN
                      T.归类金额
                   END),
               0) AS 血费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10021' THEN
                      T.归类金额
                   END),
               0) AS 白蛋白类制品费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10022' THEN
                      T.归类金额
                   END),
               0) AS 球蛋白类制品费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10023' THEN
                      T.归类金额
                   END),
               0) AS 凝血因子类制品费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10024' THEN
                      T.归类金额
                   END),
               0) AS 细胞因子类制品费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10025' THEN
                      T.归类金额
                   END),
               0) AS 检查用一次性医用材料费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10026' THEN
                      T.归类金额
                   END),
               0) AS 治疗用一次性医用材料费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10027' THEN
                      T.归类金额
                   END),
               0) AS 手术用一次性医用材料费,
           NVL(SUM(CASE T.归类编码
                     WHEN '10028' THEN
                      T.归类金额
                   END),
               0) AS 其他费
      FROM (SELECT ZT.编码       AS 归类编码,
                   ZT.名称       AS 归类名称,
                   ZZ.总金额     AS 归类金额,
                   ZZ.机构编码,
                   ZZ.住院病历号,
                   ZZ.记账时间
              FROM 基础项目_字典明细 ZT
              LEFT JOIN (SELECT SUM(总金额) AS 总金额,
                               西医病案编码,
                               机构编码,
                               住院病历号,
                               记账时间
                          FROM (SELECT A.总金额 AS 总金额,
                                       NVL(Z.BAGLBM, '10028') AS 西医病案编码,
                                       A.机构编码,
                                       A.住院病历号,
                                       A.记账时间
                                  FROM 住院管理_出院病人处方 A
                                  LEFT JOIN JCXM_ZLZD_FJXM Z
                                    ON A.机构编码 = Z.JGBM
                                   AND A.项目编码 = Z.XMBM
                                 WHERE A.机构编码 = STR_机构编码
                                   AND A.大类编码 = '1'
                                
                                UNION ALL
                                
                                SELECT A.总金额 AS 总金额,
                                       NVL(Y.BAGLBM, '10028') AS 西医病案编码,
                                       A.机构编码,
                                       A.住院病历号,
                                       A.记账时间
                                  FROM 住院管理_出院病人处方 A
                                  LEFT JOIN JCXM_YPZD_FJSX Y
                                    ON A.机构编码 = Y.JGBM
                                   AND A.项目编码 = Y.YPBM
                                 WHERE A.机构编码 = STR_机构编码
                                   AND A.大类编码 = '2')
                         GROUP BY 机构编码,
                                  住院病历号,
                                  记账时间,
                                  西医病案编码) ZZ
                ON ZT.编码 = ZZ.西医病案编码
             WHERE ZT.分类编码 = 'GB_009001'
               AND ZT.有效状态 = '有效'
               AND ZT.删除标志 = '0'
               AND ZT.名称 LIKE '西医病案_%') T,
           住院管理_出院病人信息 TT,
           住院管理_病案首页 TTT
     WHERE T.住院病历号 = TT.住院病历号
       AND T.住院病历号 = TTT.住院病历号
       AND TTT.治疗类别 = '3' --西医
       AND TTT.归档人编码 IS NOT NULL
       AND TT.出院时间 BETWEEN DAT_出院时间起始 AND DAT_出院时间截止
       AND T.记账时间 >= TT.入院时间
       AND (TT.住院病历号 LIKE '%' || STR_过滤数据 || '%' OR
           TT.病案号 LIKE '%' || STR_过滤数据 || '%' OR
           TT.病人姓名 LIKE '%' || STR_过滤数据 || '%')
     GROUP BY T.机构编码, T.住院病历号;

  ROW_费用记录 CUR_费用记录%ROWTYPE;

  --获取重症监护记录CURSOR
  CURSOR CUR_重症监护记录 IS
    SELECT *
      FROM (SELECT ROW_NUMBER() OVER(PARTITION BY T.住院病历号 ORDER BY T.流水码 DESC) RN,
                   T.科室名称,
                   T.进入时间,
                   T.退出时间,
                   T.住院病历号
              FROM 住院管理_病案首页_重症监护 T,
                   住院管理_出院病人信息      TT,
                   住院管理_病案首页          TTT
             WHERE T.住院病历号 = TT.住院病历号
               AND T.住院病历号 = TTT.住院病历号
               AND TTT.治疗类别 = '3' --西医
               AND TTT.归档人编码 IS NOT NULL
               AND TT.出院时间 BETWEEN DAT_出院时间起始 AND DAT_出院时间截止
               AND T.更新时间 >= TT.入院时间
               AND (TT.住院病历号 LIKE '%' || STR_过滤数据 || '%' OR
                   TT.病案号 LIKE '%' || STR_过滤数据 || '%' OR
                   TT.病人姓名 LIKE '%' || STR_过滤数据 || '%')) G
     WHERE G.RN <= 5;

  ROW_重症监护记录 CUR_重症监护记录%ROWTYPE;

BEGIN

  BEGIN
  
    DELETE FROM 临时表_数据上报_出院流感病例;
  
    --更新基本信息
    INSERT INTO 临时表_数据上报_出院流感病例
      (住院病历号,
       P900, --医疗机构编码
       P6891, --机构名称
       P686, --医疗保险手册（卡）号
       p800, --健康卡号
       P1, --医疗付款方式
       P2, --住院次数
       P3, --病案号
       P4, --姓名
       P5, --性别
       P6, --出生日期
       P7, --年龄
       P8, --婚姻状况
       P9, --职业
       P101, --出生省份
       P102, --出生地市
       P103, --出生地县
       P11, --民族
       P12, --国籍
       P13, --身份证号
       P801, --现住址
       P802, --住宅电话
       P803, --现住址邮政编码
       P14, --工作单位及地址
       P15, --电话
       P16, --工作单位邮政编码
       P17, --户口地址
       P171, --户口所在地邮政编码
       P18, --联系人姓名
       P19, --关系
       P20, --联系人地址
       P804, --入院途径
       P21, --联系人电话
       P22, --入院日期
       P23, --入院科别
       P231, --入院病室
       P24, --转科科别
       P25, --出院日期
       P26, --出院科别
       P261, --出院病室
       P27, --实际住院天数
       P28, --门（急）诊诊断编码
       P281, --门（急）诊诊断描述
       
       --诊断相关
       P372, --过敏药物名称
       P38, --HBsAg
       P39, --HCV-Ab
       P40, --HIV-Ab
       P411, --门诊与出院诊断符合情况
       P412, --入院与出院诊断符合情况
       P413, --术前与术后诊断符合情况
       P414, --临床与病理诊断符合情况
       P415, --放射与病理诊断符合情况
       P421, --抢救次数
       P422, --抢救成功次数
       P687, --最高诊断依据
       P688, --分化程度
       
       P431, --科主任
       P432, --主(副主)任医师
       P433, --主治医师
       P434, --住院医师
       P819, --责任护士
       P435, --进修医师
       P436, --研究生实习医师
       P437, --实习医师
       P438, --编码员
       P44, --病案质量
       P45, --质控医师
       P46, --质控护师
       P47, --质控日期
       
       --手术
       
       P561, --特级护理天数
       P562, --一级护理天数
       P563, --二级护理天数
       P564, --三级护理天数
       
       --重症监护数据
       P57, --死亡患者尸检
       P58, --手术、治疗、检查、诊断为本院第一例
       P581, --手术患者类型
       P60, --随诊
       p611, --随诊周数
       p612, --随诊月数
       p613, --随诊年数
       P59, --示教病例
       P62, --ABO血型
       P63, --Rh血型
       P64, --输血反应
       p651, --红细胞
       p652, --血小板
       P653, --血浆
       P654, --全血
       P655, --自体回收
       P656, --其它
       
       p66, --（婴幼儿）年龄
       P681, --新生儿出生体重1
       P67, --新生儿入院体重
       P731, --入院前多少小时(昏迷时间)
       P732, --入院前多少分钟(昏迷时间)
       P733, --入院后多少小时(昏迷时间)
       P734, --入院后多少分钟(昏迷时间)
       P72, --呼吸机使用时间
       P830, --是否有出院31天内再住院计划
       P831, --出院31天再住院计划目的
       P741, --离院方式
       P742, --转入医院名称
       P743 --社区服务机构名称
       )
      SELECT T.住院病历号,
             T.机构编码,
             '营口经济技术开发区第二人民医院' 机构名称,
             null,
             T.健康卡号,
             T.医疗付费方式,
             T.住院次数,
             T.病案号,
             T.病人姓名,
             NVL(T.性别, '0'),
             TO_CHAR(T.出生日期, 'yyyy-MM-dd') 出生日期,
             TO_NUMBER(REGEXP_REPLACE(T.年龄, '[^-0-9.]', '')) 年龄,
             T.婚姻,
             T.职业,
             T.出生地省,
             T.出生地市,
             T.出生地县,
             T.民族,
             T.国籍,
             T.身份证号,
             T.现住址,
             T.户口电话,
             T.现住址邮编,
             T.工作单位地址,
             T.工作电话,
             T.工作邮政编码,
             T.户口地址,
             T.户口邮政编码,
             T.联系人姓名,
             T.关系,
             T.联系人地址,
             T.入院途径,
             T.联系人电话,
             TO_CHAR(T.入院日期, 'yyyy-MM-dd hh24:miss') AS 入院日期,
             (SELECT A.科室类别
                FROM 基础项目_科室补充资料 A
               WHERE A.机构编码 = STR_机构编码
                 AND A.科室编码 = T.入院科室编码) 入院科室编码,
             T.入院病室,
             (SELECT LISTAGG(转出科室编码, '→') WITHIN GROUP(ORDER BY 转科时间) AS 转科科别
                FROM 住院管理_在院病人转科记录
               WHERE 机构编码 = STR_机构编码
                 AND 住院病历号 = T.住院病历号
                 AND ROWNUM <= 3) 转科科别,
             TO_CHAR(T.出院日期, 'yyyy-MM-dd hh24:miss') AS 出院日期,
             (SELECT A.科室类别
                FROM 基础项目_科室补充资料 A
               WHERE A.机构编码 = STR_机构编码
                 AND A.科室编码 = T.出院科室编码) 出院科室编码,
             T.出院病室,
             T.住院天数,
             (SELECT (CASE
                       WHEN A.ICD码 = A.疾病编码 THEN
                        (SELECT B.ICD码
                           FROM 基础项目_疾病字典 B
                          WHERE B.疾病编码 = A.疾病编码)
                       ELSE
                        A.ICD码
                     END) ICD码
                FROM 住院管理_在院病人诊断 A
               WHERE A.机构编码 = STR_机构编码
                 AND A.住院病历号 = T.住院病历号
                 AND A.诊断类型 = '门诊诊断'
                 AND A.诊断分类 = '1'
                 AND ROWNUM = 1) AS 疾病编码,
             (SELECT A.疾病名称
                FROM 住院管理_在院病人诊断 A
               WHERE A.机构编码 = STR_机构编码
                 AND A.住院病历号 = T.住院病历号
                 AND A.诊断类型 = '门诊诊断'
                 AND A.诊断分类 = '1'
                 AND ROWNUM = 1) AS 门诊诊断名称,
             
             --省略若干诊断
             
             T.过敏药物,
             T.HBSAG,
             T.HCV_AB,
             T.HIV_AB,
             null, --门诊与出院诊断符合情况
             null, --入院与出院诊断符合情况
             null, --术前与术后诊断符合情况
             null, --临床与病理诊断符合情况
             null, --放射与病理诊断符合情况
             T.抢救次数,
             T.成功次数,
             null, --最高诊断依据
             null, --分化程度
             
             (SELECT 人员姓名
                FROM 基础项目_人员资料
               WHERE 机构编码 = T.机构编码
                 AND 删除标志 = '0'
                 AND 人员编码 = T.科主任) AS 科主任,
             (SELECT 人员姓名
                FROM 基础项目_人员资料
               WHERE 机构编码 = T.机构编码
                 AND 删除标志 = '0'
                 AND 人员编码 = T.主任医师) AS 主任医师,
             (SELECT 人员姓名
                FROM 基础项目_人员资料
               WHERE 机构编码 = T.机构编码
                 AND 删除标志 = '0'
                 AND 人员编码 = T.主治医师) AS 主治医师,
             (SELECT 人员姓名
                FROM 基础项目_人员资料
               WHERE 机构编码 = T.机构编码
                 AND 删除标志 = '0'
                 AND 人员编码 = T.住院医师) AS 住院医师,
             (SELECT 人员姓名
                FROM 基础项目_人员资料
               WHERE 机构编码 = T.机构编码
                 AND 删除标志 = '0'
                 AND 人员编码 = T.责任护士) AS 责任护士,
             (SELECT 人员姓名
                FROM 基础项目_人员资料
               WHERE 机构编码 = T.机构编码
                 AND 删除标志 = '0'
                 AND 人员编码 = T.进修医师) AS 进修医师,
             (SELECT 人员姓名
                FROM 基础项目_人员资料
               WHERE 机构编码 = T.机构编码
                 AND 删除标志 = '0'
                 AND 人员编码 = T.研究生实习医师) AS 研究生实习医师,
             (SELECT 人员姓名
                FROM 基础项目_人员资料
               WHERE 机构编码 = T.机构编码
                 AND 删除标志 = '0'
                 AND 人员编码 = T.实习医师) AS 实习医师,
             (SELECT 人员姓名
                FROM 基础项目_人员资料
               WHERE 机构编码 = T.机构编码
                 AND 删除标志 = '0'
                 AND 人员编码 = T.编码员) AS 编码员,
             T.病案质量,
             (SELECT 人员姓名
                FROM 基础项目_人员资料
               WHERE 机构编码 = T.机构编码
                 AND 删除标志 = '0'
                 AND 人员编码 = T.质控医生) AS 质控医生,
             (SELECT 人员姓名
                FROM 基础项目_人员资料
               WHERE 机构编码 = T.机构编码
                 AND 删除标志 = '0'
                 AND 人员编码 = T.质控护士) AS 质控护士,
             
             TO_CHAR(TO_DATE(T.质控日期, 'yyyy-MM-dd hh24:mi:ss'),
                     'yyyy-MM-dd') 质控日期,
             
             --省略若干手术
             
             T.特级护理天数,
             T.一级护理天数,
             T.二级护理天数,
             T.三级护理天数,
             
             --重症监护数据
             T.尸检,
             T.是否本院第一例,
             null, --手术患者类型
             T.随诊,
             T.随诊期限周,
             T.随诊期限月,
             T.随诊期限年,
             T.示教病例,
             T.血型,
             T.RH,
             T.输血反应,
             T.红细胞,
             T.血小板,
             T.血浆,
             T.全血,
             T.自体血回输,
             T.其它,
             
             TO_NUMBER(REGEXP_REPLACE(T.年龄不足1周岁, '[^-0-9.]', '')) 年龄不足1周岁,
             (CASE
               WHEN TO_NUMBER(T.入院日期 - T.出生日期) < 28 THEN
                REGEXP_REPLACE(T.新生儿出生体重, '[^-0-9.]', '')
               ELSE
                ''
             END) AS 新生儿出生体重,
             (CASE
               WHEN TO_NUMBER(T.入院日期 - T.出生日期) < 28 THEN
                REGEXP_REPLACE(T.新生儿入院体重, '[^-0-9.]', '')
               ELSE
                ''
             END) AS 新生儿入院体重,
             
             TO_NUMBER(REGEXP_REPLACE(颅脑损伤患者昏迷时间入院前小时,
                                      '[^-0-9.]',
                                      '')) 颅脑损伤患者昏迷时间入院前小时,
             TO_NUMBER(REGEXP_REPLACE(颅脑损伤患者昏迷时间入院前分钟,
                                      '[^-0-9.]',
                                      '')) 颅脑损伤患者昏迷时间入院前分钟,
             TO_NUMBER(REGEXP_REPLACE(颅脑损伤患者昏迷时间入院后小时,
                                      '[^-0-9.]',
                                      '')) 颅脑损伤患者昏迷时间入院后小时,
             TO_NUMBER(REGEXP_REPLACE(颅脑损伤患者昏迷时间入院分钟,
                                      '[^-0-9.]',
                                      '')) 颅脑损伤患者昏迷时间入院分钟,
             T.有创呼吸机使用时间,
             T.是否有出院31天在住院计划,
             T.目的,
             T.离院方式,
             T.医嘱转院,
             T.医嘱转卫生服务机构
      
        FROM 住院管理_病案首页 T, 住院管理_出院病人信息 TT
       WHERE T.住院病历号 = TT.住院病历号
         AND T.机构编码 = STR_机构编码
         AND T.治疗类别 = '3' --西医
         AND T.出院日期 BETWEEN DAT_出院时间起始 AND DAT_出院时间截止
         AND T.归档人编码 IS NOT NULL
         AND (T.住院病历号 LIKE '%' || STR_过滤数据 || '%' OR
             T.病案号 LIKE '%' || STR_过滤数据 || '%' OR
             T.病人姓名 LIKE '%' || STR_过滤数据 || '%');
  END;

  --更新诊断记录信息
  FOR ROW_诊断记录 IN CUR_诊断记录 LOOP
    EXIT WHEN CUR_诊断记录%NOTFOUND;
    IF ROW_诊断记录.诊断类型 = '出院诊断' THEN
      STR_SQL := 'UPDATE 临时表_数据上报_出院流感病例 SET P321=:1,P322=:2,P805=:3,P323=:4 WHERE 住院病历号=:5';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_诊断记录.ICD码, ROW_诊断记录.疾病名称, ROW_诊断记录.入院病情, ROW_诊断记录.出院情况, ROW_诊断记录.住院病历号;
    ELSIF ROW_诊断记录.诊断类型 = '入院诊断' THEN
      STR_SQL := 'UPDATE 临时表_数据上报_出院流感病例 SET P30=:1,P301=:2,P29=:3,P31=:4 WHERE 住院病历号=:5';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_诊断记录.ICD码, ROW_诊断记录.疾病名称, ROW_诊断记录.入院时情况, ROW_诊断记录.入院后确诊日期;
    ELSIF ROW_诊断记录.诊断类型 = '其他诊断' THEN
      STR_SQL := 'UPDATE 临时表_数据上报_出院流感病例 SET QTZDBM' || ROW_诊断记录.RN ||
                 '=:1,QTZDMS' || ROW_诊断记录.RN || '=:2,QTZDRYBQ' ||
                 ROW_诊断记录.RN || '=:3,QTZDCYQK' || ROW_诊断记录.RN ||
                 '=:4 WHERE 住院病历号=:5';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_诊断记录.ICD码, ROW_诊断记录.疾病名称, ROW_诊断记录.入院病情, ROW_诊断记录.出院情况, ROW_诊断记录.住院病历号;
    ELSIF ROW_诊断记录.诊断类型 = '损伤和中毒外部原因' THEN
      STR_SQL := 'UPDATE 临时表_数据上报_出院流感病例 SET WBYSBM=:1,WBYSMC=:2 WHERE 住院病历号=:3';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_诊断记录.ICD码, ROW_诊断记录.疾病名称, ROW_诊断记录.住院病历号;
    ELSIF ROW_诊断记录.诊断类型 = '病理诊断' THEN
      STR_SQL := 'UPDATE 临时表_数据上报_出院流感病例 SET BLZDBM=:1,BLZDBM=:2,BLH=:3 WHERE 住院病历号=:4';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_诊断记录.ICD码, ROW_诊断记录.疾病名称, ROW_诊断记录.病理号, ROW_诊断记录.住院病历号;
    END IF;
  END LOOP;

  --更新费用记录信息
  FOR ROW_费用记录 IN CUR_费用记录 LOOP
    EXIT WHEN CUR_费用记录%NOTFOUND;
    UPDATE 临时表_数据上报_出院流感病例 T
       SET T.P782 = ROW_费用记录.总金额,
           T.P751 = ROW_费用记录.自付金额,
           T.P752 = ROW_费用记录.一般医疗服务费总,
           T.P754 = ROW_费用记录.一般治疗操作费,
           T.P755 = ROW_费用记录.护理费,
           T.P756 = ROW_费用记录.综合医疗服务类其他费用,
           T.P757 = ROW_费用记录.病理诊断费,
           T.P758 = ROW_费用记录.实验室诊断费,
           T.P759 = ROW_费用记录.影像学诊断费,
           T.P760 = ROW_费用记录.临床诊断项目费,
           T.P761 = ROW_费用记录.非手术治疗项目费,
           T.P762 = ROW_费用记录.其中_临床物理治疗费,
           T.P763 = ROW_费用记录.手术治疗费,
           T.P764 = ROW_费用记录.其中_麻醉费,
           T.P765 = ROW_费用记录.其中_手术费,
           T.P767 = ROW_费用记录.康复费,
           T.P768 = ROW_费用记录.中医治疗费,
           T.P769 = ROW_费用记录.西药费,
           T.P770 = ROW_费用记录.其中_抗菌药物费,
           T.P771 = ROW_费用记录.中成药费,
           T.P772 = ROW_费用记录.中草药费,
           T.P773 = ROW_费用记录.血费,
           T.P774 = ROW_费用记录.白蛋白类制品费,
           T.P775 = ROW_费用记录.球蛋白类制品费,
           T.P776 = ROW_费用记录.凝血因子类制品费,
           T.P777 = ROW_费用记录.细胞因子类制品费,
           T.P778 = ROW_费用记录.检查用一次性医用材料费,
           T.P779 = ROW_费用记录.治疗用一次性医用材料费,
           T.P780 = ROW_费用记录.手术用一次性医用材料费,
           T.P781 = ROW_费用记录.其他费
    
     WHERE T.住院病历号 = ROW_费用记录.住院病历号;
  END LOOP;

  --更新手术记录信息
  FOR ROW_手术记录 IN CUR_手术记录 LOOP
    EXIT WHEN CUR_手术记录%NOTFOUND;
  
    STR_SQL := 'UPDATE 临时表_数据上报_出院流感病例 SET SSBM' || ROW_手术记录.RN ||
               '=:1, SSRQ' || ROW_手术记录.RN || '=:2, SSJB' || ROW_手术记录.RN ||
               '= :3
               , SSMC' || ROW_手术记录.RN || '=:4,  SZ' ||
               ROW_手术记录.RN || '=:5, YZ' || ROW_手术记录.RN || '=:6, EZ' ||
               ROW_手术记录.RN || '=:7, MZFS' || ROW_手术记录.RN || '=:8, MZFJ' ||
               ROW_手术记录.RN || '=:9, QKYHDJ' || ROW_手术记录.RN ||
               '=:10 WHERE 住院病历号=:11';
    EXECUTE IMMEDIATE STR_SQL
      USING ROW_手术记录.国标码, ROW_手术记录.手术操作日期, ROW_手术记录.手术级别编码, ROW_手术记录.手术操作名称, ROW_手术记录.术者, ROW_手术记录.I助, ROW_手术记录.II助, ROW_手术记录.麻醉方式编码, ROW_手术记录.麻醉分级编码, ROW_手术记录.切口愈合等级编码, ROW_手术记录.麻醉医师;
  
  END LOOP;

  --更新重症监护记录信息
  FOR ROW_重症监护记录 IN CUR_重症监护记录 LOOP
    EXIT WHEN CUR_重症监护记录%NOTFOUND;
    STR_SQL := 'UPDATE 临时表_数据上报_出院流感病例 SET JHSMC' || ROW_重症监护记录.RN ||
               '=:1, JRSJ' || ROW_重症监护记录.RN || '=:2, TCSJ' || ROW_重症监护记录.RN ||
               '= :3 WHERE 住院病历号=:4';
  
    EXECUTE IMMEDIATE STR_SQL
      USING ROW_重症监护记录.科室名称, ROW_重症监护记录.进入时间, ROW_重症监护记录.退出时间, ROW_重症监护记录.住院病历号;
  
  END LOOP;

  --返回数据集
  OPEN CUR_导出_列表信息 FOR
    SELECT T.* FROM 临时表_数据上报_出院流感病例 T;

END PR_数据上报_出院流感病历;
/
