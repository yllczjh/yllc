CREATE OR REPLACE PROCEDURE PR_������ҳ_��ȡ��������(STR_����          IN VARCHAR2,
                                           CUR_����_�б���Ϣ OUT SYS_REFCURSOR) IS
  STR_�������� VARCHAR2(100);
  STR_SQL      VARCHAR2(1000);

  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 1);
  DAT_��Ժʱ����ʼ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 2),
                             'yyyy-MM-dd hh24:mi:ss');
  DAT_��Ժʱ���ֹ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 3),
                             'yyyy-MM-dd hh24:mi:ss');
  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 4);

  --��ȡ��ϼ�¼CURSOR
  CURSOR CUR_��ϼ�¼ IS
    SELECT *
      FROM (SELECT ROW_NUMBER() OVER(PARTITION BY T.סԺ������, T.������� ORDER BY T.�������, T.�Ƿ������ DESC) RN,
                   T.סԺ������,
                   (CASE
                     WHEN T.ICD�� = T.�������� THEN
                      (SELECT B.ICD��
                         FROM ������Ŀ_�����ֵ� B
                        WHERE B.�������� = T.��������)
                     ELSE
                      T.ICD��
                   END) ICD��,
                   T.��������,
                   T.��Ժ����,
                   T.�������
              FROM סԺ����_��Ժ������� T,
                   סԺ����_��Ժ������Ϣ TT,
                   סԺ����_������ҳ     TTT
             WHERE T.סԺ������ = TT.סԺ������
               AND T.סԺ������ = TTT.סԺ������
               AND T.�������� = STR_��������
               AND TTT.�鵵�˱��� IS NOT NULL
               AND TT.��Ժʱ�� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
               AND T.��Ϸ��� = '1'
               AND T.������� IN
                   ('��Ժ���', '�������', '���˺��ж��ⲿԭ��', '�������')
               AND T.����ʱ�� >= TT.��Ժʱ��
               AND (TT.סԺ������ LIKE '%' || STR_�������� || '%' OR
                   TT.������ LIKE '%' || STR_�������� || '%' OR
                   TT.�������� LIKE '%' || STR_�������� || '%')) G
     WHERE (G.������� = '�������' AND G.RN <= 15)
        OR (G.������� <> '�������' AND G.RN = 1);
  ROW_��ϼ�¼ CUR_��ϼ�¼%ROWTYPE;

  --��ȡ������¼CURSOR
  CURSOR CUR_������¼ IS
    SELECT *
      FROM (SELECT ROW_NUMBER() OVER(PARTITION BY T.סԺ������ ORDER BY T.��ˮ�� DESC) RN,
                   T.סԺ������,
                   T.������,
                   TO_CHAR(T.������������, 'yyyy-MM-dd') ������������,
                   T.�����������,
                   T.������������,
                   (SELECT A.��Ա����
                      FROM ������Ŀ_��Ա���� A
                     WHERE A.�������� = T.��������
                       AND A.ɾ����־ = '0'
                       AND A.��Ա���� = T.����) AS ����,
                   (SELECT A.��Ա����
                      FROM ������Ŀ_��Ա���� A
                     WHERE A.�������� = T.��������
                       AND A.ɾ����־ = '0'
                       AND A.��Ա���� = T.I��) AS I��,
                   (SELECT A.��Ա����
                      FROM ������Ŀ_��Ա���� A
                     WHERE A.�������� = T.��������
                       AND A.ɾ����־ = '0'
                       AND A.��Ա���� = T.II��) AS II��,
                   DECODE(FU_ͨ��_��ȡ�ַ���ֵ(T.�п����ϵȼ�, '/', 1),
                          '��',
                          '2',
                          '��',
                          '3',
                          '��',
                          '4',
                          '') �пڵȼ�,
                   DECODE(FU_ͨ��_��ȡ�ַ���ֵ(T.�п����ϵȼ�, '/', 2),
                          '��',
                          '1',
                          '��',
                          '2',
                          '��',
                          '3',
                          '9',
                          '') �п����ϵȼ�,
                   T.����ʽ����,
                   (SELECT A.��Ա����
                      FROM ������Ŀ_��Ա���� A
                     WHERE A.�������� = T.��������
                       AND A.ɾ����־ = '0'
                       AND A.��Ա���� = T.����ҽʦ) AS ����ҽʦ
              FROM סԺ����_������ҳ������ T,
                   סԺ����_��Ժ������Ϣ   TT,
                   סԺ����_������ҳ       TTT
             WHERE T.סԺ������ = TT.סԺ������
               AND T.סԺ������ = TTT.סԺ������
               AND T.�������� = STR_��������
               AND TTT.�鵵�˱��� IS NOT NULL
               AND TT.��Ժʱ�� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
               AND T.����ʱ�� >= TT.��Ժʱ��
               AND (TT.סԺ������ LIKE '%' || STR_�������� || '%' OR
                   TT.������ LIKE '%' || STR_�������� || '%' OR
                   TT.�������� LIKE '%' || STR_�������� || '%')) G
     WHERE G.RN <= 7;

  ROW_������¼ CUR_������¼%ROWTYPE;

  --��ȡ���ü�¼CURSOR
  CURSOR CUR_���ü�¼ IS
    SELECT T.��������,
           T.סԺ������,
           SUM(T.������) AS �ܽ��,
           (SELECT (A.ʵ�ս�� - A.�ܲ������)
              FROM סԺ����_��Ժ���˷�Ʊ�Ǽ� A
             WHERE A.�������� = STR_��������
               AND A.סԺ������ = T.סԺ������
               AND A.�ٻر�־ = '��') AS �Ը����,
           NVL(SUM(CASE T.�������
                     WHEN '10001' THEN
                      T.������
                   END),
               0) AS һ��ҽ�Ʒ������,
           NVL(SUM(CASE T.�������
                     WHEN '10002' THEN
                      T.������
                   END),
               0) AS һ�����Ʋ�����,
           NVL(SUM(CASE T.�������
                     WHEN '10003' THEN
                      T.������
                   END),
               0) AS �����,
           NVL(SUM(CASE T.�������
                     WHEN '10004' THEN
                      T.������
                   END),
               0) AS �ۺ�ҽ�Ʒ�������������,
           NVL(SUM(CASE T.�������
                     WHEN '10005' THEN
                      T.������
                   END),
               0) AS ������Ϸ�,
           NVL(SUM(CASE T.�������
                     WHEN '10006' THEN
                      T.������
                   END),
               0) AS ʵ������Ϸ�,
           NVL(SUM(CASE T.�������
                     WHEN '10007' THEN
                      T.������
                   END),
               0) AS Ӱ��ѧ��Ϸ�,
           NVL(SUM(CASE T.�������
                     WHEN '10008' THEN
                      T.������
                   END),
               0) AS �ٴ������Ŀ��,
           NVL(SUM(CASE T.�������
                     WHEN '10009' THEN
                      T.������
                     WHEN '10010' THEN
                      T.������
                   END),
               0) AS ������������Ŀ��,
           NVL(SUM(CASE T.�������
                     WHEN '10010' THEN
                      T.������
                   END),
               0) AS ����_�ٴ��������Ʒ�,
           NVL(SUM(CASE T.�������
                     WHEN '10011' THEN
                      T.������
                     WHEN '10012' THEN
                      T.������
                     WHEN '10013' THEN
                      T.������
                   END),
               0) AS �������Ʒ�,
           NVL(SUM(CASE T.�������
                     WHEN '10012' THEN
                      T.������
                   END),
               0) AS ����_�����,
           NVL(SUM(CASE T.�������
                     WHEN '10013' THEN
                      T.������
                   END),
               0) AS ����_������,
           NVL(SUM(CASE T.�������
                     WHEN '10014' THEN
                      T.������
                   END),
               0) AS ������,
           NVL(SUM(CASE T.�������
                     WHEN '10015' THEN
                      T.������
                   END),
               0) AS ��ҽ���Ʒ�,
           NVL(SUM(CASE T.�������
                     WHEN '10016' THEN
                      T.������
                     WHEN '10017' THEN
                      T.������
                   END),
               0) AS ��ҩ��,
           NVL(SUM(CASE T.�������
                     WHEN '10017' THEN
                      T.������
                   END),
               0) AS ����_����ҩ���,
           NVL(SUM(CASE T.�������
                     WHEN '10018' THEN
                      T.������
                   END),
               0) AS �г�ҩ��,
           NVL(SUM(CASE T.�������
                     WHEN '10019' THEN
                      T.������
                   END),
               0) AS �в�ҩ��,
           NVL(SUM(CASE T.�������
                     WHEN '10020' THEN
                      T.������
                   END),
               0) AS Ѫ��,
           NVL(SUM(CASE T.�������
                     WHEN '10021' THEN
                      T.������
                   END),
               0) AS �׵�������Ʒ��,
           NVL(SUM(CASE T.�������
                     WHEN '10022' THEN
                      T.������
                   END),
               0) AS �򵰰�����Ʒ��,
           NVL(SUM(CASE T.�������
                     WHEN '10023' THEN
                      T.������
                   END),
               0) AS ��Ѫ��������Ʒ��,
           NVL(SUM(CASE T.�������
                     WHEN '10024' THEN
                      T.������
                   END),
               0) AS ϸ����������Ʒ��,
           NVL(SUM(CASE T.�������
                     WHEN '10025' THEN
                      T.������
                   END),
               0) AS �����һ����ҽ�ò��Ϸ�,
           NVL(SUM(CASE T.�������
                     WHEN '10026' THEN
                      T.������
                   END),
               0) AS ������һ����ҽ�ò��Ϸ�,
           NVL(SUM(CASE T.�������
                     WHEN '10027' THEN
                      T.������
                   END),
               0) AS ������һ����ҽ�ò��Ϸ�,
           NVL(SUM(CASE T.�������
                     WHEN '10028' THEN
                      T.������
                   END),
               0) AS ������
      FROM (SELECT ZT.����       AS �������,
                   ZT.����       AS ��������,
                   ZZ.�ܽ��     AS ������,
                   ZZ.��������,
                   ZZ.סԺ������,
                   ZZ.����ʱ��
              FROM ������Ŀ_�ֵ���ϸ ZT
              LEFT JOIN (SELECT SUM(�ܽ��) AS �ܽ��,
                               ��ҽ��������,
                               ��������,
                               סԺ������,
                               ����ʱ��
                          FROM (SELECT A.�ܽ�� AS �ܽ��,
                                       NVL(Z.BAGLBM, '10028') AS ��ҽ��������,
                                       A.��������,
                                       A.סԺ������,
                                       A.����ʱ��
                                  FROM סԺ����_��Ժ���˴��� A
                                  LEFT JOIN JCXM_ZLZD_FJXM Z
                                    ON A.�������� = Z.JGBM
                                   AND A.��Ŀ���� = Z.XMBM
                                 WHERE A.�������� = STR_��������
                                   AND A.������� = '1'
                                
                                UNION ALL
                                
                                SELECT A.�ܽ�� AS �ܽ��,
                                       NVL(Y.BAGLBM, '10028') AS ��ҽ��������,
                                       A.��������,
                                       A.סԺ������,
                                       A.����ʱ��
                                  FROM סԺ����_��Ժ���˴��� A
                                  LEFT JOIN JCXM_YPZD_FJSX Y
                                    ON A.�������� = Y.JGBM
                                   AND A.��Ŀ���� = Y.YPBM
                                 WHERE A.�������� = STR_��������
                                   AND A.������� = '2')
                         GROUP BY ��������,
                                  סԺ������,
                                  ����ʱ��,
                                  ��ҽ��������) ZZ
                ON ZT.���� = ZZ.��ҽ��������
             WHERE ZT.������� = 'GB_009001'
               AND ZT.��Ч״̬ = '��Ч'
               AND ZT.ɾ����־ = '0'
               AND ZT.���� LIKE '��ҽ����_%') T,
           סԺ����_��Ժ������Ϣ TT,
           סԺ����_������ҳ TTT
     WHERE T.סԺ������ = TT.סԺ������
       AND T.סԺ������ = TTT.סԺ������
       AND TTT.�鵵�˱��� IS NOT NULL
       AND TT.��Ժʱ�� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
       AND T.����ʱ�� >= TT.��Ժʱ��
       AND (TT.סԺ������ LIKE '%' || STR_�������� || '%' OR
           TT.������ LIKE '%' || STR_�������� || '%' OR
           TT.�������� LIKE '%' || STR_�������� || '%')
     GROUP BY T.��������, T.סԺ������;

  ROW_���ü�¼ CUR_���ü�¼%ROWTYPE;

BEGIN

  --��ȡ��������
  BEGIN
    SELECT A.��������
      INTO STR_��������
      FROM ������Ŀ_�������� A
     WHERE A.�������� = STR_��������
       AND ROWNUM = 1;
  EXCEPTION
    WHEN OTHERS THEN
      STR_�������� := '';
  END;

  BEGIN
  
    DELETE FROM ��ʱ��_������ҳ��;
  
    --���»�����Ϣ
    INSERT INTO ��ʱ��_������ҳ��
      (סԺ������,
       USERNAME, --��������
       YLFKFS, --���ʽ
       JKKH, --��������
       ZYCS, --סԺ����
       BAH, --������
       XM, --��������
       XB, --�Ա�
       CSRQ, --��������
       NL, --����
       GJ, --����
       BZYZSNL, --���䲻��1����
       XSECSTZ, --��������������
       XSERYTZ, --��������Ժ����
       CSD, --������
       GG, --����
       MZ, --����
       SFZH, --���֤��
       ZY, --ְҵ
       HY, --����
       XZZ, --��סַ
       DH, --�绰
       YB1, --�ʱ�
       HKDZ, --���ڵ�ַ,
       YB2, --�ʱ�
       GZDWJDZ, --������λ����ַ
       DWDH, --��λ�绰
       YB3, --�ʱ�
       LXRXM, --��ϵ������
       GX, --��ϵ
       DZ, --��ַ
       DH2, --�绰
       RYTJ, --��Ժ;��
       RYSJ, --��Ժʱ��
       RYSJS, --ʱ
       RYKB, --��Ժ�Ʊ�
       RYBF, --��Ժ����
       ZKKB, --ת�ƿƱ�
       CYSJ, --��Ժʱ��
       CYSJS, --ʱ
       CYKB, --��Ժ�Ʊ�
       CYBF, --��Ժ����
       SJZYTS, --ʵ��סԺ(��)
       MZZD, --��(��)�����
       JBBM, --��������   
       --ʡ���������
       BLH, --�����
       YWGM, --ҩ�����
       GMYW, --����ҩ��
       SWHZSJ, --��������ʬ��
       XX, --Ѫ��
       RH,
       KZR, --������
       ZRYS, --���Σ������Σ�ҽʦ
       ZZYS, --����ҽʦ
       ZYYS, --סԺҽʦ
       ZRHS, --���λ�ʿ
       JXYS, --����ҽʦס
       SXYS, --ʵϰҽʦ
       BMY, --����Ա
       BAZL, --��������
       ZKYS, --�ʿ�ҽʦ
       ZKHS, --�ʿػ�ʿ
       ZKRQ, --�ʿ�����   
       --ʡ���������
       LYFS, --��Ժ��ʽ
       YZZY_YLJG, --ҽ��תԺ�������ҽ�ƻ�������
       WSY_YLJG, --ҽ��ת���������������/��������Ժ�������ҽ�ƻ�������
       SFZZYJH, --�Ƿ��г�Ժ31������סԺ�ƻ��������
       MD, --Ŀ��
       RYQ_T, --­�����˻��߻�����Ժǰʱ����
       RYQ_XS, --Сʱ
       RYQ_F, --����
       RYH_T, --­�����˻��߻�����Ժ��ʱ����
       RYH_XS, --Сʱ
       RYH_F --����  
       --ʡ�Է�������
       )
      SELECT T.סԺ������,
             STR_��������,
             T.ҽ�Ƹ��ѷ�ʽ,
             T.��������,
             T.סԺ����,
             T.������,
             T.��������,
             NVL(T.�Ա�, '0'),
             TO_CHAR(T.��������, 'yyyy-MM-dd') ��������,
             TO_NUMBER(REGEXP_REPLACE(T.����, '[^-0-9.]', '')) ����,
             T.����,
             TO_NUMBER(REGEXP_REPLACE(T.���䲻��1����, '[^-0-9.]', '')) ���䲻��1����,
             (CASE
               WHEN TO_NUMBER(T.��Ժ���� - T.��������) < 28 THEN
                REGEXP_REPLACE(T.��������������, '[^-0-9.]', '')
               ELSE
                ''
             END) AS ��������������,
             (CASE
               WHEN TO_NUMBER(T.��Ժ���� - T.��������) < 28 THEN
                REGEXP_REPLACE(T.��������Ժ����, '[^-0-9.]', '')
               ELSE
                ''
             END) AS ��������Ժ����,
             ((SELECT ����
                 FROM ������Ŀ_�ֵ���ϸ
                WHERE ������� = 'RC036'
                  AND ɾ����־ = '0'
                  AND ���� = T.������ʡ) || T.�������� || T.��������) AS ������,
             ((SELECT ����
                 FROM ������Ŀ_�ֵ���ϸ
                WHERE ������� = 'RC036'
                  AND ɾ����־ = '0'
                  AND ���� = T.����ʡ) || T.������ || T.��������) AS ����,
             T.����,
             T.���֤��,
             T.ְҵ,
             T.����,
             T.��סַ,
             T.���ڵ绰,
             T.��סַ�ʱ�,
             T.���ڵ�ַ,
             T.������������,
             T.������λ��ַ,
             T.�����绰,
             T.������������,
             T.��ϵ������,
             T.��ϵ,
             T.��ϵ�˵�ַ,
             T.��ϵ�˵绰,
             T.��Ժ;��,
             TO_CHAR(T.��Ժ����, 'yyyy-MM-dd') AS ��Ժ����,
             TO_CHAR(T.��Ժ����, 'HH') ʱ,
             (SELECT A.�������
                FROM ������Ŀ_���Ҳ������� A
               WHERE A.�������� = STR_��������
                 AND A.���ұ��� = T.��Ժ���ұ���) ��Ժ���ұ���,
             T.��Ժ����,
             (SELECT LISTAGG(ת�����ұ���, '��') WITHIN GROUP(ORDER BY ת��ʱ��) AS ת�ƿƱ�
                FROM סԺ����_��Ժ����ת�Ƽ�¼
               WHERE �������� = STR_��������
                 AND סԺ������ = T.סԺ������
                 AND ROWNUM <= 3) ת�ƿƱ�,
             TO_CHAR(T.��Ժ����, 'yyyy-MM-dd') AS ��Ժ����,
             TO_CHAR(T.��Ժ����, 'HH') ʱ,
             (SELECT A.�������
                FROM ������Ŀ_���Ҳ������� A
               WHERE A.�������� = STR_��������
                 AND A.���ұ��� = T.��Ժ���ұ���) ��Ժ���ұ���,
             T.��Ժ����,
             T.סԺ����,
             (SELECT A.��������
                FROM סԺ����_��Ժ������� A
               WHERE A.�������� = STR_��������
                 AND A.סԺ������ = T.סԺ������
                 AND A.������� = '�������'
                 AND A.��Ϸ��� = '1'
                 AND ROWNUM = 1) AS �����������,
             (SELECT (CASE
                       WHEN A.ICD�� = A.�������� THEN
                        (SELECT B.ICD��
                           FROM ������Ŀ_�����ֵ� B
                          WHERE B.�������� = A.��������)
                       ELSE
                        A.ICD��
                     END) ICD��
                FROM סԺ����_��Ժ������� A
               WHERE A.�������� = STR_��������
                 AND A.סԺ������ = T.סԺ������
                 AND A.������� = '�������'
                 AND A.��Ϸ��� = '1'
                 AND ROWNUM = 1) AS ��������,
             --ʡ���������        
             T.�����,
             DECODE(T.ҩ�����, '1', '��', '2', '��'),
             T.����ҩ��,
             NVL(T.ʬ��, '��'),
             T.Ѫ��,
             T.RH,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.������) AS ������,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.����ҽʦ) AS ����ҽʦ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.����ҽʦ) AS ����ҽʦ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.סԺҽʦ) AS סԺҽʦ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.���λ�ʿ) AS ���λ�ʿ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.����ҽʦ) AS ����ҽʦ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.ʵϰҽʦ) AS ʵϰҽʦ,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.����Ա) AS ����Ա,
             T.��������,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.�ʿ�ҽ��) AS �ʿ�ҽ��,
             (SELECT ��Ա����
                FROM ������Ŀ_��Ա����
               WHERE �������� = T.��������
                 AND ɾ����־ = '0'
                 AND ��Ա���� = T.�ʿػ�ʿ) AS �ʿػ�ʿ,
             TO_CHAR(TO_DATE(T.�ʿ�����, 'yyyy-MM-dd hh24:mi:ss'),
                     'yyyy-MM-dd') �ʿ�����,
             --ʡ����������          
             T.��Ժ��ʽ,
             T.ҽ��תԺ,
             T.ҽ��ת�����������,
             T.�Ƿ��г�Ժ31����סԺ�ƻ�,
             T.Ŀ��,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����Ժǰ��,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժǰ��,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����ԺǰСʱ,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����ԺǰСʱ,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����Ժǰ����,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժǰ����,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����Ժ����,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժ����,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����Ժ��Сʱ,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժ��Сʱ,
             TO_NUMBER(REGEXP_REPLACE(­�����˻��߻���ʱ����Ժ����,
                                      '[^-0-9.]',
                                      '')) ­�����˻��߻���ʱ����Ժ����
        FROM סԺ����_������ҳ T, סԺ����_��Ժ������Ϣ TT
       WHERE T.סԺ������ = TT.סԺ������
         AND T.�������� = STR_��������
         AND T.��Ժ���� BETWEEN DAT_��Ժʱ����ʼ AND DAT_��Ժʱ���ֹ
         AND T.�鵵�˱��� IS NOT NULL
         AND (T.סԺ������ LIKE '%' || STR_�������� || '%' OR
             T.������ LIKE '%' || STR_�������� || '%' OR
             T.�������� LIKE '%' || STR_�������� || '%');
  END;

  --������ϼ�¼��Ϣ
  FOR ROW_��ϼ�¼ IN CUR_��ϼ�¼ LOOP
    EXIT WHEN CUR_��ϼ�¼%NOTFOUND;
    IF ROW_��ϼ�¼.������� = '��Ժ���' THEN
      STR_SQL := 'UPDATE ��ʱ��_������ҳ�� SET ZYZD=:1,JBDM=:2,RYBQ=:3 WHERE סԺ������=:4';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_��ϼ�¼.��������, ROW_��ϼ�¼.ICD��, ROW_��ϼ�¼.��Ժ����, ROW_��ϼ�¼.סԺ������;
    ELSIF ROW_��ϼ�¼.������� = '�������' THEN
      STR_SQL := 'UPDATE ��ʱ��_������ҳ�� SET QTZD' || ROW_��ϼ�¼.RN || '=:1,JBDM' ||
                 ROW_��ϼ�¼.RN || '=:2,RYBQ' || ROW_��ϼ�¼.RN ||
                 '=:3 WHERE סԺ������=:4';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_��ϼ�¼.��������, ROW_��ϼ�¼.ICD��, ROW_��ϼ�¼.��Ժ����, ROW_��ϼ�¼.סԺ������;
    ELSIF ROW_��ϼ�¼.������� = '���˺��ж��ⲿԭ��' THEN
      STR_SQL := 'UPDATE ��ʱ��_������ҳ�� SET WBYY=:1,H23=:2 WHERE סԺ������=:3';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_��ϼ�¼.��������, ROW_��ϼ�¼.ICD��, ROW_��ϼ�¼.סԺ������;
    ELSIF ROW_��ϼ�¼.������� = '�������' THEN
      STR_SQL := 'UPDATE ��ʱ��_������ҳ�� SET BLZD=:1,JBMM=:2 WHERE סԺ������=:3';
      EXECUTE IMMEDIATE STR_SQL
        USING ROW_��ϼ�¼.��������, ROW_��ϼ�¼.ICD��, ROW_��ϼ�¼.סԺ������;
    END IF;
  END LOOP;

  --���·��ü�¼��Ϣ
  FOR ROW_���ü�¼ IN CUR_���ü�¼ LOOP
    EXIT WHEN CUR_���ü�¼%NOTFOUND;
    UPDATE ��ʱ��_������ҳ�� T
       SET T.ZFY      = ROW_���ü�¼.�ܽ��,
           T.ZFJE     = ROW_���ü�¼.�Ը����,
           T.YLFUF    = ROW_���ü�¼.һ��ҽ�Ʒ������,
           T.ZLCZF    = ROW_���ü�¼.һ�����Ʋ�����,
           T.HLF      = ROW_���ü�¼.�����,
           T.QTFY     = ROW_���ü�¼.�ۺ�ҽ�Ʒ�������������,
           T.BLZDF    = ROW_���ü�¼.������Ϸ�,
           T.SYSZDF   = ROW_���ü�¼.ʵ������Ϸ�,
           T.YXXZDF   = ROW_���ü�¼.Ӱ��ѧ��Ϸ�,
           T.LCZDXMF  = ROW_���ü�¼.�ٴ������Ŀ��,
           T.FSSZLXMF = ROW_���ü�¼.������������Ŀ��,
           T.WLZLF    = ROW_���ü�¼.����_�ٴ��������Ʒ�,
           T.SSZLF    = ROW_���ü�¼.�������Ʒ�,
           T.MAF      = ROW_���ü�¼.����_�����,
           T.SSF      = ROW_���ü�¼.����_������,
           T.KFF      = ROW_���ü�¼.������,
           T.ZYZLF    = ROW_���ü�¼.��ҽ���Ʒ�,
           T.XYF      = ROW_���ü�¼.��ҩ��,
           T.KJYWF    = ROW_���ü�¼.����_����ҩ���,
           T.ZCYF     = ROW_���ü�¼.�г�ҩ��,
           T.ZCYF1    = ROW_���ü�¼.�в�ҩ��,
           T.XF       = ROW_���ü�¼.Ѫ��,
           T.BDBLZPF  = ROW_���ü�¼.�׵�������Ʒ��,
           T.QDBLZPF  = ROW_���ü�¼.�򵰰�����Ʒ��,
           T.NXYZLZPF = ROW_���ü�¼.��Ѫ��������Ʒ��,
           T.XBYZLZPF = ROW_���ü�¼.ϸ����������Ʒ��,
           T.HCYYCLF  = ROW_���ü�¼.�����һ����ҽ�ò��Ϸ�,
           T.YYCLF    = ROW_���ü�¼.������һ����ҽ�ò��Ϸ�,
           T.YCXYYCLF = ROW_���ü�¼.������һ����ҽ�ò��Ϸ�,
           T.QTF      = ROW_���ü�¼.������
    
     WHERE T.סԺ������ = ROW_���ü�¼.סԺ������;
  END LOOP;

  --����������¼��Ϣ
  FOR ROW_������¼ IN CUR_������¼ LOOP
    EXIT WHEN CUR_������¼%NOTFOUND;
  
    STR_SQL := 'UPDATE ��ʱ��_������ҳ�� SET SSJCZBM' || ROW_������¼.RN ||
               '=:1, SSJCZRQ' || ROW_������¼.RN || '=:2, SSJB' || ROW_������¼.RN ||
               '= :3
               , SSJCZMC' || ROW_������¼.RN || '=:4, SZ' ||
               ROW_������¼.RN || '=:5, YZ' || ROW_������¼.RN || '=:6, EZ' ||
               ROW_������¼.RN || '=:7, QKDJ' || ROW_������¼.RN || '=:8, QKYHLB' ||
               ROW_������¼.RN || '=:9, MZFS' || ROW_������¼.RN || '=:10, MZYS' ||
               ROW_������¼.RN || '=:11 WHERE סԺ������=:12';
    EXECUTE IMMEDIATE STR_SQL
      USING ROW_������¼.������, ROW_������¼.������������, ROW_������¼.�����������, ROW_������¼.������������, ROW_������¼.����, ROW_������¼.I��, ROW_������¼.II��, ROW_������¼.�пڵȼ�, ROW_������¼.�п����ϵȼ�, ROW_������¼.����ʽ����, ROW_������¼.����ҽʦ, ROW_������¼.סԺ������;
  
  END LOOP;

  --�������ݼ�
  OPEN CUR_����_�б���Ϣ FOR
    SELECT T.* FROM ��ʱ��_������ҳ�� T;

END PR_������ҳ_��ȡ��������;
/
