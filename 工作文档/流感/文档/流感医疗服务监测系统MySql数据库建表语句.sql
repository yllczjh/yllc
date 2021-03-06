-- ----------------------------
-- Table structure for flu
-- ----------------------------
DROP TABLE IF EXISTS `flu`;
CREATE TABLE `flu` (
  `Id` int(11) unsigned NOT NULL,
  `P900` varchar(40) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '医疗机构代码',
  `P6891` varchar(80) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '医疗机构名称',
  `P686` varchar(50) DEFAULT NULL COMMENT '医疗保险手册（卡）号',
  `P800` varchar(50) DEFAULT NULL COMMENT '健康卡号',
  `P7501` varchar(60) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '就诊类型',
  `P7502` varchar(60) NOT NULL COMMENT '就诊卡号',
  `P4` varchar(40) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '姓名',
  `P5` varchar(2) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '性别',
  `P6` date DEFAULT NULL COMMENT '出生日期',
  `P7` int(3) DEFAULT NULL COMMENT '年龄（岁）',
  `P7503` varchar(60) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '注册证件类型代码',
  `P13` varchar(80) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '身份证号',
  `P7504` varchar(60) DEFAULT NULL COMMENT '就诊科室代码',
  `P7505` varchar(60) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '就诊次数',
  `P7506` datetime NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '就诊日期',
  `P7507` varchar(600) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '主诉',
  `P321` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '主要诊断编码',
  `P322` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '主要诊断疾病描述',
  `P324` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '其他诊断编码1',
  `P325` varchar(200) DEFAULT NULL COMMENT '其他诊断疾病描述1',
  `P327` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '其他诊断编码2',
  `P328` varchar(200) DEFAULT NULL COMMENT '其他诊断疾病描述2',
  `P3291` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '其他诊断编码3',
  `P3292` varchar(200) DEFAULT NULL COMMENT '其他诊断疾病描述3',
  `P3294` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '其他诊断编码4',
  `P3295` varchar(200) DEFAULT NULL COMMENT '其他诊断疾病描述4',
  `P3297` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '其他诊断编码5',
  `P3298` varchar(200) DEFAULT NULL COMMENT '其他诊断疾病描述5',
  `P3281` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '其他诊断编码6',
  `P3282` varchar(200) DEFAULT NULL COMMENT '其他诊断疾病描述6',
  `P3284` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '其他诊断编码7',
  `P3285` varchar(200) DEFAULT NULL COMMENT '其他诊断疾病描述7',
  `P3287` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '其他诊断编码8',
  `P3288` varchar(200) DEFAULT NULL COMMENT '其他诊断疾病描述8',
  `P3271` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '其他诊断编码9',
  `P3272` varchar(200) DEFAULT NULL COMMENT '其他诊断疾病描述9',
  `P3274` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '其他诊断编码10',
  `P3275` varchar(200) DEFAULT NULL COMMENT '其他诊断疾病描述10',
  `P6911` varchar(4) DEFAULT NULL COMMENT '重症监护室名称1',
  `P6912` datetime DEFAULT NULL COMMENT '进入时间1',
  `P6913` datetime DEFAULT NULL COMMENT '进入时间1',
  `P6914` varchar(4) DEFAULT NULL COMMENT '重症监护室名称2',
  `P6915` datetime DEFAULT NULL COMMENT '进入时间2',
  `P6916` datetime DEFAULT NULL COMMENT '进入时间2',
  `P6917` varchar(4) DEFAULT NULL COMMENT '重症监护室名称3',
  `P6918` datetime DEFAULT NULL COMMENT '进入时间3',
  `P6919` datetime DEFAULT NULL COMMENT '进入时间3',
  `P6920` varchar(4) DEFAULT NULL COMMENT '重症监护室名称4',
  `P6921` datetime DEFAULT NULL COMMENT '进入时间4',
  `P6922` datetime DEFAULT NULL COMMENT '进入时间4',
  `P6923` varchar(4) DEFAULT NULL COMMENT '重症监护室名称5',
  `P6924` datetime DEFAULT NULL COMMENT '进入时间5',
  `P6925` datetime DEFAULT NULL COMMENT '进入时间5',
  `P1` varchar(2) DEFAULT NULL COMMENT '医疗付款方式',
  `P7508` decimal(10,2) DEFAULT NULL COMMENT '总费用',
  `P7509` decimal(10,2) DEFAULT NULL COMMENT '挂号费',
  `P7510` decimal(10,2) DEFAULT NULL COMMENT '药品费',
  `P7511` decimal(10,2) DEFAULT NULL COMMENT '检查费',
  `P7512` decimal(10,2) DEFAULT NULL COMMENT '自付费用',
  `P8508` varchar(2) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '是否死亡',
  `P8509` datetime DEFAULT NULL COMMENT '死亡时间',
  `P4_P1` varchar(40) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `P13_P1` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `P13_P2` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `P13_P3` varchar(2) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC

-- ----------------------------
-- Table structure for hda
-- ----------------------------
DROP TABLE IF EXISTS `hda`;
CREATE TABLE `hda` (
  `Id` int(11) unsigned NOT NULL,
  `P3` varchar(20) NOT NULL COMMENT '病案号',
  `P4` varchar(40) DEFAULT NULL COMMENT '姓名',
  `P5` varchar(2) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '性别',
  `P7` int(3) DEFAULT NULL COMMENT '年龄',
  `P22` datetime NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '入院日期',
  `P23` varchar(6) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '入院科别',
  `P24` varchar(6) DEFAULT NULL COMMENT '转科科别',
  `P25` datetime NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '出院日期',
  `P26` varchar(6) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '出院科别',
  `P27` int(6) DEFAULT NULL COMMENT '实际住院天数',
  `P8600` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '入院诊断',
  `P8601` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '出院诊断',
  `P8602` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '入院情况及诊疗经过',
  `P8603` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '出院情况及治疗结果',
  `P8604` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '出院医嘱',
  PRIMARY KEY (`Id`),
  KEY `mo` (`P3`,`P22`,`P25`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPRESSED

-- ----------------------------
-- Table structure for hdr
-- ----------------------------
DROP TABLE IF EXISTS `hdr`;
CREATE TABLE `hdr` (
  `Id` int(11) unsigned NOT NULL,
  `P3` varchar(20) NOT NULL COMMENT '病案号',
  `P4` varchar(40) DEFAULT NULL COMMENT '姓名',
  `P5` varchar(2) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '性别',
  `P7` int(3) DEFAULT NULL COMMENT '年龄',
  `P22` datetime NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '入院日期',
  `P23` varchar(6) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '入院科别',
  `P24` varchar(6) DEFAULT NULL COMMENT '转科科别',
  `P25` datetime NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '出院日期',
  `P26` varchar(6) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '出院科别',
  `P27` int(6) DEFAULT NULL COMMENT '实际住院天数',
  `P8600` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '入院诊断',
  `P8604` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '入院情况及诊疗和抢救经过',
  `P8605` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '死亡诊断',
  `P8606` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '死亡原因',
  `P8509` datetime DEFAULT NULL COMMENT '死亡时间',
  PRIMARY KEY (`Id`),
  KEY `mo` (`P3`,`P22`,`P25`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPRESSED

-- ----------------------------
-- Table structure for hqms
-- ----------------------------
DROP TABLE IF EXISTS `hqms`;
CREATE TABLE `hqms` (
  `Id` int(11) unsigned NOT NULL,
  `P900` varchar(40) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '医疗机构代码',
  `P6891` varchar(80) DEFAULT NULL COMMENT '机构名称',
  `P686` varchar(50) DEFAULT NULL COMMENT '医疗保险手册（卡）号',
  `p800` varchar(50) DEFAULT NULL COMMENT '健康卡号',
  `P1` varchar(2) DEFAULT NULL COMMENT '医疗付款方式',
  `P2` int(4) DEFAULT NULL COMMENT '住院次数',
  `P3` varchar(20) DEFAULT NULL COMMENT '病案号',
  `P4` varchar(40) DEFAULT NULL COMMENT '姓名',
  `P5` varchar(2) DEFAULT NULL COMMENT '性别',
  `P6` datetime DEFAULT NULL COMMENT '出生日期',
  `P7` int(3) DEFAULT NULL COMMENT '年龄',
  `P8` varchar(2) DEFAULT NULL COMMENT '婚姻状况',
  `P9` varchar(2) DEFAULT NULL COMMENT '职业',
  `P101` varchar(30) DEFAULT NULL COMMENT '出生省份',
  `P102` varchar(30) DEFAULT NULL COMMENT '出生地市',
  `P103` varchar(30) DEFAULT NULL COMMENT '出生地县',
  `P11` varchar(20) DEFAULT NULL COMMENT '民族',
  `P12` varchar(40) DEFAULT NULL COMMENT '国籍',
  `P13` varchar(80) DEFAULT NULL,
  `P801` varchar(200) DEFAULT NULL COMMENT '现住址',
  `P802` varchar(40) DEFAULT NULL COMMENT '住宅电话',
  `P803` varchar(6) DEFAULT NULL COMMENT '现住址邮政编码',
  `P14` varchar(200) DEFAULT NULL COMMENT '工作单位及地址',
  `P15` varchar(40) DEFAULT NULL COMMENT '电话',
  `P16` varchar(6) DEFAULT NULL COMMENT '工作单位邮政编码',
  `P17` varchar(200) DEFAULT NULL COMMENT '户口地址',
  `P171` varchar(6) DEFAULT NULL COMMENT '户口所在地邮政编码',
  `P18` varchar(40) DEFAULT NULL COMMENT '联系人姓名',
  `P19` varchar(40) DEFAULT NULL COMMENT '关系',
  `P20` varchar(200) DEFAULT NULL COMMENT '联系人地址',
  `P804` varchar(2) DEFAULT NULL COMMENT '入院途径',
  `P21` varchar(40) DEFAULT NULL COMMENT '联系人电话',
  `P22` datetime DEFAULT NULL COMMENT '入院日期',
  `P23` varchar(6) DEFAULT NULL COMMENT '入院科别',
  `P231` varchar(30) DEFAULT NULL COMMENT '入院病室',
  `P24` varchar(6) DEFAULT NULL COMMENT '转科科别',
  `P25` datetime DEFAULT NULL COMMENT '出院日期',
  `P26` varchar(6) DEFAULT NULL COMMENT '出院科别',
  `P261` varchar(30) DEFAULT NULL COMMENT '出院病室',
  `P27` int(6) DEFAULT NULL COMMENT '实际住院天数',
  `P28` varchar(20) DEFAULT NULL COMMENT '门（急）诊诊断编码',
  `P281` varchar(100) DEFAULT NULL COMMENT '门（急）诊诊断描述',
  `P29` varchar(2) DEFAULT NULL COMMENT '入院时情况',
  `P30` varchar(30) DEFAULT NULL COMMENT '入院诊断编码',
  `P301` varchar(100) DEFAULT NULL COMMENT '入院诊断描述',
  `P31` datetime DEFAULT NULL COMMENT '入院后确诊日期',
  `P321` varchar(20) DEFAULT NULL COMMENT '主要诊断编码',
  `P322` varchar(100) DEFAULT NULL COMMENT '主要诊断疾病描述',
  `P805` varchar(2) DEFAULT NULL COMMENT '主要诊断入院病情',
  `P323` varchar(2) DEFAULT NULL COMMENT '主要诊断出院情况',
  `P324` varchar(20) DEFAULT NULL COMMENT '其他诊断编码1',
  `P325` varchar(100) DEFAULT NULL COMMENT '其他诊断疾病描述1',
  `P806` varchar(2) DEFAULT NULL COMMENT '其他诊断入院病情1',
  `P326` varchar(2) DEFAULT NULL COMMENT '其他诊断出院情况1',
  `P327` varchar(20) DEFAULT NULL COMMENT '其他诊断编码2',
  `P328` varchar(100) DEFAULT NULL COMMENT '其他诊断疾病描述2',
  `P807` varchar(2) DEFAULT NULL COMMENT '其他诊断入院病情2',
  `P329` varchar(2) DEFAULT NULL COMMENT '其他诊断出院情况2',
  `P3291` varchar(20) DEFAULT NULL COMMENT '其他诊断编码3',
  `P3292` varchar(100) DEFAULT NULL COMMENT '其他诊断疾病描述3',
  `P808` varchar(2) DEFAULT NULL COMMENT '其他诊断入院病情3',
  `P3293` varchar(2) DEFAULT NULL COMMENT '其他诊断出院情况3',
  `P3294` varchar(20) DEFAULT NULL COMMENT '其他诊断编码4',
  `P3295` varchar(100) DEFAULT NULL COMMENT '其他诊断疾病描述4',
  `P809` varchar(2) DEFAULT NULL COMMENT '其他诊断入院病情4',
  `P3296` varchar(2) DEFAULT NULL COMMENT '其他诊断出院情况4',
  `P3297` varchar(20) DEFAULT NULL COMMENT '其他诊断编码5',
  `P3298` varchar(100) DEFAULT NULL COMMENT '其他诊断疾病描述5',
  `P810` varchar(2) DEFAULT NULL COMMENT '其他诊断入院病情5',
  `P3299` varchar(2) DEFAULT NULL COMMENT '其他诊断出院情况5',
  `P3281` varchar(20) DEFAULT NULL COMMENT '其他诊断编码6',
  `P3282` varchar(100) DEFAULT NULL COMMENT '其他诊断疾病描述6',
  `P811` varchar(2) DEFAULT NULL COMMENT '其他诊断入院病情6',
  `P3283` varchar(2) DEFAULT NULL COMMENT '其他诊断出院情况6',
  `P3284` varchar(20) DEFAULT NULL COMMENT '其他诊断编码7',
  `P3285` varchar(100) DEFAULT NULL COMMENT '其他诊断疾病描述7',
  `P812` varchar(2) DEFAULT NULL COMMENT '其他诊断入院病情7',
  `P3286` varchar(2) DEFAULT NULL COMMENT '其他诊断出院情况7',
  `P3287` varchar(20) DEFAULT NULL COMMENT '其他诊断编码8',
  `P3288` varchar(100) DEFAULT NULL COMMENT '其他诊断疾病描述8',
  `P813` varchar(2) DEFAULT NULL COMMENT '其他诊断入院病情8',
  `P3289` varchar(2) DEFAULT NULL COMMENT '其他诊断出院情况8',
  `P3271` varchar(20) DEFAULT NULL COMMENT '其他诊断编码9',
  `P3272` varchar(100) DEFAULT NULL COMMENT '其他诊断疾病描述9',
  `P814` varchar(2) DEFAULT NULL COMMENT '其他诊断入院病情9',
  `P3273` varchar(2) DEFAULT NULL COMMENT '其他诊断出院情况9',
  `P3274` varchar(20) DEFAULT NULL COMMENT '其他诊断编码10',
  `P3275` varchar(100) DEFAULT NULL COMMENT '其他诊断疾病描述10',
  `P815` varchar(2) DEFAULT NULL COMMENT '其他诊断入院病情10',
  `P3276` varchar(2) DEFAULT NULL COMMENT '其他诊断出院情况10',
  `P689` int(5) DEFAULT NULL COMMENT '医院感染总次数',
  `P351` varchar(20) DEFAULT NULL COMMENT '病理诊断编码1',
  `P352` varchar(100) DEFAULT NULL COMMENT '病理诊断名称1',
  `P816` varchar(50) DEFAULT NULL COMMENT '病理号1',
  `P353` varchar(20) DEFAULT NULL COMMENT '病理诊断编码2',
  `P354` varchar(100) DEFAULT NULL COMMENT '病理诊断名称2',
  `P817` varchar(50) DEFAULT NULL COMMENT '病理号2',
  `P355` varchar(20) DEFAULT NULL COMMENT '病理诊断编码3',
  `P356` varchar(100) DEFAULT NULL COMMENT '病理诊断名称3',
  `P818` varchar(50) DEFAULT NULL COMMENT '病理号3',
  `P361` varchar(20) DEFAULT NULL COMMENT '损伤、中毒的外部因素编码1',
  `P362` varchar(100) DEFAULT NULL COMMENT '损伤、中毒的外部因素名称1',
  `P363` varchar(20) DEFAULT NULL COMMENT '损伤、中毒的外部因素编码2',
  `P364` varchar(100) DEFAULT NULL COMMENT '损伤、中毒的外部因素名称2',
  `P365` varchar(20) DEFAULT NULL COMMENT '损伤、中毒的外部因素编码3',
  `P366` varchar(100) DEFAULT NULL COMMENT '损伤、中毒的外部因素名称3',
  `P371` varchar(200) DEFAULT NULL COMMENT '过敏源',
  `P372` varchar(100) DEFAULT NULL COMMENT '过敏药物名称',
  `P38` varchar(2) DEFAULT NULL COMMENT 'HBsAg',
  `P39` varchar(2) DEFAULT NULL COMMENT 'HCV-Ab',
  `P40` varchar(2) DEFAULT NULL COMMENT 'HIV-Ab',
  `P411` varchar(2) DEFAULT NULL COMMENT '门诊与出院诊断符合情况',
  `P412` varchar(2) DEFAULT NULL COMMENT '入院与出院诊断符合情况',
  `P413` varchar(2) DEFAULT NULL COMMENT '术前与术后诊断符合情况',
  `P414` varchar(2) DEFAULT NULL COMMENT '临床与病理诊断符合情况',
  `P415` varchar(2) DEFAULT NULL COMMENT '放射与病理诊断符合情况',
  `P421` int(3) DEFAULT NULL COMMENT '抢救次数',
  `P422` int(3) DEFAULT NULL COMMENT '抢救成功次数',
  `P687` varchar(2) DEFAULT NULL COMMENT '最高诊断依据',
  `P688` varchar(2) DEFAULT NULL COMMENT '分化程度',
  `P431` varchar(40) DEFAULT NULL COMMENT '科主任',
  `P432` varchar(40) DEFAULT NULL COMMENT '主(副主)任医师',
  `P433` varchar(40) DEFAULT NULL COMMENT '主治医师',
  `P434` varchar(40) DEFAULT NULL COMMENT '住院医师',
  `P819` varchar(40) DEFAULT NULL COMMENT '责任护士',
  `P435` varchar(40) DEFAULT NULL COMMENT '进修医师',
  `P436` varchar(40) DEFAULT NULL COMMENT '研究生实习医师',
  `P437` varchar(40) DEFAULT NULL COMMENT '实习医师',
  `P438` varchar(40) DEFAULT NULL COMMENT '编码员',
  `P44` varchar(2) DEFAULT NULL COMMENT '病案质量',
  `P45` varchar(40) DEFAULT NULL COMMENT '质控医师',
  `P46` varchar(40) DEFAULT NULL COMMENT '质控护师',
  `P47` datetime DEFAULT NULL COMMENT '质控日期',
  `P490` varchar(20) DEFAULT NULL COMMENT '手术/操作编码1',
  `P491` datetime DEFAULT NULL COMMENT '手术/操作日期1',
  `P820` varchar(2) DEFAULT NULL COMMENT '手术级别1',
  `P492` varchar(100) DEFAULT NULL COMMENT '手术/操作名称1',
  `P493` varchar(100) DEFAULT NULL COMMENT '手术/操作部位1',
  `p494` decimal(7,2) DEFAULT NULL COMMENT '手术持续时间1',
  `P495` varchar(40) DEFAULT NULL COMMENT '术者1',
  `P496` varchar(40) DEFAULT NULL COMMENT 'Ⅰ助1',
  `P497` varchar(40) DEFAULT NULL COMMENT 'Ⅱ助1',
  `P498` varchar(6) DEFAULT NULL COMMENT '麻醉方式1',
  `P4981` varchar(2) DEFAULT NULL COMMENT '麻醉分级1',
  `P499` varchar(2) DEFAULT NULL COMMENT '切口愈合等级1',
  `P4910` varchar(40) DEFAULT NULL COMMENT '麻醉医师1',
  `P4911` varchar(20) DEFAULT NULL COMMENT '手术/操作编码2',
  `P4912` datetime DEFAULT NULL COMMENT '手术/操作日期2',
  `P821` varchar(2) DEFAULT NULL COMMENT '手术级别2',
  `P4913` varchar(100) DEFAULT NULL COMMENT '手术/操作名称2',
  `P4914` varchar(100) DEFAULT NULL COMMENT '手术/操作部位2',
  `P4915` decimal(7,2) DEFAULT NULL COMMENT '手术持续时间2',
  `P4916` varchar(40) DEFAULT NULL COMMENT '术者2',
  `P4917` varchar(40) DEFAULT NULL COMMENT 'Ⅰ助2',
  `P4918` varchar(40) DEFAULT NULL COMMENT 'Ⅱ助2',
  `P4919` varchar(6) DEFAULT NULL COMMENT '麻醉方式2',
  `P4982` varchar(2) DEFAULT NULL COMMENT '麻醉分级2',
  `P4920` varchar(2) DEFAULT NULL COMMENT '切口愈合等级2',
  `P4921` varchar(40) DEFAULT NULL COMMENT '麻醉医师2',
  `P4922` varchar(20) DEFAULT NULL COMMENT '手术/操作编码3',
  `P4923` datetime DEFAULT NULL COMMENT '手术/操作日期3',
  `P822` varchar(2) DEFAULT NULL COMMENT '手术级别3',
  `P4924` varchar(100) DEFAULT NULL COMMENT '手术/操作名称3',
  `P4925` varchar(100) DEFAULT NULL COMMENT '手术/操作部位3',
  `P4526` decimal(7,2) DEFAULT NULL COMMENT '手术持续时间3',
  `P4527` varchar(40) DEFAULT NULL COMMENT '术者3',
  `P4528` varchar(40) DEFAULT NULL COMMENT 'Ⅰ助3',
  `P4529` varchar(40) DEFAULT NULL COMMENT 'Ⅱ助3',
  `P4530` varchar(6) DEFAULT NULL COMMENT '麻醉方式3',
  `P4983` varchar(2) DEFAULT NULL COMMENT '麻醉分级3',
  `P4531` varchar(2) DEFAULT NULL COMMENT '切口愈合等级3',
  `P4532` varchar(40) DEFAULT NULL COMMENT '麻醉医师3',
  `P4533` varchar(20) DEFAULT NULL COMMENT '手术/操作编码4',
  `P4534` datetime DEFAULT NULL COMMENT '手术/操作日期4',
  `P823` varchar(2) DEFAULT NULL COMMENT '手术级别4',
  `P4535` varchar(100) DEFAULT NULL COMMENT '手术/操作名称4',
  `P4536` varchar(100) DEFAULT NULL COMMENT '手术/操作部位4',
  `P4537` decimal(7,2) DEFAULT NULL COMMENT '手术持续时间4',
  `P4538` varchar(40) DEFAULT NULL COMMENT '术者4',
  `P4539` varchar(40) DEFAULT NULL COMMENT 'Ⅰ助4',
  `P4540` varchar(40) DEFAULT NULL COMMENT 'Ⅱ助4',
  `P4541` varchar(6) DEFAULT NULL COMMENT '麻醉方式4',
  `P4984` varchar(2) DEFAULT NULL COMMENT '麻醉分级4',
  `P4542` varchar(2) DEFAULT NULL COMMENT '切口愈合等级4',
  `P4543` varchar(40) DEFAULT NULL COMMENT '麻醉医师4',
  `P4544` varchar(20) DEFAULT NULL COMMENT '手术/操作编码5',
  `P4545` datetime DEFAULT NULL COMMENT '手术/操作日期5',
  `P824` varchar(2) DEFAULT NULL COMMENT '手术级别5',
  `P4546` varchar(100) DEFAULT NULL COMMENT '手术/操作名称5',
  `P4547` varchar(100) DEFAULT NULL COMMENT '手术/操作部位5',
  `P4548` decimal(7,2) DEFAULT NULL COMMENT '手术持续时间5',
  `P4549` varchar(40) DEFAULT NULL COMMENT '术者5',
  `P4550` varchar(40) DEFAULT NULL COMMENT 'Ⅰ助5',
  `P4551` varchar(40) DEFAULT NULL COMMENT 'Ⅱ助5',
  `P4552` varchar(6) DEFAULT NULL COMMENT '麻醉方式5',
  `P4985` varchar(2) DEFAULT NULL COMMENT '麻醉分级5',
  `P4553` varchar(2) DEFAULT NULL COMMENT '切口愈合等级5',
  `P4554` varchar(40) DEFAULT NULL COMMENT '麻醉医师5',
  `P45002` varchar(20) DEFAULT NULL COMMENT '手术/操作编码6',
  `P45003` datetime DEFAULT NULL COMMENT '手术/操作日期6',
  `P825` varchar(2) DEFAULT NULL COMMENT '手术级别6',
  `P45004` varchar(100) DEFAULT NULL COMMENT '手术/操作名称6',
  `P45005` varchar(100) DEFAULT NULL COMMENT '手术/操作部位6',
  `p45006` decimal(7,2) DEFAULT NULL COMMENT '手术持续时间6',
  `P45007` varchar(40) DEFAULT NULL COMMENT '术者6',
  `P45008` varchar(40) DEFAULT NULL COMMENT 'Ⅰ助6',
  `P45009` varchar(40) DEFAULT NULL COMMENT 'Ⅱ助6',
  `P45010` varchar(6) DEFAULT NULL COMMENT '麻醉方式6',
  `P45011` varchar(2) DEFAULT NULL COMMENT '麻醉分级6',
  `P45012` varchar(2) DEFAULT NULL COMMENT '切口愈合等级6',
  `P45013` varchar(40) DEFAULT NULL COMMENT '麻醉医师6',
  `P45014` varchar(20) DEFAULT NULL COMMENT '手术/操作编码7',
  `P45015` datetime DEFAULT NULL COMMENT '手术/操作日期7',
  `P826` varchar(2) DEFAULT NULL COMMENT '手术级别7',
  `P45016` varchar(100) DEFAULT NULL COMMENT '手术/操作名称7',
  `P45017` varchar(100) DEFAULT NULL COMMENT '手术/操作部位7',
  `p45018` decimal(7,2) DEFAULT NULL COMMENT '手术持续时间7',
  `P45019` varchar(40) DEFAULT NULL COMMENT '术者7',
  `P45020` varchar(40) DEFAULT NULL COMMENT 'Ⅰ助7',
  `P45021` varchar(40) DEFAULT NULL COMMENT 'Ⅱ助7',
  `P45022` varchar(6) DEFAULT NULL COMMENT '麻醉方式7',
  `P45023` varchar(2) DEFAULT NULL COMMENT '麻醉分级7',
  `P45024` varchar(2) DEFAULT NULL COMMENT '切口愈合等级7',
  `P45025` varchar(40) DEFAULT NULL COMMENT '麻醉医师7',
  `P45026` varchar(20) DEFAULT NULL COMMENT '手术/操作编码8',
  `P45027` datetime DEFAULT NULL COMMENT '手术/操作日期8',
  `P827` varchar(2) DEFAULT NULL COMMENT '手术级别8',
  `P45028` varchar(100) DEFAULT NULL COMMENT '手术/操作名称8',
  `P45029` varchar(100) DEFAULT NULL COMMENT '手术/操作部位8',
  `p45030` decimal(7,2) DEFAULT NULL COMMENT '手术持续时间8',
  `P45031` varchar(40) DEFAULT NULL COMMENT '术者8',
  `P45032` varchar(40) DEFAULT NULL COMMENT 'Ⅰ助8',
  `P45033` varchar(40) DEFAULT NULL COMMENT 'Ⅱ助8',
  `P45034` varchar(6) DEFAULT NULL COMMENT '麻醉方式8',
  `P45035` varchar(2) DEFAULT NULL COMMENT '麻醉分级8',
  `P45036` varchar(2) DEFAULT NULL COMMENT '切口愈合等级8',
  `P45037` varchar(40) DEFAULT NULL COMMENT '麻醉医师8',
  `P45038` varchar(20) DEFAULT NULL COMMENT '手术/操作编码9',
  `P45039` datetime DEFAULT NULL COMMENT '手术/操作日期9',
  `P828` varchar(2) DEFAULT NULL COMMENT '手术级别9',
  `P45040` varchar(100) DEFAULT NULL COMMENT '手术/操作名称9',
  `P45041` varchar(100) DEFAULT NULL COMMENT '手术/操作部位9',
  `p45042` decimal(7,2) DEFAULT NULL COMMENT '手术持续时间9',
  `P45043` varchar(40) DEFAULT NULL COMMENT '术者9',
  `P45044` varchar(40) DEFAULT NULL COMMENT 'Ⅰ助9',
  `P45045` varchar(40) DEFAULT NULL COMMENT 'Ⅱ助9',
  `P45046` varchar(6) DEFAULT NULL COMMENT '麻醉方式9',
  `P45047` varchar(2) DEFAULT NULL COMMENT '麻醉分级9',
  `P45048` varchar(2) DEFAULT NULL COMMENT '切口愈合等级9',
  `P45049` varchar(40) DEFAULT NULL COMMENT '麻醉医师9',
  `P45050` varchar(20) DEFAULT NULL COMMENT '手术/操作编码10',
  `P45051` datetime DEFAULT NULL COMMENT '手术/操作日期10',
  `P829` varchar(2) DEFAULT NULL COMMENT '手术级别10',
  `P45052` varchar(100) DEFAULT NULL COMMENT '手术/操作名称10',
  `P45053` varchar(100) DEFAULT NULL COMMENT '手术/操作部位10',
  `p45054` decimal(7,2) DEFAULT NULL COMMENT '手术持续时间10',
  `P45055` varchar(40) DEFAULT NULL COMMENT '术者10',
  `P45056` varchar(40) DEFAULT NULL COMMENT 'Ⅰ助10',
  `P45057` varchar(40) DEFAULT NULL COMMENT 'Ⅱ助10',
  `P45058` varchar(6) DEFAULT NULL COMMENT '麻醉方式10',
  `P45059` varchar(2) DEFAULT NULL COMMENT '麻醉分级10',
  `P45060` varchar(2) DEFAULT NULL COMMENT '切口愈合等级10',
  `P45061` varchar(40) DEFAULT NULL COMMENT '麻醉医师10',
  `P561` int(6) DEFAULT NULL COMMENT '特级护理天数',
  `P562` int(6) DEFAULT NULL COMMENT '一级护理天数',
  `P563` int(6) DEFAULT NULL COMMENT '二级护理天数',
  `P564` int(6) DEFAULT NULL COMMENT '三级护理天数',
  `P6911` varchar(4) DEFAULT NULL COMMENT '重症监护室名称1',
  `P6912` datetime DEFAULT NULL COMMENT '进入时间1',
  `P6913` datetime DEFAULT NULL COMMENT '退出时间1',
  `P6914` varchar(4) DEFAULT NULL COMMENT '重症监护室名称2',
  `P6915` datetime DEFAULT NULL COMMENT '进入时间2',
  `P6916` datetime DEFAULT NULL COMMENT '退出时间2',
  `P6917` varchar(4) DEFAULT NULL COMMENT '重症监护室名称3',
  `P6918` datetime DEFAULT NULL COMMENT '进入时间3',
  `P6919` datetime DEFAULT NULL COMMENT '退出时间3',
  `P6920` varchar(4) DEFAULT NULL COMMENT '重症监护室名称4',
  `P6921` datetime DEFAULT NULL COMMENT '进入时间4',
  `P6922` datetime DEFAULT NULL COMMENT '退出时间4',
  `P6923` varchar(4) DEFAULT NULL COMMENT '重症监护室名称5',
  `P6924` datetime DEFAULT NULL COMMENT '进入时间5',
  `P6925` datetime DEFAULT NULL COMMENT '退出时间5',
  `P57` varchar(2) DEFAULT NULL COMMENT '死亡患者尸检',
  `P58` varchar(2) DEFAULT NULL COMMENT '手术、治疗、检查、诊断为本院第一例',
  `P581` varchar(10) DEFAULT NULL COMMENT '手术患者类型',
  `P60` varchar(2) DEFAULT NULL COMMENT '随诊',
  `p611` decimal(4,2) DEFAULT NULL COMMENT '随诊周数',
  `p612` decimal(4,2) DEFAULT NULL COMMENT '随诊月数',
  `p613` decimal(4,2) DEFAULT NULL COMMENT '随诊年数',
  `P59` varchar(2) DEFAULT NULL COMMENT '示教病例',
  `P62` varchar(2) DEFAULT NULL COMMENT 'ABO血型',
  `P63` varchar(2) DEFAULT NULL COMMENT 'Rh血型',
  `P64` varchar(2) DEFAULT NULL COMMENT '输血反应',
  `p651` decimal(8,2) DEFAULT NULL COMMENT '红细胞',
  `p652` decimal(8,2) DEFAULT NULL COMMENT '血小板',
  `P653` int(11) DEFAULT NULL COMMENT '血浆',
  `P654` int(11) DEFAULT NULL COMMENT '全血',
  `P655` int(11) DEFAULT NULL COMMENT '自体回收',
  `P656` int(11) DEFAULT NULL COMMENT '其它',
  `p66` decimal(4,2) DEFAULT NULL COMMENT '（婴幼儿）年龄',
  `P681` int(11) DEFAULT NULL COMMENT '新生儿出生体重1',
  `P682` int(11) DEFAULT NULL COMMENT '新生儿出生体重2',
  `P683` int(11) DEFAULT NULL COMMENT '新生儿出生体重3',
  `P684` int(11) DEFAULT NULL COMMENT '新生儿出生体重4',
  `P685` int(11) DEFAULT NULL COMMENT '新生儿出生体重5',
  `P67` int(11) DEFAULT NULL COMMENT '新生儿入院体重',
  `P731` int(11) DEFAULT NULL COMMENT '入院前多少小时(昏迷时间)',
  `P732` int(11) DEFAULT NULL COMMENT '入院前多少分钟(昏迷时间)',
  `P733` int(11) DEFAULT NULL COMMENT '入院后多少小时(昏迷时间)',
  `P734` int(11) DEFAULT NULL COMMENT '入院后多少分钟(昏迷时间)',
  `P72` int(11) DEFAULT NULL COMMENT '呼吸机使用时间',
  `P830` varchar(2) DEFAULT NULL COMMENT '是否有出院31天内再住院计划',
  `P831` varchar(100) DEFAULT NULL COMMENT '出院31天再住院计划目的',
  `P741` varchar(2) DEFAULT NULL COMMENT '离院方式',
  `P742` varchar(100) DEFAULT NULL COMMENT '转入医院名称',
  `P743` varchar(100) DEFAULT NULL COMMENT '社区服务机构名称',
  `P782` decimal(10,2) DEFAULT NULL COMMENT '住院总费用',
  `P751` decimal(10,2) DEFAULT NULL COMMENT '住院总费用其中自付金额',
  `P752` decimal(10,2) DEFAULT NULL COMMENT '一般医疗服务费',
  `P754` decimal(10,2) DEFAULT NULL COMMENT '一般治疗操作费',
  `P755` decimal(10,2) DEFAULT NULL COMMENT '护理费',
  `P756` decimal(10,2) DEFAULT NULL COMMENT '综合医疗服务类其他费用',
  `P757` decimal(10,2) DEFAULT NULL COMMENT '病理诊断费',
  `P758` decimal(10,2) DEFAULT NULL COMMENT '实验室诊断费',
  `P759` decimal(10,2) DEFAULT NULL COMMENT '影像学诊断费',
  `P760` decimal(10,2) DEFAULT NULL COMMENT '临床诊断项目费',
  `P761` decimal(10,2) DEFAULT NULL COMMENT '非手术治疗项目费',
  `P762` decimal(10,2) DEFAULT NULL COMMENT '临床物理治疗费',
  `P763` decimal(10,2) DEFAULT NULL COMMENT '手术治疗费',
  `P764` decimal(10,2) DEFAULT NULL COMMENT '麻醉费',
  `P765` decimal(10,2) DEFAULT NULL COMMENT '手术费',
  `P767` decimal(10,2) DEFAULT NULL COMMENT '康复费',
  `P768` decimal(10,2) DEFAULT NULL COMMENT '中医治疗费',
  `P769` decimal(10,2) DEFAULT NULL COMMENT '西药费',
  `P770` decimal(10,2) DEFAULT NULL COMMENT '抗菌药物费用',
  `P771` decimal(10,2) DEFAULT NULL COMMENT '中成药费',
  `P772` decimal(10,2) DEFAULT NULL COMMENT '中草药费',
  `P773` decimal(10,2) DEFAULT NULL COMMENT '血费',
  `P774` decimal(10,2) DEFAULT NULL COMMENT '白蛋白类制品费',
  `P775` decimal(10,2) DEFAULT NULL COMMENT '球蛋白类制品费',
  `P776` decimal(10,2) DEFAULT NULL COMMENT '凝血因子类制品费',
  `P777` decimal(10,2) DEFAULT NULL COMMENT '细胞因子类制品费',
  `P778` decimal(10,2) DEFAULT NULL COMMENT '检查用一次性医用材料费',
  `P779` decimal(10,2) DEFAULT NULL COMMENT '治疗用一次性医用材料费',
  `P780` decimal(10,2) DEFAULT NULL COMMENT '手术用一次性医用材料费',
  `P781` decimal(10,2) DEFAULT NULL COMMENT '其他费',
  `P4_P1` varchar(40) DEFAULT NULL,
  `P13_P1` varchar(20) DEFAULT NULL,
  `P13_P2` varchar(20) DEFAULT NULL,
  `P13_P3` varchar(2) DEFAULT NULL,
  `P802_P1` varchar(20) DEFAULT NULL,
  `P802_P2` varchar(20) DEFAULT NULL,
  `P802_P3` varchar(2) DEFAULT NULL,
  `P802_P4` varchar(40) DEFAULT NULL,
  `P15_P1` varchar(20) DEFAULT NULL,
  `P15_P2` varchar(20) DEFAULT NULL,
  `P15_P3` varchar(2) DEFAULT NULL,
  `P15_P4` varchar(40) DEFAULT NULL,
  `P21_P1` varchar(20) DEFAULT NULL,
  `P21_P2` varchar(20) DEFAULT NULL,
  `P21_P3` varchar(2) DEFAULT NULL,
  `P21_P4` varchar(40) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `mo` (`P3`,`P22`,`P25`)
 ) ENGINE=MyISAM DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;

-- ----------------------------
-- Table structure for lis
-- ----------------------------
DROP TABLE IF EXISTS `lis`;
CREATE TABLE `lis` (
  `Id` int(11) unsigned NOT NULL,
  `P7501` varchar(60) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '就诊类型',
  `P7502` varchar(60) NOT NULL COMMENT '就诊卡号',
  `P7506` datetime NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '就诊日期',
  `P8000` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '标本号',
  `P8001` int(6) DEFAULT NULL COMMENT '检验',
  `P8002` datetime DEFAULT NULL COMMENT '送检时间',
  `P8003` varchar(200) DEFAULT NULL COMMENT '检验结果描述',
  `P8004` varchar(60) DEFAULT NULL COMMENT '检验结果是否阳性',
  `P8005` int(6) DEFAULT NULL COMMENT '检测结果阳性类别',
  PRIMARY KEY (`Id`),
  KEY `dis_index` (`P7502`,`P7506`,`P8000`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC

-- ----------------------------
-- Table structure for pdr
-- ----------------------------
DROP TABLE IF EXISTS `pdr`;
CREATE TABLE `pdr` (
  `Id` int(11) unsigned NOT NULL,
  `P7501` varchar(60) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '就诊类型',
  `P7502` varchar(60) NOT NULL COMMENT '就诊卡号',
  `P7506` datetime NOT NULL DEFAULT '1900-01-01 00:00:00' COMMENT '就诊日期',
  `P7500` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '顺序号',
  `P8016` varchar(50) DEFAULT NULL COMMENT '药物名称',
  `P8017` double(10,4) DEFAULT NULL COMMENT '药物使用频率（日次数）',
  `P8018` double(12,4) DEFAULT NULL COMMENT '药物使用总剂量',
  `P8019` double(12,4) DEFAULT NULL COMMENT '药物使用次剂量',
  `P8020` varchar(60) DEFAULT NULL COMMENT '药物使用剂量单位',
  `P8021` datetime DEFAULT NULL COMMENT '药物使用开始时间',
  `P8022` datetime DEFAULT NULL COMMENT '药物使用结束时间',
  PRIMARY KEY (`Id`),
  KEY `dis_index` (`P7502`,`P7506`,`P7500`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC

