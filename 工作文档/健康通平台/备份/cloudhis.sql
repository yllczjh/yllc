prompt PL/SQL Developer Export User Objects for user CLOUDHIS@47.104.4.221:9900/YKEY
prompt Created by syyyhl on 2020-06-12
set define off
spool cloudhis.log

prompt
prompt Creating table ������ͨ_������־
prompt ========================
prompt
create table CLOUDHIS.������ͨ_������־
(
  ��ˮ��  VARCHAR2(50) not null,
  ƽ̨��ʶ VARCHAR2(50),
  ҽԺ���� VARCHAR2(50),
  ���ܱ��� VARCHAR2(50),
  ������� VARCHAR2(4000),
  ����ʱ�� DATE,
  ����ֵ  NUMBER,
  ������Ϣ VARCHAR2(50),
  ִ��ʱ�� DATE
)
tablespace CLOUDHIS
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
comment on column CLOUDHIS.������ͨ_������־.��ˮ��
  is '��ˮ��';
comment on column CLOUDHIS.������ͨ_������־.ƽ̨��ʶ
  is 'ƽ̨��ʶ';
comment on column CLOUDHIS.������ͨ_������־.ҽԺ����
  is 'ҽԺ����';
comment on column CLOUDHIS.������ͨ_������־.���ܱ���
  is '���ܱ���';
comment on column CLOUDHIS.������ͨ_������־.�������
  is '�������';
comment on column CLOUDHIS.������ͨ_������־.����ʱ��
  is '����ʱ��';
comment on column CLOUDHIS.������ͨ_������־.����ֵ
  is '����ֵ';
comment on column CLOUDHIS.������ͨ_������־.������Ϣ
  is '������Ϣ';
comment on column CLOUDHIS.������ͨ_������־.ִ��ʱ��
  is 'ִ��ʱ��';
alter table CLOUDHIS.������ͨ_������־
  add constraint PK_������ͨ_������־ primary key (��ˮ��)
  using index 
  tablespace CLOUDHIS
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

prompt
prompt Creating table ������ͨ_����
prompt ======================
prompt
create table CLOUDHIS.������ͨ_����
(
  ��ˮ��     NUMBER not null,
  ƽ̨��ʶ    VARCHAR2(50),
  ҽԺ����    VARCHAR2(50),
  ����id    VARCHAR2(50),
  ���ﲡ����   VARCHAR2(50),
  ��������    VARCHAR2(50),
  ����״̬    VARCHAR2(50),
  ҽԺ������   VARCHAR2(50),
  ƽ̨������   VARCHAR2(50),
  ƽ̨����ʱ��  DATE,
  �Һŷ���    NUMBER(10,4),
  ���Ʒ���    NUMBER(10,4),
  �Һ�����    VARCHAR2(50),
  �Һ�����    VARCHAR2(50),
  ԤԼ�Һ�����  VARCHAR2(50),
  ҽԺ֧����   VARCHAR2(50),
  ƽ̨֧����   VARCHAR2(50),
  ƽ̨������ˮ�� VARCHAR2(50),
  ƽ̨����ʱ��  DATE,
  ֧������    VARCHAR2(50),
  �ܽ��     NUMBER(10,4),
  Ӧ�����    NUMBER(10,4),
  ʵ�����    NUMBER(10,4),
  ҽԺ�˿��   VARCHAR2(50),
  ƽ̨�˿��   VARCHAR2(50),
  ƽ̨�˿���ˮ�� VARCHAR2(50),
  ƽ̨�˿�ʱ��  DATE,
  �˿���    NUMBER(10,4),
  �˿�ԭ��    VARCHAR2(100),
  �˿��־    VARCHAR2(50),
  ��������    VARCHAR2(50),
  ����ʱ��    DATE,
  ȡ��ʱ��    DATE,
  ȡ��ԭ��    VARCHAR2(1000),
  ������     VARCHAR2(50),
  ����ʱ��    DATE,
  ������     VARCHAR2(50),
  ����ʱ��    DATE,
  �����     INTEGER,
  ��ע      VARCHAR2(4000),
  ״̬      VARCHAR2(50)
)
tablespace CLOUDHIS
  pctfree 10
  initrans 1
  maxtrans 255;
comment on table CLOUDHIS.������ͨ_����
  is '������ͨ_����';
comment on column CLOUDHIS.������ͨ_����.��ˮ��
  is '��ˮ��';
comment on column CLOUDHIS.������ͨ_����.ƽ̨��ʶ
  is 'ƽ̨��ʶ';
comment on column CLOUDHIS.������ͨ_����.ҽԺ����
  is 'ҽԺ����';
comment on column CLOUDHIS.������ͨ_����.����id
  is '����ID';
comment on column CLOUDHIS.������ͨ_����.���ﲡ����
  is '���ﲡ����';
comment on column CLOUDHIS.������ͨ_����.��������
  is 'ԤԼ�Һű������ֵ';
comment on column CLOUDHIS.������ͨ_����.����״̬
  is '��֧������֧�������˿��ȡ������ɾ����';
comment on column CLOUDHIS.������ͨ_����.ҽԺ������
  is 'ҽԺ������';
comment on column CLOUDHIS.������ͨ_����.ƽ̨������
  is 'ƽ̨������';
comment on column CLOUDHIS.������ͨ_����.ƽ̨����ʱ��
  is 'ƽ̨����ʱ��';
comment on column CLOUDHIS.������ͨ_����.�Һŷ���
  is '�Һŷ���';
comment on column CLOUDHIS.������ͨ_����.���Ʒ���
  is '���Ʒ���';
comment on column CLOUDHIS.������ͨ_����.�Һ�����
  is '1���ˣ�2��Ů��3����';
comment on column CLOUDHIS.������ͨ_����.�Һ�����
  is '�Һ�����';
comment on column CLOUDHIS.������ͨ_����.ԤԼ�Һ�����
  is '1����Һţ�2ԤԼ�Һţ�3���ŹҺ�';
comment on column CLOUDHIS.������ͨ_����.ҽԺ֧����
  is 'ҽԺ֧����';
comment on column CLOUDHIS.������ͨ_����.ƽ̨֧����
  is 'ƽ̨֧����';
comment on column CLOUDHIS.������ͨ_����.ƽ̨������ˮ��
  is 'ƽ̨������ˮ��';
comment on column CLOUDHIS.������ͨ_����.ƽ̨����ʱ��
  is 'ƽ̨����ʱ��';
comment on column CLOUDHIS.������ͨ_����.֧������
  is '֧������';
comment on column CLOUDHIS.������ͨ_����.�ܽ��
  is '�ܽ��';
comment on column CLOUDHIS.������ͨ_����.Ӧ�����
  is 'Ӧ�����';
comment on column CLOUDHIS.������ͨ_����.ʵ�����
  is 'ʵ�����';
comment on column CLOUDHIS.������ͨ_����.ҽԺ�˿��
  is 'ҽԺ�˿��';
comment on column CLOUDHIS.������ͨ_����.ƽ̨�˿��
  is 'ƽ̨�˿��';
comment on column CLOUDHIS.������ͨ_����.ƽ̨�˿���ˮ��
  is 'ƽ̨�˿���ˮ��';
comment on column CLOUDHIS.������ͨ_����.ƽ̨�˿�ʱ��
  is 'ƽ̨�˿�ʱ��';
comment on column CLOUDHIS.������ͨ_����.�˿���
  is '�˿���';
comment on column CLOUDHIS.������ͨ_����.�˿�ԭ��
  is '�˿�ԭ��';
comment on column CLOUDHIS.������ͨ_����.�˿��־
  is '0ʧ�ܣ�1�ҷ��˿2Ժ���˿�';
comment on column CLOUDHIS.������ͨ_����.��������
  is 'ԤԼ�Һţ�ԤԼ�˺ţ������շѣ�Ԥ��Ѻ�𣻳�Ժ����';
comment on column CLOUDHIS.������ͨ_����.����ʱ��
  is '����ʱ��';
comment on column CLOUDHIS.������ͨ_����.ȡ��ʱ��
  is 'ȡ��ʱ��';
comment on column CLOUDHIS.������ͨ_����.ȡ��ԭ��
  is 'ȡ��ԭ��';
comment on column CLOUDHIS.������ͨ_����.������
  is '������';
comment on column CLOUDHIS.������ͨ_����.����ʱ��
  is '����ʱ��';
comment on column CLOUDHIS.������ͨ_����.������
  is '������';
comment on column CLOUDHIS.������ͨ_����.����ʱ��
  is '����ʱ��';
comment on column CLOUDHIS.������ͨ_����.�����
  is '�����';
comment on column CLOUDHIS.������ͨ_����.��ע
  is '��ע';
comment on column CLOUDHIS.������ͨ_����.״̬
  is '״̬';
alter table CLOUDHIS.������ͨ_����
  add constraint PK_������ͨ_���� primary key (��ˮ��)
  using index 
  tablespace CLOUDHIS
  pctfree 10
  initrans 2
  maxtrans 255;

prompt
prompt Creating table ������ͨ_������ϸ
prompt ========================
prompt
create table CLOUDHIS.������ͨ_������ϸ
(
  ��ˮ��  NUMBER not null,
  ������� VARCHAR2(50),
  С����� VARCHAR2(50),
  ��Ŀ���� VARCHAR2(50),
  ��Ŀ���� VARCHAR2(100),
  ���   VARCHAR2(100),
  ���κ�  VARCHAR2(50),
  ����   NUMBER(10,4),
  ��λ   VARCHAR2(50),
  ����   NUMBER(10,4),
  �ܽ��  NUMBER(10,4),
  ������� VARCHAR2(50),
  Ψһ���� VARCHAR2(50),
  ������  VARCHAR2(50)
)
tablespace CLOUDHIS
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
comment on column CLOUDHIS.������ͨ_������ϸ.��ˮ��
  is '��ˮ��';
comment on column CLOUDHIS.������ͨ_������ϸ.�������
  is '�������';
comment on column CLOUDHIS.������ͨ_������ϸ.С�����
  is 'С�����';
comment on column CLOUDHIS.������ͨ_������ϸ.��Ŀ����
  is '��Ŀ����';
comment on column CLOUDHIS.������ͨ_������ϸ.��Ŀ����
  is '��Ŀ����';
comment on column CLOUDHIS.������ͨ_������ϸ.���
  is '���';
comment on column CLOUDHIS.������ͨ_������ϸ.���κ�
  is '���κ�';
comment on column CLOUDHIS.������ͨ_������ϸ.����
  is '����';
comment on column CLOUDHIS.������ͨ_������ϸ.��λ
  is '��λ';
comment on column CLOUDHIS.������ͨ_������ϸ.����
  is '����';
comment on column CLOUDHIS.������ͨ_������ϸ.�ܽ��
  is '�ܽ��';
comment on column CLOUDHIS.������ͨ_������ϸ.�������
  is '�������';
comment on column CLOUDHIS.������ͨ_������ϸ.Ψһ����
  is 'Ψһ����';
comment on column CLOUDHIS.������ͨ_������ϸ.������
  is '������';
alter table CLOUDHIS.������ͨ_������ϸ
  add constraint PK_������ͨ_������ϸ primary key (��ˮ��)
  using index 
  tablespace CLOUDHIS
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

prompt
prompt Creating table ������ͨ_�û���Ϣ
prompt ========================
prompt
create table CLOUDHIS.������ͨ_�û���Ϣ
(
  ��ˮ��     VARCHAR2(50) not null,
  ҽԺ����    VARCHAR2(50),
  ƽ̨��ʶ    VARCHAR2(50),
  ����id    VARCHAR2(50),
  �û����    VARCHAR2(50),
  ����      VARCHAR2(50),
  �Ա�      VARCHAR2(50),
  ��������    DATE,
  ֤������    VARCHAR2(50),
  ֤������    VARCHAR2(50),
  ֤����֤����  VARCHAR2(50),
  ֤����Ч����  VARCHAR2(50),
  �ֻ�����    VARCHAR2(50),
  ��ϵ��ַ    VARCHAR2(100),
  �໤��֤������ VARCHAR2(50),
  �໤��֤������ VARCHAR2(50),
  �໤������   VARCHAR2(50),
  �û�������   VARCHAR2(50),
  �û�����    VARCHAR2(50),
  ������     VARCHAR2(50),
  ����ʱ��    DATE
)
tablespace CLOUDHIS
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
comment on column CLOUDHIS.������ͨ_�û���Ϣ.��ˮ��
  is '��ˮ��';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.ҽԺ����
  is 'ҽԺ����';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.ƽ̨��ʶ
  is 'ƽ̨��ʶ';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.����id
  is '����ID';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.�û����
  is '�û����';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.����
  is '����';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.�Ա�
  is '�Ա�';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.��������
  is '��������';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.֤������
  is '֤������';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.֤������
  is '֤������';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.֤����֤����
  is '֤����֤����';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.֤����Ч����
  is '֤����Ч����';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.�ֻ�����
  is '�ֻ�����';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.��ϵ��ַ
  is '��ϵ��ַ';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.�໤��֤������
  is '�໤��֤������';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.�໤��֤������
  is '�໤��֤������';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.�໤������
  is '�໤������';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.�û�������
  is '�û�������';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.�û�����
  is '�û�����';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.������
  is '������';
comment on column CLOUDHIS.������ͨ_�û���Ϣ.����ʱ��
  is '����ʱ��';
alter table CLOUDHIS.������ͨ_�û���Ϣ
  add constraint PK_������ͨ_�û���Ϣ primary key (��ˮ��)
  using index 
  tablespace CLOUDHIS
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

prompt
prompt Creating sequence SEQ_������ͨ_������־_��ˮ��
prompt ===================================
prompt
create sequence CLOUDHIS.SEQ_������ͨ_������־_��ˮ��
minvalue 1
maxvalue 9999999999
start with 1
increment by 1
cache 10;

prompt
prompt Creating sequence SEQ_������ͨ_����_��ˮ��
prompt =================================
prompt
create sequence CLOUDHIS.SEQ_������ͨ_����_��ˮ��
minvalue 1
maxvalue 9999999999
start with 11
increment by 1
cache 10;

prompt
prompt Creating sequence SEQ_������ͨ_������ϸ_��ˮ��
prompt ===================================
prompt
create sequence CLOUDHIS.SEQ_������ͨ_������ϸ_��ˮ��
minvalue 1
maxvalue 9999999999
start with 11
increment by 1
cache 10;

prompt
prompt Creating sequence SEQ_������ͨ_�û���Ϣ_��ˮ��
prompt ===================================
prompt
create sequence CLOUDHIS.SEQ_������ͨ_�û���Ϣ_��ˮ��
minvalue 1
maxvalue 9999999999
start with 21
increment by 1
cache 10;

prompt
prompt Creating function FU_������ͨ_�õ���Ӧ����
prompt ================================
prompt
CREATE OR REPLACE FUNCTION CLOUDHIS.FU_������ͨ_�õ���Ӧ����(STR_SQL    VARCHAR2,
                                          STR_����ǩ VARCHAR2,
                                          STR_�б�ǩ VARCHAR2) RETURN CLOB IS
  LOB_��Ӧ���� CLOB;
  XML_�����   DBMS_XMLGEN.CTXTYPE;
BEGIN

  XML_����� := DBMS_XMLGEN.NEWCONTEXT(STR_SQL);

  DBMS_XMLGEN.SETROWSETTAG(XML_�����, STR_����ǩ); --�����м���ǩROWSET

  DBMS_XMLGEN.SETROWTAG(XML_�����, STR_�б�ǩ); --=�����б�ǩROW

  DBMS_XMLGEN.SETNULLHANDLING(XML_�����, DBMS_XMLGEN.EMPTY_TAG); --�����ֵ

  LOB_��Ӧ���� := DBMS_XMLGEN.GETXML(XML_�����);

  LOB_��Ӧ���� := SUBSTR(LOB_��Ӧ����, INSTR(LOB_��Ӧ����, '>')+1); --ȥ��<?XML VERSION="1.0">

  RETURN(LOB_��Ӧ����);

END FU_������ͨ_�õ���Ӧ����;
/

prompt
prompt Creating function FU_������ͨ_�ڵ�ֵ
prompt =============================
prompt
CREATE OR REPLACE FUNCTION CLOUDHIS.FU_������ͨ_�ڵ�ֵ(STR_���봮 VARCHAR2,
                                       STR_�ڵ��� VARCHAR2) RETURN VARCHAR2 IS
  STR_�ڵ�ֵ VARCHAR2(100);
BEGIN
  BEGIN
    --FUNCTIONRESULT := XMLTYPE(STR_���봮).EXTRACT('/REQ/' || STR_�ڵ��� ||'/text()').GETSTRINGVAL();
    SELECT EXTRACTVALUE(XMLTYPE(STR_���봮), '/REQ/' || STR_�ڵ���)
      INTO STR_�ڵ�ֵ
      FROM DUAL;
  
  EXCEPTION
    WHEN OTHERS THEN
      STR_�ڵ�ֵ := '';
  END;

  RETURN(STR_�ڵ�ֵ);
END FU_������ͨ_�ڵ�ֵ;
/

prompt
prompt Creating function FU_������ͨ_����½ڵ�
prompt ===============================
prompt
CREATE OR REPLACE FUNCTION CLOUDHIS.FU_������ͨ_����½ڵ�(LOB_��Ӧ����   CLOB,
                                         STR_���λ��   VARCHAR2,
                                         STR_��ӽڵ��� VARCHAR2,
                                         STR_��ӽڵ�ֵ VARCHAR2) RETURN CLOB IS
  LOB_��Ӧ������ CLOB;
  STR_�¼ӽڵ�   VARCHAR2(100);
BEGIN

  --�� <STR_���λ��>�ڵ�ǰ����µĽڵ�<STR_��ӽڵ���>

  /*  SELECT INSERTCHILDXMLAFTER(XMLTYPE(LOB_��Ӧ����), '/RES', STR_���λ��, XMLTYPE('<' || STR_��ӽڵ��� || '>' || STR_��ӽڵ�ֵ || '</' || STR_��ӽڵ��� || '>')).GETCLOBVAL()
  INTO LOB_��Ӧ������
  FROM DUAL;*/

  STR_�¼ӽڵ�   := '<' || STR_��ӽڵ��� || '>' || STR_��ӽڵ�ֵ || '</' || STR_��ӽڵ��� || '>';
  LOB_��Ӧ������ := SUBSTR(LOB_��Ӧ����,
                      0,
                      INSTR(LOB_��Ӧ����, '<' || STR_���λ��, 1, 1) - 1) ||
               STR_�¼ӽڵ� ||
               SUBSTR(LOB_��Ӧ����,
                      INSTR(LOB_��Ӧ����, '<' || STR_���λ��, 1, 1));

  RETURN(LOB_��Ӧ������);
END FU_������ͨ_����½ڵ�;
/

prompt
prompt Creating function FU_������ͨ_��֤����
prompt ==============================
prompt
CREATE OR REPLACE FUNCTION CLOUDHIS.FU_������ͨ_��֤����(p_date IN VARCHAR2) RETURN BOOLEAN IS
  v_flag       BOOLEAN;
  v_year       NUMBER;
  v_month      NUMBER;
  v_day        NUMBER;
  v_isLeapYear BOOLEAN;
BEGIN
  --[��ʼ��]--
  v_flag := TRUE;

  --[��ȡ��Ϣ]--
  v_year  := to_number(substr(p_date, 1, 4));
  v_month := to_number(substr(p_date, 5, 2));
  v_day   := to_number(substr(p_date, 7, 2));

  --[�ж��Ƿ�Ϊ����]--
  IF (MOD(v_year, 400) = 0)
     OR (MOD(v_year, 100) <> 0 AND MOD(v_year, 4) = 0) THEN
    v_isLeapYear := TRUE;
  ELSE
    v_isLeapYear := FALSE;
  END IF;

  --[�ж��·�]--
  IF v_month < 1
     OR v_month > 12 THEN
    v_flag := FALSE;
    RETURN v_flag;
  END IF;

  --[�ж�����]--
  IF v_month IN (1, 3, 5, 7, 8, 10, 12)
     AND (v_day < 1 OR v_day > 31) THEN
    v_flag := FALSE;
  END IF;
  IF v_month IN (4, 6, 9, 11)
     AND (v_day < 1 OR v_day > 30) THEN
    v_flag := FALSE;
  END IF;
  IF v_month IN (2) THEN
    IF (v_isLeapYear) THEN
      --[����]--
      IF (v_day < 1 OR v_day > 29) THEN
        v_flag := FALSE;
      END IF;
    ELSE
      --[������]--
      IF (v_day < 1 OR v_day > 28) THEN
        v_flag := FALSE;
      END IF;
    END IF;
  END IF;

  --[���ؽ��]--
  RETURN v_flag;
END FU_������ͨ_��֤����;
/

prompt
prompt Creating function FU_������ͨ_��֤��ֵ
prompt ==============================
prompt
CREATE OR REPLACE FUNCTION CLOUDHIS.FU_������ͨ_��֤��ֵ(p_string IN VARCHAR2)
  RETURN BOOLEAN
    Is
        i           number;
        k           number;
        flag        boolean;
        v_length    number;
    Begin
        /*
        �㷨:
            ͨ��ASCII���ж��Ƿ����֣�����[48, 57]֮�䡣
            select ascii('0'),ascii('1'),ascii('2'),ascii('3'),ascii('4'),ascii('5'),ascii('6'),ascii('7'),ascii('8'),ascii('9') from dual;
        */

        flag := True;
    SELECT length(p_string) INTO v_length FROM dual;

    FOR i IN 1 .. v_length
    LOOP
      k := ascii(substr(p_string, i, 1));
      IF k < 48
         OR k > 57 THEN
        flag := FALSE;
        EXIT;
      END IF;
    END LOOP;

    RETURN flag;
    END FU_������ͨ_��֤��ֵ;
/

prompt
prompt Creating function FU_������ͨ_��֤���֤
prompt ===============================
prompt
CREATE OR REPLACE FUNCTION CLOUDHIS.FU_������ͨ_��֤���֤(P_IDCARD IN VARCHAR2)
  RETURN INTEGER IS
  V_SUM         NUMBER;
  V_MOD         NUMBER;
  V_LENGTH      NUMBER;
  V_DATE        VARCHAR2(10);
  V_ISDATE      BOOLEAN;
  V_ISNUMBER    BOOLEAN;
  V_ISNUMBER_17 BOOLEAN;
  V_CHECKBIT    CHAR(1);
  V_CHECKCODE   CHAR(11) := '10X98765432';
  V_AREACODE    VARCHAR2(2000) := '11,12,13,14,15,21,22,23,31,32,33,34,35,36,37,41,42,43,44,45,46,50,51,52,53,54,61,62,63,64,65,71,81,82,91,';
BEGIN
  /*
  ����ֵ˵��:
      -1      ���֤����λ������
      -2      ���֤����������ڳ�����Χ
      -3      ���֤���뺬�зǷ��ַ�
      -4      ���֤����У�������
      -5      ���֤���������Ƿ�
      0       ���֤����ͨ��У��
  */
  --[����У��]--
  IF P_IDCARD IS NULL THEN
    RETURN(-1);
  END IF;

  SELECT LENGTHB(P_IDCARD) INTO V_LENGTH FROM DUAL;
  IF V_LENGTH NOT IN (15, 18) THEN
    RETURN(-1);
  END IF;

  --[��λ��У��]--
  IF INSTRB(V_AREACODE, SUBSTR(P_IDCARD, 1, 2) || ',') = 0 THEN
    RETURN(-5);
  END IF;

  --[��ʽ��У��]--
  IF V_LENGTH = 15 THEN
    V_ISNUMBER := FU_������ͨ_��֤��ֵ(P_IDCARD);
    IF NOT (V_ISNUMBER) THEN
      RETURN(-3);
    END IF;
  ELSIF V_LENGTH = 18 THEN
    V_ISNUMBER    := FU_������ͨ_��֤��ֵ(P_IDCARD);
    V_ISNUMBER_17 := FU_������ͨ_��֤��ֵ(SUBSTR(P_IDCARD, 1, 17));
    IF NOT ((V_ISNUMBER) OR
        (V_ISNUMBER_17 AND UPPER(SUBSTR(P_IDCARD, 18, 1)) = 'X')) THEN
      RETURN(-3);
    END IF;
  END IF;

  --[��������У��]--
  IF V_LENGTH = 15 THEN
    SELECT '19' || SUBSTR(P_IDCARD, 7, 6) INTO V_DATE FROM DUAL;
  ELSIF V_LENGTH = 18 THEN
    SELECT SUBSTR(P_IDCARD, 7, 8) INTO V_DATE FROM DUAL;
  END IF;
  V_ISDATE := FU_������ͨ_��֤����(V_DATE);
  IF NOT (V_ISDATE) THEN
    RETURN(-2);
  END IF;

  --[У����У��]--
  IF V_LENGTH = 18 THEN
    V_SUM      := (TO_NUMBER(SUBSTRB(P_IDCARD, 1, 1)) +
                  TO_NUMBER(SUBSTRB(P_IDCARD, 11, 1))) * 7 +
                  (TO_NUMBER(SUBSTRB(P_IDCARD, 2, 1)) +
                  TO_NUMBER(SUBSTRB(P_IDCARD, 12, 1))) * 9 +
                  (TO_NUMBER(SUBSTRB(P_IDCARD, 3, 1)) +
                  TO_NUMBER(SUBSTRB(P_IDCARD, 13, 1))) * 10 +
                  (TO_NUMBER(SUBSTRB(P_IDCARD, 4, 1)) +
                  TO_NUMBER(SUBSTRB(P_IDCARD, 14, 1))) * 5 +
                  (TO_NUMBER(SUBSTRB(P_IDCARD, 5, 1)) +
                  TO_NUMBER(SUBSTRB(P_IDCARD, 15, 1))) * 8 +
                  (TO_NUMBER(SUBSTRB(P_IDCARD, 6, 1)) +
                  TO_NUMBER(SUBSTRB(P_IDCARD, 16, 1))) * 4 +
                  (TO_NUMBER(SUBSTRB(P_IDCARD, 7, 1)) +
                  TO_NUMBER(SUBSTRB(P_IDCARD, 17, 1))) * 2 +
                  TO_NUMBER(SUBSTRB(P_IDCARD, 8, 1)) * 1 +
                  TO_NUMBER(SUBSTRB(P_IDCARD, 9, 1)) * 6 +
                  TO_NUMBER(SUBSTRB(P_IDCARD, 10, 1)) * 3;
    V_MOD      := MOD(V_SUM, 11);
    V_CHECKBIT := SUBSTRB(V_CHECKCODE, V_MOD + 1, 1);

    IF V_CHECKBIT = UPPER(SUBSTRB(P_IDCARD, 18, 1)) THEN
      RETURN 0;
    ELSE
      RETURN(-4);
    END IF;
  ELSE
    RETURN 0;
  END IF;
EXCEPTION
  WHEN OTHERS THEN
    RETURN(-1);
END FU_������ͨ_��֤���֤;
/

prompt
prompt Creating function FU_������ͨ_��֤�ֻ���
prompt ===============================
prompt
CREATE OR REPLACE FUNCTION CLOUDHIS.FU_������ͨ_��֤�ֻ���(STR_�ֻ����� VARCHAR2) RETURN INTEGER IS
  INT_����ֵ INTEGER;
BEGIN
  /*
    ��Ч������
      1)11λ
      2)��1��ͷ
      3)ֻ�������ֹ���
    ����ֵ��
      -1��ʧ�ܣ�0���ɹ�
  */

  SELECT REGEXP_INSTR(STR_�ֻ�����, '1\d{10}$')
    INTO INT_����ֵ
    FROM DUAL;
  IF INT_����ֵ = 0 THEN
    RETURN - 1;
  END IF;

  RETURN 0;

END FU_������ͨ_��֤�ֻ���;
/

prompt
prompt Creating procedure PR_������ͨ_������־
prompt ===============================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_������־(Str_ƽ̨��ʶ IN VARCHAR2,
                                         Str_ҽԺ���� IN VARCHAR2,
                                         Str_���ܱ��� IN VARCHAR2,
                                         Str_������� IN VARCHAR2,
                                         Dat_����ʱ�� IN DATE,
                                         INT_����ֵ   IN INTEGER,
                                         Str_������Ϣ IN VARCHAR2,
                                         Dat_ִ��ʱ�� IN DATE) IS

  PRAGMA AUTONOMOUS_TRANSACTION; --�������ﲻӰ��������
BEGIN

  BEGIN
    INSERT INTO ������ͨ_������־
      (��ˮ��,
       ƽ̨��ʶ,
       ҽԺ����,
       ���ܱ���,
       �������,
       ����ʱ��,
       ����ֵ,
       ������Ϣ,
       ִ��ʱ��)
    VALUES
      (seq_ƽ̨�ӿ�_������־_��ˮ��.nextval,
       Str_ƽ̨��ʶ,
       Str_ҽԺ����,
       Str_���ܱ���,
       Str_�������,
       Dat_����ʱ��,
       INT_����ֵ,
       Str_������Ϣ,
       Dat_ִ��ʱ��);
  
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;
      RETURN;
  END;

  COMMIT;

  RETURN;

END PR_������ͨ_������־;
/

prompt
prompt Creating procedure PR_������ͨ_���ɷѼ�¼֧��
prompt ==================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_���ɷѼ�¼֧��(STR_������� IN VARCHAR2,
                                            LOB_��Ӧ���� OUT CLOB,
                                            RES_CODE     OUT INTEGER,
                                            RES_MSG      OUT VARCHAR2) IS

  STR_SQL              VARCHAR2(1000);
  STR_ҽԺID           VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
  STR_ƽ̨������       VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ORDER_ID');
  STR_�ɷѵ�ΨһID     VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_SEQUENCE');
  STR_��ˮ��           VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'SERIAL_NUM');
  STR_��������         VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_DATE');
  STR_����ʱ��         VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_TIME');
  STR_֧������ID       VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������,
                                               'PAY_CHANNEL_ID');
  STR_�ܽ��           VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������,
                                                'PAY_TOTAL_FEE');
  STR_Ӧ�����         VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������,
                                               'PAY_BEHOOVE_FEE');
  STR_�����Ը����     VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_ACTUAL_FEE');
  STR_ҽ��ͳ��֧����� VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_MI_FEE');
  STR_������Ӧ����     VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_RES_CODE');
  STR_������Ӧ����     VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_RES_DESC');
  STR_�̻���           VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'MERCHANT_ID');
  STR_�ն˺�           VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'TERMINAL_ID');
  STR_���п���         VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'BANK_NO');
  STR_������֧���ʺ�   VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_ACCOUNT');
  STR_����ԱID         VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'OPERATOR_ID');
  STR_�վݺ�           VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'RECEIPT_ID');

BEGIN
  BEGIN
  
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
    
      RES_CODE := 0;
      RES_MSG  := '���׳ɹ�';
    ELSE
      RES_CODE := 300201;
      RES_MSG  := '��Ч�ĽɷѼ�¼';
    END IF;
  
  END;

END PR_������ͨ_���ɷѼ�¼֧��;
/

prompt
prompt Creating procedure PR_������ͨ_�Һż�¼��ѯ
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_�Һż�¼��ѯ(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2,
                                           STR_���ܱ��� IN VARCHAR2,
                                           LOB_��Ӧ���� OUT CLOB,
                                           INT_����ֵ   OUT INTEGER,
                                           STR_������Ϣ OUT VARCHAR2) IS

  --�����������
  STR_ҽԺID     VARCHAR2(50);
  STR_ƽ̨������ VARCHAR2(50);
  STR_ҽԺ������ VARCHAR2(50);
  STR_��ʼ����   VARCHAR2(50);
  STR_��������   VARCHAR2(50);
  STR_��ǰҳ��   VARCHAR2(50);
  STR_ÿҳ����   VARCHAR2(50);

  STR_SQL VARCHAR2(2000);

BEGIN

  --���������������
  STR_ҽԺID     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
  STR_ƽ̨������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ORDER_ID');
  STR_ҽԺ������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_ORDER_ID');
  STR_��ʼ����   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'BEGIN_DATE');
  STR_��������   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'END_DATE');
  STR_��ǰҳ��   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAGE_CURRENT');
  STR_ÿҳ����   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAGE_SIZE');
  BEGIN
  
    STR_SQL := 'SELECT T.ƽ̨������ AS ORDER_ID,
                   DECODE(T.����״̬,
                          ''��֧��'',
                          ''2'',
                          ''��ȡ��'',
                          ''3'',
                          ''��ȡ��'',
                          ''4'',
                          ''���˿�'',
                          ''5'',
                          ''��֧��'',
                          ''6'',
                          ''1'') AS ORDER_STATUS,
                   '''' AS HOSP_SERIAL_NUM,
                   TT.ȡ��ʱ�� AS GET_REGNO_DATE,
                   T.ҽԺ���׺� AS HOSP_PAY_ID,
                   T.���ﲡ���� AS HOSP_MEDICAL_NUM,
                   TO_CHAR(TT.ԤԼʱ�ο�ʼ, ''hh24:mi'') || ''-'' ||
                   TO_CHAR(TT.ԤԼʱ�ν���, ''hh24:mi'') AS ORDER_STATUS,
                   T.ҽԺ�˿�� AS HOSP_REFUND_ID,
                   T.�˿��־ AS REFUND_FLAG,
                   T.ȡ��ʱ�� AS CANCEL_DATE
              FROM ������ͨ_���� T, �������_ԤԼ�Һ� TT
             WHERE T.ҽԺ���� = TT.��������
               AND T.�������� = TT.����ID
               AND (T.ƽ̨������=''' || STR_ƽ̨������ || ''' OR ''' ||
               STR_ƽ̨������ || ''' IS NULL)
               AND TT.ԤԼʱ�� BETWEEN ' || 'TO_DATE(''' ||
               STR_��ʼ���� || ''', ''yyyy-MM-dd'')  AND 
                    TO_DATE(''' || STR_�������� ||
               ''', ''yyyy-MM-dd'')';
  
  
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', 'ORDER_LIST');
  
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
    
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM ������ͨ_���� T, �������_ԤԼ�Һ� TT
       WHERE T.ҽԺ���� = TT.��������
         AND T.�������� = TT.����ID
         AND (T.ƽ̨������ = STR_ƽ̨������ OR STR_ƽ̨������ IS NULL)
         AND TT.ԤԼʱ�� BETWEEN TO_DATE(STR_��ʼ����, 'yyyy-MM-dd ') AND
             TO_DATE(STR_��������, 'yyyy-MM-dd');
    
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'ORDER_LIST',
                                'COUNT',
                                INT_����ֵ);
    
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    ELSE
      INT_����ֵ   := 201201;
      STR_������Ϣ := 'δ��ѯ���ҺŶ�����¼';
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '��Ӧ���󱨴�:' || SQLERRM;
      GOTO �˳�;
  END;
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);
              
  RETURN;
END PR_������ͨ_�Һż�¼��ѯ;
/

prompt
prompt Creating procedure PR_������ͨ_�Һ�֧��
prompt ===============================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_�Һ�֧��(STR_������� IN VARCHAR2,
                                         STR_ƽ̨��ʶ IN VARCHAR2,
                                         STR_���ܱ��� IN VARCHAR2,
                                         LOB_��Ӧ���� OUT CLOB,
                                         INT_����ֵ   OUT INTEGER,
                                         STR_������Ϣ OUT VARCHAR2) IS

  --�����������
  STR_ҽԺID         VARCHAR2(50);
  STR_ƽ̨������     VARCHAR2(50);
  STR_ҽԺ������     VARCHAR2(50);
  STR_��ˮ��         VARCHAR2(50);
  STR_��������       VARCHAR2(50);
  STR_����ʱ��       VARCHAR2(50);
  STR_֧������ID     VARCHAR2(50);
  STR_�ܽ��         VARCHAR2(50);
  STR_Ӧ�����       VARCHAR2(50);
  STR_ʵ�����       VARCHAR2(50);
  STR_������Ӧ����   VARCHAR2(50);
  STR_������Ӧ����   VARCHAR2(50);
  STR_�̻���         VARCHAR2(50);
  STR_�ն˺�         VARCHAR2(50);
  STR_���п���       VARCHAR2(50);
  STR_������֧���˺� VARCHAR2(50);

  --��ҵ�������
  STR_SQL          VARCHAR2(1000);
  STR_ԤԼ����     VARCHAR2(50);
  STR_����״̬     VARCHAR2(50);
  DAT_ϵͳʱ��     DATE;
  STR_ҽԺ֧������ VARCHAR2(50);
  NUM_�Һŷ���     NUMBER(10, 4);
  NUM_���Ʒ���     NUMBER(10, 4);
  STR_��α�ʶ varchar2(50);

BEGIN
  BEGIN
    --���������������
    STR_ҽԺID         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_ƽ̨������     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ORDER_ID');
    STR_ҽԺ������     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_ORDER_ID');
    STR_��ˮ��         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'SERIAL_NUM');
    STR_��������       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_DATE');
    STR_����ʱ��       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_TIME');
    STR_֧������ID     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_CHANNEL_ID');
    STR_�ܽ��         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_TOTAL_FEE');
    STR_Ӧ�����       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_COPE_FEE');
    STR_ʵ�����       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_FEE');
    STR_������Ӧ����   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_RES_CODE');
    STR_������Ӧ����   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_RES_DESC');
    STR_�̻���         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'MERCHANT_ID');
    STR_�ն˺�         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'TERMINAL_ID');
    STR_���п���       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'BANK_NO');
    STR_������֧���˺� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_ACCOUNT');
  
    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    -- ��ҽԺ֧�����š�
    SELECT SYS_GUID() INTO STR_ҽԺ֧������ FROM DUAL;
  
    --����֤���ݡ�
    IF STR_ƽ̨������ IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ƽ̨�����Ų���Ϊ�գ�';
      GOTO �˳�;
    END IF;
    IF STR_��ˮ�� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��ˮ�Ų���Ϊ�գ�';
      GOTO �˳�;
    END IF;
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�������ڲ���Ϊ�գ�';
      GOTO �˳�;
    END IF;
    IF STR_����ʱ�� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '����ʱ�䲻��Ϊ�գ�';
      GOTO �˳�;
    END IF;
    IF STR_�ܽ�� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�ܽ���Ϊ�գ�';
      GOTO �˳�;
    END IF;
    IF STR_Ӧ����� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'Ӧ������Ϊ�գ�';
      GOTO �˳�;
    END IF;
    IF STR_ʵ����� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ʵ������Ϊ�գ�';
      GOTO �˳�;
    END IF;
  
    --����֤����״̬��
    BEGIN
      SELECT ��������, ����״̬, �Һŷ���, ���Ʒ���
        INTO STR_ԤԼ����, STR_����״̬, NUM_�Һŷ���, NUM_���Ʒ���
        FROM ������ͨ_����
       WHERE ҽԺ���� = STR_ҽԺID
         AND ƽ̨������ = STR_ƽ̨������
         AND (ҽԺ������ = STR_ҽԺ������ OR STR_ҽԺ������ IS NULL);
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 200801;
        STR_������Ϣ := '�ҺŶ���������';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
    IF NUM_�Һŷ��� + NUM_���Ʒ��� <> to_NUMBER(STR_ʵ�����) THEN
      INT_����ֵ   := 200804;
      STR_������Ϣ := '�ҺŽ���ȷ';
      GOTO �˳�;
    END IF;
  
    IF STR_����״̬ = '��֧��' THEN
      INT_����ֵ   := 200802;
      STR_������Ϣ := '�ҺŶ�����֧��';
      GOTO �˳�;
    ELSIF STR_����״̬ = '��ȡ��' THEN
      INT_����ֵ   := 200805;
      STR_������Ϣ := '�ҺŶ�����ȡ��';
      GOTO �˳�;
    ELSIF STR_����״̬ = '���˿�' THEN
      INT_����ֵ   := 200806;
      STR_������Ϣ := '�ҺŶ������˿�';
      GOTO �˳�;
    END IF;
  
    -- ��֤ԤԼ��
    SELECT COUNT(1)
      INTO INT_����ֵ
      FROM �������_ԤԼ�Һ�
     WHERE �������� = STR_ҽԺID
       AND ����ID = STR_ԤԼ����
       AND ȥ���־ = 'ռ��'
       AND ��ʱʱ�� < SYSDATE;
  
    IF INT_����ֵ > 0 THEN
      INT_����ֵ   := 200803;
      STR_������Ϣ := '�ҺŶ����ѹر�';
      GOTO �˳�;
    END IF;
  
   -- ��ȡ�հ�α�ʶ
    SELECT �հ�α�ʶ
      INTO STR_��α�ʶ
      FROM �������_ԤԼ�Һ�
     WHERE �������� = STR_ҽԺID
       AND ����ID = STR_ԤԼ����;

  
    -- �����ܴ���
    BEGIN
    
      -- ����ԤԼ��
      UPDATE �������_ԤԼ�Һ� G
         SET ֧����־ = '��', ȥ���־ = 'ԤԼ'
       WHERE �������� = STR_ҽԺID
         AND ����ID = STR_ԤԼ����;
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '����ԤԼ��ʧ�ܣ�';
        GOTO �˳�;
      END IF;
      
       -- ����
      UPDATE �������_���Ű�ʱ�α�
         SET �ѹҺ��� = �ѹҺ��� + 1
       WHERE �������� = STR_ҽԺID
         AND �հ�α�ʶ = STR_��α�ʶ;
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '������Դʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      -- ���¶���״̬
      UPDATE ������ͨ_����
         SET ����״̬       = '��֧��',
             �ܽ��         = TO_NUMBER(STR_�ܽ��),
             Ӧ�����       = TO_NUMBER(STR_Ӧ�����),
             ʵ�����       = TO_NUMBER(STR_ʵ�����),
             ƽ̨������ˮ�� = STR_��ˮ��,
             ƽ̨����ʱ��   = TO_DATE(STR_�������� || ' ' || STR_����ʱ��,
                                'yyyy-MM-dd hh24:mi:ss'),
             ҽԺ֧����     = STR_ҽԺ֧������,
             ֧������       = STR_֧������ID,
             ƽ̨�˿��     = NULL,
             ƽ̨�˿�ʱ��   = NULL,
             ������         = STR_ƽ̨��ʶ,
             ����ʱ��       = DAT_ϵͳʱ��
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ҽԺ���� = STR_ҽԺID
         AND ƽ̨������ = STR_ƽ̨������
         AND ����״̬ = '��֧��';
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '���¶���ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
    END;
  
    STR_SQL      := 'select ''' || STR_ҽԺ֧������ ||
                    ''' as HOSP_PAY_ID,
                            '''' as RECEIPT_ID,
                            '''' as HOSP_SERIAL_NUM,
                            '''' as HOSP_MEDICAL_NUM,
                            '''' as HOSP_GETREG_DATE,
                            '''' as HOSP_SEE_DOCT_ADDR,
                            '''' as HOSP_REMARK
                            from dual';
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', '');
  
    INT_����ֵ   := 0;
    STR_������Ϣ := '���׳ɹ�';
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ӧ���󱨴�' || SQLERRM;
      GOTO �˳�;
  END;

  COMMIT;
  RETURN;

  --���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => DAT_ϵͳʱ��,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  ROLLBACK;
  RETURN;

END PR_������ͨ_�Һ�֧��;
/

prompt
prompt Creating procedure PR_������ͨ_��Դ����
prompt ===============================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_��Դ����(STR_������� IN VARCHAR2,
                                         LOB_��Ӧ���� OUT CLOB,
                                         RES_CODE     OUT INTEGER,
                                         RES_MSG      OUT VARCHAR2) IS

  STR_SQL          VARCHAR2(1000);
  STR_��Դ����ID   VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'LOCK_ID');
  STR_�Һ�����ID   VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'CHANNEL_ID');
  STR_�Ű�ID       VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REG_ID');
  STR_ҽԺID       VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
  STR_����ID       VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DEPT_ID');
  STR_ҽ��ID       VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DOCTOR_ID');
  STR_��������     VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REG_DATE');
  STR_ʱ��         VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'TIME_FLAG');
  STR_��ʱ��ʼʱ�� VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'BEGIN_TIME');
  STR_��ʱ����ʱ�� VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'END_TIME');
  STR_�Һŷ���     VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REG_FEE');
  STR_���Ʒ���     VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'TREAT_FEE');
  STR_��ϯ����     VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'AGENT_ID');
BEGIN
  BEGIN
  
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
    
      RES_CODE := 0;
      RES_MSG  := '���׳ɹ�';
    ELSE
      RES_CODE := 200101;
      RES_MSG  := '���Ҳ����ڣ�δ��ѯ�����Ҽ�¼';
    END IF;
  
  END;

END PR_������ͨ_��Դ����;
/

prompt
prompt Creating procedure PR_������ͨ_��鱨���ѯ
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_��鱨���ѯ(STR_������� IN VARCHAR2,
                                             STR_ƽ̨��ʶ IN VARCHAR2,
                                             STR_���ܱ��� IN VARCHAR2,
                                             LOB_��Ӧ���� OUT CLOB,
                                             INT_����ֵ   OUT INTEGER,
                                             STR_������Ϣ OUT VARCHAR2) IS

  STR_SQL VARCHAR2(3000);
  --�����������
  STR_ҽԺID       VARCHAR2(50);
  STR_��鱨�浥�� VARCHAR2(50);
  STR_�û�Ժ��ID   VARCHAR2(50);
BEGIN
  BEGIN
  
    --�����������
    STR_ҽԺID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_��鱨�浥�� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REPORT_ID');
    STR_�û�Ժ��ID   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_PATIENT_ID');
    
    IF STR_��鱨�浥�� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '���浥�Ų���Ϊ��';
      GOTO �˳�;
    END IF;
  
    STR_SQL      := 'SELECT S.�������� as HOS_ID,
                           S.����ID AS HOSP_PATIENT_ID,
                           '''' as PATIENT_IDCARD_TYPE,
                           (select A.���֤��
                              from ������Ŀ_������Ϣ A
                             where A.�������� = S.��������
                               and A.����ID = S.����ID) as PATIENT_IDCARD_NO,
                           '''' as PATIENT_CARD_TYPE,
                           '''' as PATIENT_CARD_NO,
                           (select A.����
                              from ������Ŀ_������Ϣ A
                             where A.�������� = S.��������
                               and A.����ID = S.����ID) as PATIENT_NAME,
                           (select A.�Ա�
                              from ������Ŀ_������Ϣ A
                             where A.�������� = S.��������
                               and A.����ID = S.����ID) as PATIENT_SEX,
                           (select A.����
                              from ������Ŀ_������Ϣ A
                             where A.�������� = S.��������
                               and A.����ID = S.����ID) as PATIENT_AGE,
                           '''' as VISIT_NUMBER,
                           ''ҽ��'' as MEDICAL_INSURANNCE_TYPE,
                           '''' as SPECIMEN_NAME,
                           J.������ as SPECIMEN_ID,
                           S.��Ŀ���� as ITEM_NAME,
                           '''' as COMPLAINT,
                           J.������ as DIAGNOSIS,
                           J.������� as SEEN,
                           J.���ֱ������� as "CONTENT",
                           to_char(J.����ʱ��, ''yyyy-MM-dd hh24:mi:ss'') as REPORT_TIME,
                           (select B.��������
                              from ������Ŀ_�������� B
                             where B.�������� = S.��������
                               and B.���ұ��� = S.ִ�п��ұ���) as DEPT_NAME,
                           (select C.��Ա����
                              from ������Ŀ_��Ա���� C
                             where C.�������� = S.��������
                               and C.��Ա���� = S.ҽ������) as DOCTOR_NAME,
                           (select C.��Ա����
                              from ������Ŀ_��Ա���� C
                             where C.�������� = S.��������
                               and C.��Ա���� = J.���ҽ������) as REVIEW_NAME,
                           to_char(J.���ʱ��, ''yyyy-MM-dd hh24:mi:ss'') as REVIEW_TIME,
                           '''' as REMARK
                      FROM ������_���� S, ������_��� J
                     WHERE S.�������� = J.��������
                       AND S.���뵥ID = J.���뵥ID     
                       AND S.��������=''' || STR_ҽԺID || '''
                       AND J.���浥�� =''' || STR_��鱨�浥��||'''';
                       
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', '');
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN    
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    ELSE
      INT_����ֵ   := 800401;
      STR_������Ϣ := '���/���鱨�浥�Ų�����';
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ӧ���󱨴�'||SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
-- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  RETURN;

END PR_������ͨ_��鱨���ѯ;
/

prompt
prompt Creating procedure PR_������ͨ_�������б��ѯ
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_�������б��ѯ(STR_������� IN VARCHAR2,
                                             STR_ƽ̨��ʶ IN VARCHAR2,
                                             STR_���ܱ��� IN VARCHAR2,
                                             LOB_��Ӧ���� OUT CLOB,
                                             INT_����ֵ   OUT INTEGER,
                                             STR_������Ϣ OUT VARCHAR2) IS

  STR_SQL VARCHAR2(1000);
  --�����������
  STR_ҽԺID       VARCHAR2(50);
  STR_�û�Ժ��ID   VARCHAR2(50);
  STR_�û�֤������ VARCHAR2(50);
  STR_�û�֤������ VARCHAR2(50);
  STR_�û�������   VARCHAR2(50);
  STR_�û�����     VARCHAR2(50);
  STR_�û�����     VARCHAR2(50);
  STR_�û��Ա�     VARCHAR2(50);
  STR_�û�����     VARCHAR2(50);
  STR_��ʼ����     VARCHAR2(50);
  STR_��������     VARCHAR2(50);
BEGIN
  BEGIN
  
    --���������������
    STR_ҽԺID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_�û�Ժ��ID   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_PATIENT_ID');
    STR_�û�֤������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PATIENT_IDCARD_TYPE');
    STR_�û�֤������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PATIENT_IDCARD_NO');
    STR_�û�������   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PATIENT_CARD_TYPE');
    STR_�û�����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PATIENT_CARD_NO');
    STR_�û�����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PATIENT_NAME');
    STR_�û��Ա�     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PATIENT_SEX');
    STR_�û�����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PATIENT_AGE');
    STR_��ʼ����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'BEGIN_DATE');
    STR_��������     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'END_DATE');
  
    if STR_��ʼ���� is null or fu_����ת����(STR_��ʼ����) is null then
      INT_����ֵ   := '-1';
      STR_������Ϣ := '��������ȷ����ʼ����!';
      GOTO �˳�;
    end if;
    if STR_�������� is null or fu_����ת����(STR_��������) is null then
      INT_����ֵ   := '-1';
      STR_������Ϣ := '��������ȷ�Ľ�������!';
      GOTO �˳�;
    end if;
  
    IF STR_�û�֤������ IS NULL AND STR_�û����� IS NULL THEN
      INT_����ֵ   := '-1';
      STR_������Ϣ := '֤������򿨺ű�������һ��!';
      GOTO �˳�;
    END IF;
  
    IF STR_�û�Ժ��ID IS NOT NULL THEN
      SELECT COUNT(1) INTO INT_����ֵ FROM ������Ŀ_������Ϣ;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := '800102';
        STR_������Ϣ := '�û�������';
        GOTO �˳�;
      END IF;
    ELSE
      SELECT A.����ID
        INTO STR_�û�Ժ��ID
        FROM ������Ŀ_������Ϣ A
       WHERE A.���� = STR_�û�����
         AND A.�Ա� = STR_�û��Ա�
         AND A.���� = STR_�û�����
         AND (A.���֤�� = STR_�û�֤������ OR A.����ID = STR_�û�����)
         AND ROWNUM = 1;
      IF STR_�û�Ժ��ID IS NULL THEN
        INT_����ֵ   := '800102';
        STR_������Ϣ := '�û�������';
        GOTO �˳�;
      END IF;
    END IF;
  
    STR_SQL := 'SELECT J.���浥�� AS REPORT_ID,
                     '''' AS DIAGNOSIS,
                     S.��Ŀ���� AS ITEM_NAME,
                     '''' AS SPECIMEN_NAME,
                     J.������ AS SPECIMEN_ID,
                     TO_CHAR(J.����ʱ��, ''yyyy-MM-dd hh24:mi:ss'') AS REPORT_TIME,
                     (SELECT B.��������
                        FROM ������Ŀ_�������� B
                       WHERE B.�������� = S.��������
                         AND B.���ұ��� = S.ִ�п��ұ���) AS DEPT_NAME,
                     (SELECT C.��Ա����
                        FROM ������Ŀ_��Ա���� C
                       WHERE C.�������� = S.��������
                         AND C.��Ա���� = S.ҽ������) AS DOCTOR_NAME,
                     decode(S.����,''����'',''0'',''2'') AS REPORT_TYPE,
                     '''' AS REMARK
                FROM ������_���� S, ������_��� J
               WHERE S.�������� = J.��������
                 AND S.���뵥ID = J.���뵥ID
                 AND S.��������=' || STR_ҽԺID || 'AND S.����ID=' ||
               STR_�û�Ժ��ID || 'AND J.����ʱ�� BETWEEN TO_DATE(''' || STR_��ʼ���� ||
               ''',''yyyy-MM-dd'') AND TO_DATE(''' || STR_�������� ||
               ''',''yyyy-MM-dd'')';
  
    LOB_��Ӧ���� := fu_������ͨ_�õ���Ӧ����(LOB_��Ӧ����, 'REPORT_INFO', 'REPORT_INFO');
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
    
      LOB_��Ӧ���� := fu_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'HOS_ID',
                                STR_ҽԺID);
      LOB_��Ӧ���� := fu_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'HOSP_PATIENT_ID',
                                STR_�û�Ժ��ID);
      LOB_��Ӧ���� := fu_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'PATIENT_IDCARD_TYPE',
                                STR_�û�֤������);
      LOB_��Ӧ���� := fu_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'PATIENT_IDCARD_NO',
                                STR_�û�֤������);
      LOB_��Ӧ���� := fu_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'PATIENT_CARD_TYPE',
                                STR_�û�������);
      LOB_��Ӧ���� := fu_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'PATIENT_CARD_NO',
                                STR_�û�����);
      LOB_��Ӧ���� := fu_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'PATIENT_NAME',
                                STR_�û�����);
      LOB_��Ӧ���� := fu_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'PATIENT_SEX',
                                STR_�û��Ա�);
      LOB_��Ӧ���� := fu_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'PATIENT_AGE',
                                STR_�û�����);
      LOB_��Ӧ���� := fu_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'VISIT_NUMBER',
                                '');
      LOB_��Ӧ���� := fu_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'MEDICAL_INSURANNCE_TYPE',
                                '');
      LOB_��Ӧ���� := '<RES>' || LOB_��Ӧ���� || '</RES>';
    
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    ELSE
      INT_����ֵ   := 800101;
      STR_������Ϣ := '���/���鱨���¼�����ڣ�δ��ѯ�����/���鱨���¼';
    END IF;
  
  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ӧ���󱨴�' || SQLERRM;
      GOTO �˳�;
    
  END;

  -- ���쳣�˳���
  <<�˳�>>
-- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  RETURN;

END PR_������ͨ_�������б��ѯ;
/

prompt
prompt Creating procedure PR_������ͨ_�ɷѶ�����ѯ
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_�ɷѶ�����ѯ(STR_������� IN VARCHAR2,
                                            LOB_��Ӧ���� OUT CLOB,
                                            RES_CODE     OUT INTEGER,
                                            RES_MSG      OUT VARCHAR2) IS

  STR_SQL              VARCHAR2(1000);
  STR_ҽԺID           VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
--���滹��

BEGIN
  BEGIN
  
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
    
      RES_CODE := 0;
      RES_MSG  := '���׳ɹ�';
    ELSE
      RES_CODE := 300201;
      RES_MSG  := '��Ч�ĽɷѼ�¼';
    END IF;
  
  END;

END PR_������ͨ_�ɷѶ�����ѯ;
/

prompt
prompt Creating procedure PR_������ͨ_�ɷѼ�¼��ѯ
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_�ɷѼ�¼��ѯ(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2,
                                           STR_���ܱ��� IN VARCHAR2,
                                           LOB_��Ӧ���� OUT CLOB,
                                           INT_����ֵ   OUT INTEGER,
                                           STR_������Ϣ OUT VARCHAR2) IS

  --�����������
  STR_ҽԺID       VARCHAR2(50);
  STR_�û�Ժ��ID   VARCHAR2(50);
  STR_�û�֤������ VARCHAR2(50);
  STR_�û�֤������ VARCHAR2(50);
  STR_�û�������   VARCHAR2(50);
  STR_�û�����     VARCHAR2(50);
  STR_�û�����     VARCHAR2(50);
  STR_�û��Ա�     VARCHAR2(50);
  STR_�û�����     VARCHAR2(50);
  STR_��ѯ״̬���� VARCHAR2(50);
  STR_��ʼ����     VARCHAR2(50);
  STR_��������     VARCHAR2(50);

  STR_SQL VARCHAR2(1000);

BEGIN
  BEGIN
    --���������������
    STR_ҽԺID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_�û�Ժ��ID   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_PATIENT_ID');
    STR_�û�֤������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'IDCARD_TYPE');
    STR_�û�֤������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'IDCARD_NO');
    STR_�û�������   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'CARD_TYPE');
    STR_�û�����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'CARD_NO');
    STR_�û�����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PATIENT_NAME');
    STR_�û��Ա�     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PATIENT_SEX');
    STR_�û�����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PATIENT_AGE');
    STR_��ѯ״̬���� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'QUERY_TYPE');
    STR_��ʼ����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'BEGIN_DATE');
    STR_��������     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'END_DATE');
  
    IF STR_�û�֤������ IS NULL AND STR_�û����� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��֤������򿨺�';
      GOTO �˳�;
    END IF;
  
    IF STR_��ѯ״̬���� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���ѯ״̬����';
      GOTO �˳�;
    END IF;
    IF STR_��ʼ���� IS NULL OR FU_����ת����(STR_��ʼ����) IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫����ȷ����ʼ����';
      GOTO �˳�;
    END IF;
    IF STR_�������� IS NULL OR FU_����ת����(STR_��������) IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫����ȷ�Ľ�������';
      GOTO �˳�;
    END IF;
  
    IF STR_�û�Ժ��ID IS NULL THEN
      BEGIN
        SELECT ����ID
          INTO STR_�û�Ժ��ID
          FROM ������ͨ_�û���Ϣ
         WHERE ҽԺ���� = STR_ҽԺID
           AND (֤������ = STR_�û�֤������ OR ֤������ = STR_�û�֤������);
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          INT_����ֵ   := 300102;
          STR_������Ϣ := '�û�������';
          GOTO �˳�;
        WHEN OTHERS THEN
          INT_����ֵ   := 99;
          STR_������Ϣ := 'ϵͳ����:' || SQLERRM;
          GOTO �˳�;
      END;
    END IF;
  
    STR_SQL := 'SELECT T.��ˮ�� AS HOSP_SEQUENCE,
                     (SELECT A.�Һſ�������
                        FROM �������_ԤԼ�Һ� A
                       WHERE A.�������� = T.ҽԺ����
                         AND A.����ID = T.��������) AS DEPT_NAME,
                     (SELECT A.�Һ�ҽ������
                        FROM �������_ԤԼ�Һ� A
                       WHERE A.�������� = T.ҽԺ����
                         AND A.����ID = T.��������) AS DOCTOR_NAME,
                     T.ʵ����� AS PAY_AMOUT,
                     T.֧������ AS PAY_CHANNEL_ID,
                     DECODE(T.����״̬, ''��֧��'', ''0'', ''��֧��'', ''1'', ''���˿�'', ''2'') AS ORDER_STATUS,
                     '''' AS RECEIPT_ID,
                     '''' AS PAY_REMARK,
                     T.ƽ̨����ʱ�� AS RECEIPT_DATE
                FROM ������ͨ_���� T
               WHERE T.ƽ̨��ʶ = STR_ƽ̨��ʶ
                 AND T.ҽԺ���� = STR_ҽԺID
                 AND T.����ID = STR_�û�Ժ��ID
                 AND T.ƽ̨����ʱ�� BETWEEN TO_DATE(''' || STR_��ʼ���� ||
               ''', ''yyyy-MM-dd'') AND
                     TO_DATE(''' || STR_�������� ||
               ''', ''yyyy-MM-dd'')
                 AND T.����״̬ = DECODE(STR_��ѯ״̬����,
                                     ''0'',
                                     T.����״̬,
                                     ''1'',
                                     ''��֧��'',
                                     ''2'',
                                     ''��֧��'',
                                     ''3'',
                                     ''���˿�'')';
  
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', 'PAY_LIST');
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
    
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����   => LOB_��Ӧ����,
                                STR_���λ��   => 'PAY_LIST',
                                STR_��ӽڵ��� => 'USER_NAME',
                                STR_��ӽڵ�ֵ => STR_�û�����);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����   => LOB_��Ӧ����,
                                STR_���λ��   => 'PAY_LIST',
                                STR_��ӽڵ��� => 'HOSP_PATIENT_ID',
                                STR_��ӽڵ�ֵ => STR_�û�Ժ��ID);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����   => LOB_��Ӧ����,
                                STR_���λ��   => 'PAY_LIST',
                                STR_��ӽڵ��� => 'IDCARD_TYPE',
                                STR_��ӽڵ�ֵ => STR_�û�֤������);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����   => LOB_��Ӧ����,
                                STR_���λ��   => 'PAY_LIST',
                                STR_��ӽڵ��� => 'IDCARD_NO',
                                STR_��ӽڵ�ֵ => STR_�û�֤������);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����   => LOB_��Ӧ����,
                                STR_���λ��   => 'PAY_LIST',
                                STR_��ӽڵ��� => 'CARD_TYPE',
                                STR_��ӽڵ�ֵ => STR_�û�������);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����   => LOB_��Ӧ����,
                                STR_���λ��   => 'PAY_LIST',
                                STR_��ӽڵ��� => 'CARD_NO',
                                STR_��ӽڵ�ֵ => STR_�û�����);
    
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    ELSE
      INT_����ֵ   := 300101;
      STR_������Ϣ := '�ɷѼ�¼�����ڣ�δ��ѯ���ɷѶ�����¼';
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '��Ӧ���󱨴�:' || SQLERRM;
      GOTO �˳�;
  END;
  <<�˳�>>
-- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);
  RETURN;
END PR_������ͨ_�ɷѼ�¼��ѯ;
/

prompt
prompt Creating procedure PR_������ͨ_�ɷ���ϸ��ѯ
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_�ɷ���ϸ��ѯ(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2,
                                           STR_���ܱ��� IN VARCHAR2,
                                           LOB_��Ӧ���� OUT CLOB,
                                           INT_����ֵ   OUT INTEGER,
                                           STR_������Ϣ OUT VARCHAR2) IS

  --�����������
  STR_ҽԺID       VARCHAR2(50);
  STR_�û�Ժ��ID   VARCHAR2(50);
  STR_�ɷѵ�ΨһID VARCHAR2(50);

  STR_SQL      VARCHAR2(1000);
  STR_�û����� VARCHAR2(50);
  STR_�ܽ��   VARCHAR2(50);
  STR_Ӧ����� VARCHAR2(50);
  STR_ʵ����� VARCHAR2(50);

BEGIN
  BEGIN
    STR_ҽԺID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_�û�Ժ��ID   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_PATIENT_ID');
    STR_�ɷѵ�ΨһID := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_SEQUENCE');
  
    IF STR_�ɷѵ�ΨһID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ɷѵ�ΨһID';
      GOTO �˳�;
    END IF;
  
    BEGIN
      SELECT T.����ID, T.�ܽ��, T.Ӧ�����, T.ʵ�����
        INTO STR_�û�Ժ��ID, STR_�ܽ��, STR_Ӧ�����, STR_ʵ�����
        FROM ������ͨ_���� T
       WHERE T.��ˮ�� = STR_�ɷѵ�ΨһID;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 300201;
        STR_������Ϣ := '��Ч�ĽɷѼ�¼';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := 'ϵͳ�쳣' || SQLERRM;
        GOTO �˳�;
    END;
  
    BEGIN
      SELECT T.����
        INTO STR_�û�����
        FROM ������ͨ_�û���Ϣ T
       WHERE T.ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND T.ҽԺ���� = STR_ҽԺID
         AND T.����ID = STR_�û�Ժ��ID;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 300201;
        STR_������Ϣ := '��Ч�ĽɷѼ�¼';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := 'ϵͳ�쳣' || SQLERRM;
        GOTO �˳�;
    END;
  
    STR_SQL      := ' SELECT TT.��Ŀ���� AS DETAIL_TYPE,
                     TT.��Ŀ���� AS DETAIL_NAME,
                     TT.��ˮ�� AS DETAIL_ID,
                     TT.��λ AS DETAIL_UNIT,
                     TT.���� AS DETAIL_COUNT,
                     TT.���� AS DETAIL_PRICE,
                     TT.��� AS DETAIL_SPEC,
                     TT.�ܽ�� AS DETAIL_AMOUT,
                     ''0'' AS DETAIL_MI
                FROM ������ͨ_���� T, ������ͨ_������ϸ TT
               WHERE T.ҽԺ������ = TT.������
                 AND T.��ˮ�� = STR_�ɷѵ�ΨһID';
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', 'PAY_DETAIL_LIST');
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����   => LOB_��Ӧ����,
                                STR_���λ��   => 'PAY_DETAIL_LIST',
                                STR_��ӽڵ��� => 'USER_NAME',
                                STR_��ӽڵ�ֵ => STR_�û�����);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����   => LOB_��Ӧ����,
                                STR_���λ��   => 'PAY_DETAIL_LIST',
                                STR_��ӽڵ��� => 'HOSP_PATIENT_ID',
                                STR_��ӽڵ�ֵ => STR_�û�Ժ��ID);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����   => LOB_��Ӧ����,
                                STR_���λ��   => 'PAY_DETAIL_LIST',
                                STR_��ӽڵ��� => 'MEDICAL_INSURANNCE_TYPE',
                                STR_��ӽڵ�ֵ => '�Է�');
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����   => LOB_��Ӧ����,
                                STR_���λ��   => 'PAY_DETAIL_LIST',
                                STR_��ӽڵ��� => 'PAY_TOTAL_FEE',
                                STR_��ӽڵ�ֵ => STR_�ܽ��);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����   => LOB_��Ӧ����,
                                STR_���λ��   => 'PAY_DETAIL_LIST',
                                STR_��ӽڵ��� => 'PAY_BEHOOVE_FEE',
                                STR_��ӽڵ�ֵ => STR_Ӧ�����);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����   => LOB_��Ӧ����,
                                STR_���λ��   => 'PAY_DETAIL_LIST',
                                STR_��ӽڵ��� => 'PAY_ACTUAL_FEE',
                                STR_��ӽڵ�ֵ => STR_ʵ�����);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����   => LOB_��Ӧ����,
                                STR_���λ��   => 'PAY_DETAIL_LIST',
                                STR_��ӽڵ��� => 'PAY_MI_FEE',
                                STR_��ӽڵ�ֵ => '0');
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����   => LOB_��Ӧ����,
                                STR_���λ��   => 'PAY_DETAIL_LIST',
                                STR_��ӽڵ��� => 'RECEIPT_ID',
                                STR_��ӽڵ�ֵ => '');
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    ELSE
      INT_����ֵ   := 300201;
      STR_������Ϣ := '��Ч�ĽɷѼ�¼';
    END IF;
  
  END;

  <<�˳�>>
-- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);
  RETURN;

END PR_������ͨ_�ɷ���ϸ��ѯ;
/

prompt
prompt Creating procedure PR_������ͨ_������Դ����
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_������Դ����(STR_������� IN VARCHAR2,
                                         LOB_��Ӧ���� OUT CLOB,
                                         RES_CODE     OUT INTEGER,
                                         RES_MSG      OUT VARCHAR2) IS

  STR_SQL          VARCHAR2(1000);
  STR_ҽԺID       VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
  STR_��Դ����ID   VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'LOCK_ID');
BEGIN
  BEGIN
  
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
    
      RES_CODE := 0;
      RES_MSG  := '���׳ɹ�';
    ELSE
      RES_CODE := 200101;
      RES_MSG  := '���Ҳ����ڣ�δ��ѯ�����Ҽ�¼';
    END IF;
  
  END;

END PR_������ͨ_������Դ����;
/

prompt
prompt Creating procedure PR_������ͨ_���Ҳ�ѯ
prompt ===============================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_���Ҳ�ѯ(STR_������� IN VARCHAR2,
                                         STR_ƽ̨��ʶ IN VARCHAR2,
                                         STR_���ܱ��� IN VARCHAR2,
                                         LOB_��Ӧ���� OUT CLOB,
                                         INT_����ֵ   OUT INTEGER,
                                         STR_������Ϣ OUT VARCHAR2) IS

  --�����������
  STR_ҽԺID VARCHAR2(50);
  STR_����ID VARCHAR2(50);

  STR_SQL VARCHAR2(1000);

BEGIN
  BEGIN
    --���������������  
    STR_ҽԺID := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_����ID := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DEPT_ID');
  
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '����ID����Ϊ��';
      GOTO �˳�;
    END IF;
  
    --��ҵ����ӦSQL��
    STR_SQL := 'select ���ұ��� as DEPT_ID,
                                      �������� as DEPT_NAME,
                                      �������� as PARENT_ID,
                                      '''' as SORT_ID,
                                      ��ע as "DESC",
                                      �������� as EXPERTISE,
                                      '''' as "LEVEL",
                                      '''' as ADDRESS,
                                      decode(��Ч״̬, ''��Ч'', ''1'', ''2'') as STATUS
                                from ������Ŀ_��������
                                where ��������=' || STR_ҽԺID ||
               ' and ���ұ��� in (select tt.���ұ��� from �������_����һ���Ű�� tt) and (���ұ��� =' ||
               STR_����ID || ' or ' || STR_����ID || '=''-1'' or ' || STR_����ID ||
               '=''0'')';
  
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL    => STR_SQL,
                               STR_����ǩ => 'RES',
                               STR_�б�ǩ => 'DEPT_LIST');
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
      --�����HOST_ID�ڵ㡿
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����   => LOB_��Ӧ����,
                                STR_���λ��   => 'DEPT_LIST',
                                STR_��ӽڵ��� => 'HOST_ID',
                                STR_��ӽڵ�ֵ => STR_ҽԺID);
    
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    ELSE
      INT_����ֵ   := 200101;
      STR_������Ϣ := '���Ҳ����ڣ�δ��ѯ�����Ҽ�¼';
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '��Ӧ�������' || SQLERRM;
      GOTO �˳�;
  END;

  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  RETURN;

END PR_������ͨ_���Ҳ�ѯ;
/

prompt
prompt Creating procedure PR_������ͨ_�Ű��ʱ��ѯ
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_�Ű��ʱ��ѯ(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2,
                                           STR_���ܱ��� IN VARCHAR2,
                                           LOB_��Ӧ���� OUT CLOB,
                                           INT_����ֵ   OUT INTEGER,
                                           STR_������Ϣ OUT VARCHAR2) IS

  --�����������
  STR_ҽԺID   VARCHAR2(50);
  STR_����ID   VARCHAR2(50);
  STR_ҽ��ID   VARCHAR2(50);
  STR_�������� VARCHAR2(50);
  STR_����ʱ�� VARCHAR2(50);

  STR_SQL VARCHAR2(1000);

BEGIN
  BEGIN
  
    --���������������
    STR_ҽԺID   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_����ID   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DEPT_ID');
    STR_ҽ��ID   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DOCTOR_ID');
    STR_�������� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REG_DATE');
    STR_����ʱ�� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'TIME_FLAG');
  
    -- ������У�顿
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '����ID����Ϊ��';
      GOTO �˳�;
    END IF;
    IF STR_ҽ��ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽ��ID����Ϊ��';
      GOTO �˳�;
    END IF;
    IF STR_�������� IS NULL OR FU_����ת����(STR_��������) IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫����Ч�ĳ�������';
      GOTO �˳�;
    END IF;
  
    --����֤�����Ű���Ϣ��
    SELECT COUNT(1)
      INTO INT_����ֵ
      FROM �������_�����Ű��¼
     WHERE �������� = STR_ҽԺID
       AND ���ұ��� = STR_����ID
       AND �Ű����� = TO_DATE(STR_��������, 'yyyy-MM-dd');
  
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := 200401;
      STR_������Ϣ := '���Ҳ�����';
      GOTO �˳�;
    END IF;
  
    --����֤ҽ���Ű���Ϣ��
    SELECT COUNT(1)
      INTO INT_����ֵ
      FROM �������_�����Ű��¼
     WHERE �������� = STR_ҽԺID
       AND ҽ������ = STR_ҽ��ID
       AND �Ű����� = TO_DATE(STR_��������, 'yyyy-MM-dd');
  
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := 200402;
      STR_������Ϣ := 'ҽ��������';
      GOTO �˳�;
    END IF;
  
    STR_SQL := 'select ''4'' as TIME_FLAG,
                       tt.��ʼʱ�� as BEGIN_TIME,
                       tt.����ʱ�� as END_TIME,
                       decode(tt.�޺���, ''-1'', ''99'', tt.�޺���) as TOTAL,
                       decode(tt.�޺���, ''-1'', ''99'', tt.�޺���-tt.�ѹҺ���) as OVER_COUNT,
                       tt.�հ�α�ʶ as REG_ID
                from �������_�����Ű��¼ t, �������_���Ű�ʱ�α� tt
                where t.�������� = tt.��������
                      and t.�Ű���� = tt.�Ű����
                      and t.��¼id = tt.��¼id
                      and t.��������=' || STR_ҽԺID ||
               ' and t.���ұ���=' || STR_����ID || ' and t.ҽ������=' || STR_ҽ��ID ||
               ' and t.�Ű�����=to_date(''' || STR_�������� || ''',''yyyy-MM-dd'')';
  
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', 'TIME_REG_LIST');
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    ELSE
      INT_����ֵ   := 200403;
      STR_������Ϣ := '�Ű಻����';
    END IF;
  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '��Ӧ���󱨴�' || SQLERRM;
      GOTO �˳�;
    
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  RETURN;

END PR_������ͨ_�Ű��ʱ��ѯ;
/

prompt
prompt Creating procedure PR_������ͨ_�Ű���Ϣ��ѯ
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_�Ű���Ϣ��ѯ(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2,
                                           STR_���ܱ��� IN VARCHAR2,
                                           LOB_��Ӧ���� OUT CLOB,
                                           INT_����ֵ   OUT INTEGER,
                                           STR_������Ϣ OUT VARCHAR2) IS

  STR_SQL  VARCHAR2(2000);
  STR_SQL1 VARCHAR2(2000);
  --�����������
  STR_ҽԺID       VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
  STR_����ID       VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DEPT_ID');
  STR_ҽ��ID       VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DOCTOR_ID');
  STR_�Ű࿪ʼ���� VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'START_DATE');
  STR_�Ű�������� VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'END_DATE');

  STR_��ʱҽ��ID   VARCHAR2(50) := '-999';
  STR_��ʱ�������� DATE := TO_DATE('1990-01-01', 'yyyy-MM-dd');

  CURSOR CUR_�Ű���Ϣ IS
    SELECT T.��������,
           T.��¼ID,
           T.�Ű����,
           T.���ұ���,
           T.ҽ������,
           (SELECT A.��Ա����
              FROM ������Ŀ_��Ա���� A
             WHERE A.�������� = T.��������
               AND A.��Ա���� = T.ҽ������) AS ҽ������,
           (SELECT A.ְ��
              FROM ������Ŀ_��Ա���� A
             WHERE A.�������� = T.��������
               AND A.��Ա���� = T.ҽ������) AS ҽ��ְ��,
           T.�Ű�����,
           T.����
      FROM �������_�����Ű��¼ T
     WHERE T.�������� = STR_ҽԺID
       AND T.���ұ��� = STR_����ID
       AND (T.ҽ������ = STR_ҽ��ID OR STR_ҽ��ID = '-1')
       AND T.�Ű����� BETWEEN TO_DATE(STR_�Ű࿪ʼ����, 'yyyy-MM-dd') AND
           TO_DATE(STR_�Ű��������, 'yyyy-MM-dd')
     ORDER BY T.���ұ���, T.ҽ������, T.�Ű�����;

  ROW_�Ű���Ϣ CUR_�Ű���Ϣ%ROWTYPE;

BEGIN

  BEGIN
  
    FOR ROW_�Ű���Ϣ IN CUR_�Ű���Ϣ LOOP
      EXIT WHEN CUR_�Ű���Ϣ%NOTFOUND;
    
      STR_SQL := 'SELECT T.�հ�α�ʶ AS REG_ID,
                       ''4'' AS TIME_FLAG,
                       ''2'' AS REG_STATUS,
                       DECODE(T.�޺���, -1, 99, T.�޺���) AS TOTAL,
                       DECODE(T.�޺���, -1, 99, T.�޺��� - T.�ѹҺ���) AS OVER_COUNT,
                       1 AS REG_LEVEL,
                       T2.�Һŷ� * 100 AS REG_FEE,
                       T2.���� * 100 AS TREAT_FEE,
                       0 AS ISTIME
                  FROM �������_���Ű�ʱ�α� T,
                       �������_�����Ű��¼ T1,
                       ������Ŀ_�Һ�����     T2
                 WHERE T.�������� = T1.��������
                   AND T1.�������� = T2.��������
                   AND T.��¼ID = T1.��¼ID
                   AND T.�Ű���� = T1.�Ű����
                   AND T1.�Һ����ͱ��� = T2.���ͱ���
                   AND T.��������=' || STR_ҽԺID ||
                 ' AND T.��¼ID=''' || ROW_�Ű���Ϣ.��¼ID || ''' AND T.�Ű����=' ||
                 ROW_�Ű���Ϣ.�Ű����;
    
      STR_SQL1 := 'SELECT T1.��¼ID AS REG_ID,
                         ''4'' AS TIME_FLAG,
                         ''2'' AS REG_STATUS,      
                         (select sum(�޺���)
                            from �������_���Ű�ʱ�α�
                           where �������� = T1.��������
                             and ��¼ID = T1.��¼ID
                             and �޺��� > 0) TOTAL,
                         (select sum(�޺���) - sum(�ѹҺ���)
                            from �������_���Ű�ʱ�α�
                           where �������� = T1.��������
                             and ��¼ID = T1.��¼ID
                             and �޺��� > 0) OVER_COUNT,
                         1 AS REG_LEVEL,
                         T2.�Һŷ� * 100 AS REG_FEE,
                         T2.���� * 100 AS TREAT_FEE,
                         1 AS ISTIME
                    FROM �������_�����Ű��¼ T1, ������Ŀ_�Һ����� T2
                   WHERE T1.�������� = T2.��������
                     AND T1.�Һ����ͱ��� = T2.���ͱ���
                   AND T1.��������=' || STR_ҽԺID ||
                  ' AND T1.��¼ID=''' || ROW_�Ű���Ϣ.��¼ID || ''' AND T1.�Ű����=' ||
                  ROW_�Ű���Ϣ.�Ű����;
    
      IF STR_��ʱҽ��ID <> ROW_�Ű���Ϣ.ҽ������ THEN
        STR_��ʱҽ��ID := ROW_�Ű���Ϣ.ҽ������;
        IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
          LOB_��Ӧ���� := LOB_��Ӧ���� || '</REG_DOCTOR_LIST>'; --�Ű�ҽ�����Ͻڵ����
        END IF;
        LOB_��Ӧ���� := LOB_��Ӧ���� || '<REG_DOCTOR_LIST>'; --�Ű�ҽ�����Ͻڵ㿪ʼ
        LOB_��Ӧ���� := LOB_��Ӧ���� || '<DOCTOR_ID>' || ROW_�Ű���Ϣ.ҽ������ ||
                    '</DOCTOR_ID>'; --ҽ��ID
        LOB_��Ӧ���� := LOB_��Ӧ���� || '<NAME>' || ROW_�Ű���Ϣ.ҽ������ || '</NAME>'; --ҽ������
        LOB_��Ӧ���� := LOB_��Ӧ���� || '<JOB_TITLE>' || ROW_�Ű���Ϣ.ҽ��ְ�� ||
                    '</JOB_TITLE>'; --ҽ��ְ��
      
        IF STR_��ʱ�������� <> ROW_�Ű���Ϣ.�Ű����� THEN
          STR_��ʱ�������� := ROW_�Ű���Ϣ.�Ű�����;
          LOB_��Ӧ����     := LOB_��Ӧ���� || '<REG_LIST>'; --�������ڽڵ㿪ʼ
          LOB_��Ӧ����     := LOB_��Ӧ���� || '<REG_DATE>' ||
                          TO_CHAR(ROW_�Ű���Ϣ.�Ű�����, 'yyyy-MM-dd') ||
                          '</REG_DATE>'; --��������
          LOB_��Ӧ����     := LOB_��Ӧ���� || '<REG_WEEKDAY>' || ROW_�Ű���Ϣ.���� ||
                          '</REG_WEEKDAY>'; --�������ڶ�Ӧ����
        
          LOB_��Ӧ���� := LOB_��Ӧ���� ||
                      FU_������ͨ_�õ���Ӧ����(STR_SQL1, 'REG_TIME_LIST', '');
        
          LOB_��Ӧ���� := LOB_��Ӧ���� || '</REG_LIST>'; --�������ڽڵ����
        
        END IF;
      ELSE
        IF STR_��ʱ�������� <> ROW_�Ű���Ϣ.�Ű����� THEN
          STR_��ʱ�������� := ROW_�Ű���Ϣ.�Ű�����;
          LOB_��Ӧ����     := LOB_��Ӧ���� || '<REG_LIST>'; --�������ڽڵ㿪ʼ
          LOB_��Ӧ����     := LOB_��Ӧ���� || '<REG_DATE>' ||
                          TO_CHAR(ROW_�Ű���Ϣ.�Ű�����, 'yyyy-MM-dd') ||
                          '</REG_DATE>'; --��������
          LOB_��Ӧ����     := LOB_��Ӧ���� || '<REG_WEEKDAY>' || ROW_�Ű���Ϣ.���� ||
                          '</REG_WEEKDAY>'; --�������ڶ�Ӧ����
        
          LOB_��Ӧ���� := LOB_��Ӧ���� ||
                      FU_������ͨ_�õ���Ӧ����(STR_SQL1, 'REG_TIME_LIST', '');
        
          LOB_��Ӧ���� := LOB_��Ӧ���� || '</REG_LIST>'; --�������ڽڵ����
        
        END IF;
      END IF;
    
    END LOOP;
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
      LOB_��Ӧ���� := LOB_��Ӧ���� || '</REG_DOCTOR_LIST>'; --�Ű�ҽ�����Ͻڵ����
    
      LOB_��Ӧ���� := '<RES><HOS_ID>' || STR_ҽԺID || '</HOS_ID><DEPT_ID>' ||
                  STR_����ID || '</DEPT_ID>' || LOB_��Ӧ���� || '</RES>';
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    ELSE
      INT_����ֵ   := 200303;
      STR_������Ϣ := '�Ű಻���ڣ�δ��ѯ���Ű���Ϣ';
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '��Ӧ���󱨴�' || SQLERRM;
      GOTO �˳�;
  END;
  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  RETURN;

END PR_������ͨ_�Ű���Ϣ��ѯ;
/

prompt
prompt Creating procedure PR_������ͨ_�Ŷ��б��ѯ
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_�Ŷ��б��ѯ(STR_������� IN VARCHAR2,
                                           LOB_��Ӧ���� OUT CLOB,
                                           RES_CODE     OUT INTEGER,
                                           RES_MSG      OUT VARCHAR2) IS

  STR_SQL          VARCHAR2(1000);
  STR_ҽԺID       VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
  STR_�û�Ժ��ID   VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_PATIENT_ID');
  STR_�û�֤������ VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������,
                                         'PATIENT_IDCARD_TYPE');
  STR_�û�֤������ VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PATIENT_IDCARD_NO');
  STR_�û�������   VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PATIENT_CARD_TYPE');
  STR_�û�����     VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PATIENT_CARD_NO');
  STR_�û�����     VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PATIENT_NAME');
  STR_�û��Ա�     VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PATIENT_SEX');
  STR_�û�����     VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PATIENT_AGE');

BEGIN
  BEGIN
  
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
    
      RES_CODE := 0;
      RES_MSG  := '���׳ɹ�';
    ELSE
      RES_CODE := 300201;
      RES_MSG  := '��Ч�ĽɷѼ�¼';
    END IF;
  
  END;

END PR_������ͨ_�Ŷ��б��ѯ;
/

prompt
prompt Creating procedure PR_������ͨ_��ͨ���鱨���ѯ
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_��ͨ���鱨���ѯ(STR_������� IN VARCHAR2,
                                             STR_ƽ̨��ʶ IN VARCHAR2,
                                             STR_���ܱ��� IN VARCHAR2,
                                             LOB_��Ӧ���� OUT CLOB,
                                             INT_����ֵ   OUT INTEGER,
                                             STR_������Ϣ OUT VARCHAR2) IS

  --�����������
  STR_ҽԺID       VARCHAR2(50);
  STR_���鱨�浥�� VARCHAR2(50);
  STR_�û�Ժ��ID   VARCHAR2(50);

  STR_SQL          VARCHAR2(4000);
  LOB_��Ӧ������ʱ CLOB;

BEGIN
  BEGIN
    --���������������
    STR_ҽԺID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_���鱨�浥�� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REPORT_ID');
    STR_�û�Ժ��ID   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_PATIENT_ID');
  
    STR_SQL := 'SELECT S.����ID AS HOSP_PATIENT_ID,
                           '''' as PATIENT_IDCARD_TYPE,
                           (select A.���֤��
                              from ������Ŀ_������Ϣ A
                             where A.�������� = S.��������
                               and A.����ID = S.����ID) as PATIENT_IDCARD_NO,
                           '''' as PATIENT_CARD_TYPE,
                           '''' as PATIENT_CARD_NO,
                           (select A.����
                              from ������Ŀ_������Ϣ A
                             where A.�������� = S.��������
                               and A.����ID = S.����ID) as PATIENT_NAME,
                           (select A.�Ա�
                              from ������Ŀ_������Ϣ A
                             where A.�������� = S.��������
                               and A.����ID = S.����ID) as PATIENT_SEX,
                           (select A.����
                              from ������Ŀ_������Ϣ A
                             where A.�������� = S.��������
                               and A.����ID = S.����ID) as PATIENT_AGE,
                           '''' as VISIT_NUMBER,
                           ''ҽ��'' as MEDICAL_INSURANNCE_TYPE,
                           J.������ as DIAGNOSIS,
                           S.��Ŀ���� as ITEM_NAME,
                           J.������ as SPECIMEN_ID,                                           
                           to_char(J.����ʱ��, ''yyyy-MM-dd hh24:mi:ss'') as REPORT_TIME,
                           (select B.��������
                              from ������Ŀ_�������� B
                             where B.�������� = S.��������
                               and B.���ұ��� = S.ִ�п��ұ���) as DEPT_NAME,
                           (select C.��Ա����
                              from ������Ŀ_��Ա���� C
                             where C.�������� = S.��������
                               and C.��Ա���� = S.ҽ������) as DOCTOR_NAME,
                           (select C.��Ա����
                              from ������Ŀ_��Ա���� C
                             where C.�������� = S.��������
                               and C.��Ա���� = J.���ҽ������) as REVIEW_NAME,
                           to_char(J.���ʱ��, ''yyyy-MM-dd hh24:mi:ss'') as REVIEW_TIME,
                           '''' as REMARK
                      FROM ������_���� S, ������_��� J
                     WHERE S.�������� = J.��������
                       AND S.���뵥ID = J.���뵥ID     
                       AND S.��������=''' || STR_ҽԺID || '''
                       AND J.���浥�� =''' || STR_���鱨�浥�� || '''';
  
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', 'REPORT_INFO');
  
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
    
      STR_SQL := 'SELECT M.ϸ������ AS CHECK_NAME,
                     M.ϸ��ֵ AS "RESULT",
                     M.��λ AS UNIT,
                     DECODE(M.����, ''L'', ''ƫ��'', ''H'', ''ƫ��'', ''����'') as NORMAL_FLAG,
                     M.�ο�ֵ���� AS REFERENCE_VALUE,
                     '''' as "DESC"
                FROM ������_���_��ϸ M
               WHERE M.�������� = ''' || STR_ҽԺID || '''
                 AND M.���浥ID = ''' || STR_���鱨�浥�� || '''';
      --829
      LOB_��Ӧ������ʱ := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'CHECK_LIST', 'DETAIL');
    
      --�ϲ�����XML
      SELECT INSERTCHILDXMLAFTER(XMLTYPE(LOB_��Ӧ����), '/RES', 'REPORT_INFO', XMLTYPE(LOB_��Ӧ������ʱ).EXTRACT('/CHECK_LIST')).GETCLOBVAL()
        INTO LOB_��Ӧ����
        FROM DUAL;
        
         --�����HOST_ID�ڵ㡿
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����   => LOB_��Ӧ����,
                                STR_���λ��   => 'REPORT_INFO',
                                STR_��ӽڵ��� => 'HOST_ID',
                                STR_��ӽڵ�ֵ => STR_ҽԺID);
    
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    ELSE
      INT_����ֵ   := 800201;
      STR_������Ϣ := '���/���鱨�浥�Ų�����';
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
    
  END;
  -- ���쳣�˳���
  <<�˳�>>
   -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  RETURN;

END PR_������ͨ_��ͨ���鱨���ѯ;
/

prompt
prompt Creating procedure PR_������ͨ_ȡ��
prompt =============================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_ȡ��(STR_������� IN VARCHAR2,
                                       STR_ƽ̨��ʶ IN VARCHAR2,
                                       STR_���ܱ��� IN VARCHAR2,
                                       LOB_��Ӧ���� OUT CLOB,
                                       INT_����ֵ   OUT INTEGER,
                                       STR_������Ϣ OUT VARCHAR2) IS

  --�����������
  STR_ҽԺID     VARCHAR2(50);
  STR_ƽ̨������ VARCHAR2(50);

  --��ҵ�������
  STR_SQL      VARCHAR2(1000);
  DAT_ϵͳʱ�� DATE;
  STR_ԤԼ���� VARCHAR2(50);

  STR_����ID       VARCHAR2(50);
  STR_�Ű�ID       VARCHAR2(50);
  STR_�Һ����     VARCHAR2(50);
  STR_�Һŵ���     VARCHAR2(50);
  STR_���ﲡ����   VARCHAR2(50);
  NUM_�Һŷ�       NUMBER(10, 4);
  NUM_����       NUMBER(10, 4);
  NUM_�ܷ���       NUMBER(10, 4);
  STR_���ù������ VARCHAR2(50);

  STR_�Һſ��ұ��� VARCHAR2(50);
  STR_�Һſ���λ�� VARCHAR2(50);
  STR_�Һ�ҽ������ VARCHAR2(50);
  STR_�Һ����ͱ��� VARCHAR2(50);

  STR_�������ͱ��� VARCHAR2(50);
  STR_������������ VARCHAR2(50);
  STR_����״̬     VARCHAR2(50);
  STR_�Һ���Դ     VARCHAR2(50);
  STR_���ʽ     VARCHAR2(50);

  DAT_ԤԼ��ʼʱ�� DATE;
  DAT_ԤԼ����ʱ�� DATE;
  STR_�հ�α�ʶ   VARCHAR2(50);

BEGIN
  BEGIN
  
    --���������������
    STR_ҽԺID     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_ƽ̨������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ORDER_ID');
  
    -- ��������ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    IF STR_ƽ̨������ IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ƽ̨�����Ų���Ϊ��';
      GOTO �˳�;
    END IF;
  
    -- ����ȡԤԼ���š�
    BEGIN
      SELECT ��������
        INTO STR_ԤԼ����
        FROM ������ͨ_����
       WHERE ҽԺ���� = STR_ҽԺID
         AND ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ƽ̨������ = STR_ƽ̨������
         AND ����״̬ = '��֧��';
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 201101;
        STR_������Ϣ := '�ҺŶ���������';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'ϵͳ����' || SQLERRM;
        GOTO �˳�;
    END;
  
    -- ��ȡԤԼ�Һ�����
    BEGIN
      SELECT �Һſ��ұ���,
             �Һſ���λ��,
             �Һ�ҽ������,
             �Һ����ͱ���,
             ����ID,
             �Ű�ID,
             �Һŷ�,
             ����,
             �Һŷ� + ����,
             �������,
             ԤԼʱ�ο�ʼ,
             ԤԼʱ�ν���,
             �հ�α�ʶ
        INTO STR_�Һſ��ұ���,
             STR_�Һſ���λ��,
             STR_�Һ�ҽ������,
             STR_�Һ����ͱ���,
             STR_����ID,
             STR_�Ű�ID,
             NUM_�Һŷ�,
             NUM_����,
             NUM_�ܷ���,
             STR_���ù������,
             DAT_ԤԼ��ʼʱ��,
             DAT_ԤԼ����ʱ��,
             STR_�հ�α�ʶ
        FROM �������_ԤԼ�Һ� G
       WHERE G.�������� = STR_ҽԺID
         AND G.����ID = STR_ԤԼ����
         AND G.ȥ���־ = 'ԤԼ'
         AND G.֧����־ = '��'
         AND TO_CHAR(G.ԤԼʱ��, 'yyyymmdd') = TO_CHAR(SYSDATE, 'yyyymmdd');
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'δ�ҵ���Ч��ԤԼ��¼��';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
  
    -- ���������ﲡ���š�
    PR_����_ȡ��ҵ������(STR_��������   => STR_ҽԺID,
                  STR_���������� => '���ﲡ����',
                  STR_���ز����� => STR_���ﲡ����,
                  INT_����ֵ     => INT_����ֵ,
                  STR_������Ϣ   => STR_������Ϣ);
    IF INT_����ֵ <> 1 THEN
      STR_������Ϣ := '�������ﲡ����ʧ��,ԭ��:' + STR_������Ϣ;
      GOTO �˳�;
    END IF;
  
    -- �������Һ���š�
    PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '26',
                PRM_��������   => STR_ҽԺID,
                PRM_��������   => '1',
                PRM_����Ψһ�� => STR_�Һ����,
                PRM_ִ�н��   => INT_����ֵ,
                PRM_������Ϣ   => STR_������Ϣ);
    IF INT_����ֵ <> 0 THEN
      STR_������Ϣ := '�����Һ����ʧ��!';
      GOTO �˳�;
    END IF;
  
    -- �������Һŵ��š�
    SELECT FU_����_��ȡ��ǰƱ�ݺ�(STR_ҽԺID, STR_ƽ̨��ʶ, '4')
      INTO STR_�Һŵ���
      FROM DUAL;
  
    IF STR_�Һŵ��� = '�뵽����������Ʊ��' THEN
      STR_������Ϣ := '�ò���Ա�޹Һŵ���,��֪ͨ����������Ʊ��!';
      GOTO �˳�;
    END IF;
  
    -- ���ɹҺż�¼
  
    BEGIN
      INSERT INTO �������_�ҺŵǼ�
        (��������,
         ����ID,
         ���ﲡ����,
         �Һ����,
         �Һŵ���,
         �Һſ��ұ���,
         �Һſ���λ��,
         �Һ�ҽ������,
         �Һ����ͱ���,
         ����Ա����,
         �Һ�ʱ��,
         �˺ű�־,
         �������,
         �Һŷ�,
         ������,
         ����,
         ������,
         �ܷ���,
         �Ƿ���,
         ���,
         ����״̬,
         �������ͱ���,
         �Һ���Դ,
         ������ұ���,
         ����ҽ������,
         �������,
         �Ը����,
         �Һ�������,
         ��֧�����,
         ԤԼ��ʼʱ��,
         ԤԼ����ʱ��,
         �հ�α�ʶ,
         �Ű�ID)
      VALUES
        (STR_ҽԺID,
         STR_����ID,
         STR_���ﲡ����,
         STR_�Һ����,
         STR_�Һŵ���,
         STR_�Һſ��ұ���,
         STR_�Һſ���λ��,
         STR_�Һ�ҽ������,
         STR_�Һ����ͱ���,
         STR_ƽ̨��ʶ,
         DAT_ϵͳʱ��,
         '��',
         STR_���ù������,
         NUM_�Һŷ�,
         0,
         NUM_����,
         0,
         NUM_�ܷ���,
         '��',
         '0',
         STR_����״̬,
         STR_�������ͱ���,
         STR_�Һ���Դ,
         STR_�Һſ��ұ���,
         STR_�Һ�ҽ������,
         0,
         NUM_�ܷ���,
         '-1',
         0,
         DAT_ԤԼ��ʼʱ��,
         DAT_ԤԼ����ʱ��,
         STR_�հ�α�ʶ,
         STR_�Ű�ID);
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '����Һ�����ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      -- ������֧���¼
      INSERT INTO �������_��֧��
        (��������,
         ���ݺ�,
         �շѽ��,
         ���ʽ,
         ҵ������,
         ����Ա����,
         ����Ա����,
         �շ�ʱ��,
         �Һ����,
         ��Ʊ���,
         �Һ��շѱ�־,
         �������ͱ���,
         ������������)
      VALUES
        (STR_ҽԺID,
         STR_�Һŵ���,
         NUM_�ܷ���,
         STR_���ʽ,
         '�Һ�',
         STR_ƽ̨��ʶ,
         STR_ƽ̨��ʶ,
         SYSDATE,
         STR_�Һ����,
         STR_�Һ����,
         '�Һ�',
         STR_�������ͱ���,
         STR_������������);
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '������֧������ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      -- ����ԤԼ״̬
      UPDATE �������_ԤԼ�Һ�
         SET ȥ���־ = '����',
             �Һ���� = STR_�Һ����,
             ȡ��ʱ�� = DAT_ϵͳʱ��
       WHERE �������� = STR_ҽԺID
         AND ����ID = STR_ԤԼ����;
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '����ԤԼ״̬ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      -- ���¶���
      UPDATE ������ͨ_����
         SET ���ﲡ���� = STR_���ﲡ����
       WHERE �������� = STR_ԤԼ����
         AND ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ҽԺ���� = STR_ҽԺID
         AND ƽ̨������ = STR_ƽ̨������
         AND �������� = 'ԤԼ�Һ�';
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '���¶���״̬ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      STR_SQL := '';
      IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
      
        INT_����ֵ   := 0;
        STR_������Ϣ := '���׳ɹ�';
      ELSE
        INT_����ֵ   := 200101;
        STR_������Ϣ := '���Ҳ����ڣ�δ��ѯ�����Ҽ�¼';
      END IF;
      
      COMMIT;
      RETURN;
      
    EXCEPTION
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := STR_������Ϣ || SQLERRM;
        GOTO �˳�;
      
    END;
  
  END;
  <<�˳�>>
-- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);
  ROLLBACK;
  RETURN;

END PR_������ͨ_ȡ��;
/

prompt
prompt Creating procedure PR_������ͨ_ȡ���Һ�
prompt ===============================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_ȡ���Һ�(STR_������� IN VARCHAR2,
                                         STR_ƽ̨��ʶ IN VARCHAR2,
                                         STR_���ܱ��� IN VARCHAR2,
                                         LOB_��Ӧ���� OUT CLOB,
                                         INT_����ֵ   OUT INTEGER,
                                         STR_������Ϣ OUT VARCHAR2) IS

  --�����������
  STR_ҽԺID     VARCHAR2(50);
  STR_ƽ̨������ VARCHAR2(50);
  STR_ҽԺ������ VARCHAR2(50);
  STR_ȡ��ʱ��   VARCHAR2(50);
  STR_ȡ��ԭ��   VARCHAR2(50);

  --��ҵ�������
  STR_ԤԼ���� varchar2(50);
  STR_����״̬ varchar2(50);

BEGIN

  --�����������
  STR_ҽԺID     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
  STR_ƽ̨������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ORDER_ID');
  STR_ҽԺ������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_ORDER_ID');
  STR_ȡ��ʱ��   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'CANCEL_DATE');
  STR_ȡ��ԭ��   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'CANCEL_REMARK');
  --����֤���ݡ�
  if STR_ƽ̨������ is null then
    INT_����ֵ   := -1;
    STR_������Ϣ := 'ƽ̨�����Ų���Ϊ��';
    GOTO �˳�;
  end if;

  IF STR_ȡ��ʱ�� IS NULL OR FU_����ת����(STR_ȡ��ʱ��) IS NULL THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := 'ȡ��ʱ���ʽ����ȷ��';
    GOTO �˳�;
  END IF;

  --����֤����״̬��
  BEGIN
    SELECT ��������, ����״̬
      INTO STR_ԤԼ����, STR_����״̬
      FROM ������ͨ_����
     WHERE ҽԺ���� = STR_ҽԺID
       AND ƽ̨������ = STR_ƽ̨������
       AND (ҽԺ������ = STR_ҽԺ������ OR STR_ҽԺ������ is null);
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      INT_����ֵ   := 200901;
      STR_������Ϣ := '�ҺŶ���������';
      GOTO �˳�;
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  IF STR_����״̬ = '��֧��' THEN
  
    INT_����ֵ   := 200902;
    STR_������Ϣ := '�ҺŶ�����֧��';
    GOTO �˳�;
  elsif STR_����״̬ = '��ȡ��' THEN
    INT_����ֵ   := 200805;
    STR_������Ϣ := '�ҺŶ�����ȡ��';
    GOTO �˳�;
  elsif STR_����״̬ = '���˿�' THEN
    INT_����ֵ   := 200806;
    STR_������Ϣ := '�ҺŶ������˿�';
    GOTO �˳�;
  END IF;

  -- �����ܴ���
  BEGIN
  
    -- ����ԤԼ��
    UPDATE �������_ԤԼ�Һ�
       SET ȥ���־ = '����'
     WHERE �������� = STR_ҽԺID
       AND ����ID = STR_ԤԼ����
       AND (ȥ���־ = 'ԤԼ' OR ȥ���־ = 'ռ��');
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '����ԤԼ��ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
    -- ���¶���״̬
    UPDATE ������ͨ_����
       SET ����״̬ = '��ȡ��',
           ȡ��ʱ�� = to_date(STR_ȡ��ʱ��, 'yyyy-MM-dd hh24:mi:ss'),
           ȡ��ԭ�� = STR_ȡ��ԭ��,
           ������   = STR_ƽ̨��ʶ,
           ����ʱ�� = sysdate
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND ƽ̨������ = STR_ƽ̨������
       AND �������� = STR_ԤԼ����
       AND ����״̬ = '��֧��';
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '���¶���ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := STR_������Ϣ || SQLERRM;
      GOTO �˳�;
  END;

  BEGIN
  
    LOB_��Ӧ���� := '<RES></RES>';
  
    INT_����ֵ   := 0;
    STR_������Ϣ := '���׳ɹ�';
  
  END;

  COMMIT;
  RETURN;

  --���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => sysdate,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  ROLLBACK;
  RETURN;

END PR_������ͨ_ȡ���Һ�;
/

prompt
prompt Creating procedure PR_������ͨ_�˿�Һ�
prompt ===============================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_�˿�Һ�(STR_������� IN VARCHAR2,
                                         STR_ƽ̨��ʶ IN VARCHAR2,
                                         STR_���ܱ��� IN VARCHAR2,
                                         LOB_��Ӧ���� OUT CLOB,
                                         INT_����ֵ   OUT INTEGER,
                                         STR_������Ϣ OUT VARCHAR2) IS

  --�����������
  STR_ҽԺID       VARCHAR2(50);
  STR_ƽ̨������   VARCHAR2(50);
  STR_ҽԺ������   VARCHAR2(50);
  STR_ƽ̨�˿�� VARCHAR2(50);
  STR_�˿���ˮ��   VARCHAR2(50);
  STR_�ܽ��       VARCHAR2(50);
  STR_�˿���     VARCHAR2(50);
  STR_�˿�����     VARCHAR2(50);
  STR_�˿�ʱ��     VARCHAR2(50);
  STR_������Ӧ���� VARCHAR2(50);
  STR_������Ӧ���� VARCHAR2(50);
  STR_�˿�ԭ��     VARCHAR2(50);

  STR_ԤԼ����     VARCHAR2(50);
  STR_����״̬     VARCHAR2(50);
  STR_SQL          VARCHAR2(1000);
  STR_ҽԺ�˿�� VARCHAR2(50);
  NUM_ʵ�����     NUMBER(10, 4);

BEGIN
  --���������������
  STR_ҽԺID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
  STR_ƽ̨������   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ORDER_ID');
  STR_ҽԺ������   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_ORDER_ID');
  STR_ƽ̨�˿�� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REFUND_ID');
  STR_�˿���ˮ��   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REFUND_SERIAL_NUM');
  STR_�ܽ��       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'TOTAL_FEE');
  STR_�˿���     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REFUND_FEE');
  STR_�˿�����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REFUND_DATE');
  STR_�˿�ʱ��     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REFUND_TIME');
  STR_������Ӧ���� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REFUND_RES_CODE');
  STR_������Ӧ���� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REFUND_RES_DESC');
  STR_�˿�ԭ��     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REFUND_REMARK');

  --�������˿�š�
  SELECT SYS_GUID() INTO STR_ҽԺ�˿�� FROM DUAL;

  --����֤����״̬��
  BEGIN
    SELECT ��������, ����״̬, ʵ�����
      INTO STR_ԤԼ����, STR_����״̬, NUM_ʵ�����
      FROM ������ͨ_����
     WHERE ҽԺ���� = STR_ҽԺID
       AND ƽ̨������ = STR_ƽ̨������
       AND (ҽԺ������ = STR_ҽԺ������ OR STR_ҽԺ������ IS NULL);
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      INT_����ֵ   := 201001;
      STR_������Ϣ := '�ҺŶ���������';
      GOTO �˳�;
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  IF STR_����״̬ = '��ȡ��' THEN
    INT_����ֵ   := 200805;
    STR_������Ϣ := '�ҺŶ�����ȡ��';
    GOTO �˳�;
  ELSIF STR_����״̬ = '���˿�' THEN
    INT_����ֵ   := 200806;
    STR_������Ϣ := '�ҺŶ������˿�';
    GOTO �˳�;
  END IF;

  IF NUM_ʵ����� <> TO_NUMBER(STR_�˿���) THEN
    INT_����ֵ   := 201003;
    STR_������Ϣ := '�˿����ȷ';
    GOTO �˳�;
  END IF;

  -- ��֤ԤԼ��
  SELECT COUNT(1)
    INTO INT_����ֵ
    FROM �������_ԤԼ�Һ�
   WHERE �������� = STR_ҽԺID
     AND ����ID = STR_ԤԼ����
     AND ȥ���־ = 'ռ��'
     AND ��ʱʱ�� < SYSDATE;

  IF INT_����ֵ > 0 THEN
    INT_����ֵ   := 200803;
    STR_������Ϣ := '�ҺŶ����ѹر�';
    GOTO �˳�;
  END IF;

  -- �����ܴ���
  BEGIN
  
    -- ����ԤԼ��
    UPDATE �������_ԤԼ�Һ�
       SET ȥ���־ = '����'
     WHERE �������� = STR_ҽԺID
       AND ����ID = STR_ԤԼ����
       AND (ȥ���־ = 'ԤԼ' OR ȥ���־ = 'ռ��');
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '����ԤԼ��ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
    -- ���¶���״̬
    UPDATE ������ͨ_����
       SET ����״̬       = '���˿�',
           ƽ̨�˿��     = STR_ƽ̨�˿��,
           ƽ̨�˿���ˮ�� = STR_�˿���ˮ��,
           ƽ̨�˿�ʱ��   = DECODE(STR_�˿�����,
                             NULL,
                             TO_DATE(TO_CHAR(SYSDATE, 'yyyy-MM-dd'),
                                     'yyyy-MM-dd'),
                             TO_DATE(STR_�˿����� || ' ' || STR_�˿�ʱ��,
                                     'yyyy-MM-dd hh24:mi:ss')),
           ҽԺ�˿��     = STR_ҽԺ�˿��,
           �˿��־       = '1', --�ɹ� ƽ̨�˿�
           ������         = STR_ƽ̨��ʶ,
           ����ʱ��       = SYSDATE
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND ƽ̨������ = STR_ƽ̨������
       AND �������� = STR_ԤԼ����;
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '���¶���ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := STR_������Ϣ || SQLERRM;
      GOTO �˳�;
  END;

  BEGIN
  
    STR_SQL := 'select ''' || STR_ƽ̨�˿�� ||
               ''' as HOSP_REFUND_ID, 
                   ''1'' as REFUND_FLAG from dual';
  
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', '');
    INT_����ֵ   := 0;
    STR_������Ϣ := '���׳ɹ�';
  
  END;

  COMMIT;
  RETURN;

  --���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  ROLLBACK;
  RETURN;

END PR_������ͨ_�˿�Һ�;
/

prompt
prompt Creating procedure PR_������ͨ_ϵͳ������ѯ
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_ϵͳ������ѯ(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2,
                                           STR_���ܱ��� IN VARCHAR2,
                                           LOB_��Ӧ���� OUT CLOB,
                                           INT_����ֵ   OUT INTEGER,
                                           STR_������Ϣ OUT VARCHAR2) IS

  --�����������
  STR_ҽԺID       VARCHAR2(50);
  STR_��������     VARCHAR2(50);
  STR_�˵�����     VARCHAR2(50);
  STR_ҳ����       VARCHAR2(50);
  STR_ÿҳ��¼���� VARCHAR2(50);

  STR_SQL1 VARCHAR2(1000);
  STR_SQL2 VARCHAR2(1000);
  STR_SQL  VARCHAR2(3000);

BEGIN
  BEGIN
    --���������������
    STR_ҽԺID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_��������     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'BILL_DATE');
    STR_�˵�����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'BILL_TYPE'); --1���� 2֧��  3�˿�
    STR_ҳ����       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAGE_CURRENT');
    STR_ÿҳ��¼���� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAGE_SIZE');
  
    IF STR_�������� IS NULL OR FU_����ת����(STR_��������) IS NULL THEN
      INT_����ֵ   := 0;
      STR_������Ϣ := '��������ȷ�ļ�������';
      GOTO �˳�;
    END IF;
  
    STR_SQL1 := 'select ''1'' as BUSS_TYPE,
                       t.ҽԺ֧���� as TRADE_ORDER_NO,
                       t.ƽ̨������ as ORDER_NO_12320,
                       t.ƽ̨������ˮ�� as TRANSACTION_ID,
                       t.֧������ as TRADE_CHANNEL_ID,
                       t.ʵ����� as TRADE_AMOUNT,
                       t.ƽ̨����ʱ�� as TRADE_TIME,
                       ''1'' as FEE_TYPE
                  from ������ͨ_���� t
                 where t.����״̬ = ''��֧��'' and to_date(t.ƽ̨����ʱ��,''yyyy-MM-dd'')=to_date(''' ||
                STR_�������� || ''',''yyyy-MM-dd'')';
  
    STR_SQL2 := 'select ''2'' as BUSS_TYPE,
                       t.ҽԺ�˿�� as TRADE_ORDER_NO,
                       t.ƽ̨������ as ORDER_NO_12320,
                       t.ƽ̨�˿���ˮ�� as TRANSACTION_ID,
                       t.֧������ as TRADE_CHANNEL_ID,
                       t.�˿��� as TRADE_AMOUNT,
                       t.ƽ̨�˿�ʱ�� as TRADE_TIME,
                       ''1'' as FEE_TYPE
                  from ������ͨ_���� t
                 where t.����״̬ = ''���˿�'' and to_date(t.ƽ̨�˿�ʱ��,''yyyy-MM-dd'')=to_date(''' ||
                STR_�������� || ''',''yyyy-MM-dd'')';
    IF STR_�˵����� = '2' THEN
      STR_SQL := STR_SQL1;
    ELSIF STR_�˵����� = '3' THEN
      STR_SQL := STR_SQL2;
    ELSE
      STR_SQL := STR_SQL1 || ' union all ' || STR_SQL2;
    END IF;
  
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', 'ORDER_LIST');
  
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
    
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    ELSE
      INT_����ֵ   := 900301;
      STR_������Ϣ := 'δ��ѯ������';
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ӧ���󱨴�:' || SQLERRM;
      GOTO �˳�;
    
  END;

  <<�˳�>>
-- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  RETURN;

END PR_������ͨ_ϵͳ������ѯ;
/

prompt
prompt Creating procedure PR_������ͨ_ҽ����ѯ
prompt ===============================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_ҽ����ѯ(STR_������� IN VARCHAR2,
                                         STR_ƽ̨��ʶ IN VARCHAR2,
                                         STR_���ܱ��� IN VARCHAR2,
                                         LOB_��Ӧ���� OUT CLOB,
                                         INT_����ֵ   OUT INTEGER,
                                         STR_������Ϣ OUT VARCHAR2) IS

  --�����������
  STR_ҽԺID VARCHAR2(50);
  STR_����ID VARCHAR2(50);
  STR_ҽ��ID VARCHAR2(50);

  STR_SQL VARCHAR2(2000);

BEGIN
  BEGIN
    --���������������  
    STR_ҽԺID := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_����ID := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DEPT_ID'); --  -1��ѯ���п���
    STR_ҽ��ID := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DOCTOR_ID'); --  -1��ѯ����������ҽ��
  
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '����ID����Ϊ��';
      GOTO �˳�;
    END IF;
    IF STR_ҽ��ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽ��ID����Ϊ��';
      GOTO �˳�;
    END IF;
  
    IF STR_����ID <> '-1' THEN
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM �������_����һ���Ű��
       WHERE �������� = STR_ҽԺID
         AND ���ұ��� = STR_����ID;
    
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 200201;
        STR_������Ϣ := '���Ҳ�����';
        GOTO �˳�;
      END IF;
    END IF;
  
    --��ҵ����ӦSQL��
    STR_SQL := 'select tt.���ұ��� as DEPT_ID,
                       t.��Ա����  as DOCTOR_ID,
                       t.��Ա����  as "NAME",
                       t.���֤��  as IDCARD,
                       t.���˼��  as "DESC",
                       t.ר���س�1 as SPECIAL,
                       t.ְ�� as JOB_TITLE,
                       (select a.����*100 from ������Ŀ_�Һ����� a where a.���ͱ���=(select b.�Һ����ͱ��� from �������_����һ���Ű�� b where b.ҽ������=t.��Ա���� and rownum=1)) as REG_FEE,    
                       decode(t.��Ч״̬,''��Ч'',''1'',''2'') as STATUS,
                       t.�Ա���� as SEX,
                       to_char(t.��������,''yyyy-MM-dd'') as BIRTHDAY,
                       t.�ֻ����� as MOBILE,
                       t.�칫�ҵ绰���� as TEL       
                  from ������Ŀ_��Ա���� t, ������Ŀ_��Ա�����б� tt
                 where t.��Ա���� = tt.��Ա����
                   and tt.ɾ����־ = ''0''
                   and tt.���ұ��� in (select t1.���ұ��� from �������_����һ���Ű�� t1)
                   and t.��Ա���� in (select t1.ҽ������ from �������_����һ���Ű�� t1)
                   and  t.��������=' || STR_ҽԺID ||
               ' and (tt.���ұ��� =' || STR_����ID || 'or ''-1''=' || STR_����ID || ') 
                   and (t.��Ա���� =' || STR_ҽ��ID || 'or ''-1''=' ||
               STR_ҽ��ID || ')';
  
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL    => STR_SQL,
                               STR_����ǩ => 'RES',
                               STR_�б�ǩ => 'DOCTOR_LIST');
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
      --�����HOST_ID�ڵ㡿  
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����   => LOB_��Ӧ����,
                                STR_���λ��   => 'DOCTOR_LIST',
                                STR_��ӽڵ��� => 'HOST_ID',
                                STR_��ӽڵ�ֵ => STR_ҽԺID);
    
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    ELSE
      INT_����ֵ   := 200202;
      STR_������Ϣ := 'ҽ�������ڣ�δ��ѯ��ҽ����¼';
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '������Ӧ����:' || SQLERRM;
      GOTO �˳�;
  END;
  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  RETURN;
END PR_������ͨ_ҽ����ѯ;
/

prompt
prompt Creating procedure PR_������ͨ_ҽ���������ݲ�ѯ
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_ҽ���������ݲ�ѯ(STR_������� IN VARCHAR2,
                                             STR_ƽ̨��ʶ IN VARCHAR2,
                                             STR_���ܱ��� IN VARCHAR2,
                                             LOB_��Ӧ���� OUT CLOB,
                                             INT_����ֵ   OUT INTEGER,
                                             STR_������Ϣ OUT VARCHAR2) IS

  --�����������
  STR_ҽԺID       VARCHAR2(50);
  STR_��������     VARCHAR2(50);
  STR_����ID       VARCHAR2(50);
  STR_ҽ��ID       VARCHAR2(50);
  STR_����ͳ������ VARCHAR2(50);
  STR_��ǰҳ��     VARCHAR2(50);
  STR_ÿҳ����     VARCHAR2(50);

   STR_SQL VARCHAR2(4000);

BEGIN
  BEGIN
    --���������������
    STR_ҽԺID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_��������     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'AREA_NAME');
    STR_����ID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DEPT_ID');
    STR_ҽ��ID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DOCTOR_ID');
    STR_����ͳ������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'COUNT_DATE');
    STR_��ǰҳ��     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAGE_CURRENT');
    STR_ÿҳ����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAGE_SIZE');
  
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '����ID����Ϊ��';
      GOTO �˳�;
    END IF;
    IF STR_ҽ��ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ҽ��ID����Ϊ��';
      GOTO �˳�;
    END IF;
    IF STR_����ͳ������ IS NULL OR FU_����ת����(STR_����ͳ������) IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч��ͳ������';
      GOTO �˳�;
    END IF;
  
    STR_SQL := 'SELECT T.��Ա���� AS DOCTOR_ID,                  
                  (SELECT COUNT(1)
                      FROM �������_ԤԼ�Һ� A
                     WHERE A.�Һſ��ұ��� = T.���ұ���
                       AND A.�Һ�ҽ������ = T.��Ա����
                       AND A.��������=' || STR_ҽԺID || '
                       AND A.֧����־ = ''��''
                       AND A.ȥ���־ = ''ԤԼ''
                       AND A.ԤԼʱ�� = TO_DATE(''' || STR_����ͳ������ ||
               ''', ''yyyy-MM-dd'') + 1) AS NEXT_COUNT,
                  (SELECT COUNT(1)
                      FROM �������_ԤԼ�Һ� A
                     WHERE A.�Һſ��ұ��� = T.���ұ���
                       AND A.�Һ�ҽ������ = T.��Ա����
                       AND A.��������=' || STR_ҽԺID || '
                       AND A.ԤԼʱ�� = TO_DATE(''' || STR_����ͳ������ ||
               ''', ''yyyy-MM-dd'')) AS BOOK_COUNT,
                  (SELECT COUNT(1)
                      FROM �������_�ҺŵǼ� A
                     WHERE A.�Һſ��ұ��� = T.���ұ���
                       AND A.�Һ�ҽ������ = T.��Ա����
                       AND A.��������=' || STR_ҽԺID || '
                       AND A.�Һ�ʱ�� = TO_DATE(''' || STR_����ͳ������ ||
               ''', ''yyyy-MM-dd'')) AS REG_COUNT,
                  (SELECT COUNT(1)
                      FROM �������_ԤԼ�Һ� A
                     WHERE A.�Һſ��ұ��� = T.���ұ���
                       AND A.�Һ�ҽ������ = T.��Ա����
                       AND A.��������=' || STR_ҽԺID || '
                       AND A.֧����־ = ''��''
                       AND A.ȥ���־ = ''����''
                       AND A.ԤԼʱ�� = TO_DATE(''' || STR_����ͳ������ ||
               ''', ''yyyy-MM-dd'')) AS RECEIVE_BOOK,
                   (SELECT COUNT(1)
                      FROM �������_�ҺŵǼ� A
                     WHERE A.������ұ��� = T.���ұ���
                       AND A.����ҽ������ = T.��Ա����
                       AND A.��������=' || STR_ҽԺID || '
                       AND A.����״̬ IN (''���ڽ���'', ''��ɽ���'')
                       AND A.�Һ�ʱ�� = TO_DATE(''' || STR_����ͳ������ ||
               ''', ''yyyy-MM-dd'')) AS RECEIVE_REG
              FROM ������Ŀ_��Ա�����б� T
             WHERE  T.��������=''' || STR_ҽԺID ||
               ''' AND T.ɾ����־ = ''0''
             AND T.���ұ���=' || STR_����ID || ' AND (��Ա����=
             ' || STR_ҽ��ID || ' OR ''-1''=' || STR_ҽ��ID || ')';
  
    LOB_��Ӧ���� := fu_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', 'DOCTOR_INFO');
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
    
      select count(1)
        into INT_����ֵ
        FROM ������Ŀ_��Ա�����б� T
       WHERE T.�������� = STR_ҽԺID
         AND T.ɾ����־ = '0'
         AND T.���ұ��� = STR_����ID
         AND (��Ա���� = STR_ҽ��ID OR STR_ҽ��ID = '-1');
    
      LOB_��Ӧ���� := fu_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'DOCTOR_INFO',
                                'COUNT',
                                INT_����ֵ);
    
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    ELSE
      INT_����ֵ   := 202001;
      STR_������Ϣ := 'δ��ѯ��ҽ����¼';
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ӧ���󱨴�:' || SQLERRM;
      GOTO �˳�;
  END;

  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);
  RETURN;

END PR_������ͨ_ҽ���������ݲ�ѯ;
/

prompt
prompt Creating procedure PR_������ͨ_ҽԺ��Ϣ��ѯ
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_ҽԺ��Ϣ��ѯ(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2,
                                           STR_���ܱ��� IN VARCHAR2,
                                           LOB_��Ӧ���� OUT CLOB,
                                           INT_����ֵ   OUT INTEGER,
                                           STR_������Ϣ OUT VARCHAR2) IS

  STR_SQL          VARCHAR2(1000);
  STR_ҽԺID       VARCHAR2(50);
  INT_���ԤԼ���� INTEGER;

BEGIN
  BEGIN
  
    STR_ҽԺID := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
  
    --����ȡ���ԤԼ������
    BEGIN
      SELECT ֵ
        INTO INT_���ԤԼ����
        FROM ������Ŀ_���������б�
       WHERE �������룽 '910540'
         AND �������� = STR_ҽԺID
         and ɾ����־ = '0';
    EXCEPTION
      WHEN OTHERS THEN
        INT_���ԤԼ���� := 15;
    END;
  
    --��ҵ����ӦSQL��
    STR_SQL := 'select �������� as HOS_ID,
                      �������� as "NAME",
                      ''Ӫ�ڶ�Ժ'' as SHORT_NAME, 
                      ������ַ as ADDRESS,
                      ��ϵ�绰 as TEL,
                      '''' as WEBSITE,
                      '''' as WEIBO,
                      ''2'' as "LEVEL",
                      '''' as "DESC",
                      '''' as SPECIAL,
                      '''' as LONGITUDE,
                      '''' as LATITUDE,
                      ' || INT_���ԤԼ���� ||
               ' as MAX_REG_DAYS,
                      '''' as START_REG_TIME,
                      '''' as END_REG_TIME,
                      '''' as STOP_BOOK_TIMEA,
                      '''' as STOP_BOOK_TIMEP                      
               from ������Ŀ_�������� where ��������=' || STR_ҽԺID;
  
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL    => STR_SQL,
                               STR_����ǩ => 'RES',
                               STR_�б�ǩ => '');
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    ELSE
      INT_����ֵ   := 100401;
      STR_������Ϣ := 'ҽԺ�����ڣ�δ��ѯ��ҽԺ��Ϣ��¼';
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '��Ӧ���󱨴�:' || SQLERRM;
      GOTO �˳�;
  END;

  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  RETURN;
END PR_������ͨ_ҽԺ��Ϣ��ѯ;
/

prompt
prompt Creating procedure PR_������ͨ_�û�����֤
prompt ================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_�û�����֤(STR_������� IN VARCHAR2,
                                          LOB_��Ӧ���� OUT CLOB,
                                          RES_CODE     OUT INTEGER,
                                          RES_MSG      OUT VARCHAR2) IS

  STR_SQL            VARCHAR2(1000);
  STR_��������       VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
  STR_�û�ID         VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������,'HOSP_PATIENT_ID');
  STR_֤������       VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ID_TYPE');
  STR_֤������       VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ID_NO');
  STR_������         VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'CARD_TYPE');
  STR_����           VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'CARD_NO');
  STR_����           VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'NAME');
  STR_�Ա�           VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'SEX');
  STR_��������       VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'BIRTHDAY');
  STR_�໤��֤������ VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PARENT_ID_TYPE');
  STR_�໤��֤������ VARCHAR2(50) := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PARENT_ID_CARD');
BEGIN
  BEGIN
  
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
    
      RES_CODE := 0;
      RES_MSG  := '���׳ɹ�';
    ELSE
      RES_CODE := 200101;
      RES_MSG  := '���Ҳ����ڣ�δ��ѯ�����Ҽ�¼';
    END IF;
  
  END;

END PR_������ͨ_�û�����֤;
/

prompt
prompt Creating procedure PR_������ͨ_�û���Ϣ��ѯ
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_�û���Ϣ��ѯ(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2,
                                           STR_���ܱ��� IN VARCHAR2,
                                           LOB_��Ӧ���� OUT CLOB,
                                           INT_����ֵ   OUT INTEGER,
                                           STR_������Ϣ OUT VARCHAR2) IS

  --�����������
  STR_ҽԺID         VARCHAR2(50);
  STR_�û�ID         VARCHAR2(50);
  STR_�û�֤������   VARCHAR2(50);
  STR_�û�֤������   VARCHAR2(50);
  STR_�û�����       VARCHAR2(50);
  STR_�û��Ա�       VARCHAR2(50);
  STR_�û���������   VARCHAR2(50);
  STR_�໤��֤������ VARCHAR2(50);
  STR_�໤��֤������ VARCHAR2(50);

  --��ҵ�������
  STR_SQL          VARCHAR2(1000);
  STR_�û�ע��ʱ�� VARCHAR2(50);
  DAT_ϵͳʱ��     DATE;

BEGIN
  BEGIN
    --���������������
    STR_ҽԺID         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_�û�ID         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_PATIENT_ID');
    STR_�û�֤������   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ID_TYPE');
    STR_�û�֤������   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ID_NO');
    STR_�û�����       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'NAME');
    STR_�û��Ա�       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'SEX');
    STR_�û���������   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'BIRTHDAY');
    STR_�໤��֤������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PARENT_ID_TYPE');
    STR_�໤��֤������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PARENT_ID_CARD');
  
    --����ȡϵͳʱ�䡿
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    --����Ϣ��֤��
    --1.����
    IF STR_�û����� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '������������';
      GOTO �˳�;
    END IF;
    --2.�Ա�
    IF STR_�û��Ա� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�������Ա�';
      GOTO �˳�;
    END IF;
    --3.��������
    IF STR_�û��������� IS NULL OR FU_����ת����(STR_�û���������) IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '��������Ч�ĳ������ڣ�';
      GOTO �˳�;
    END IF;
  
    --���ж��û��Ƿ���� ����ȡ�û�ID��
  
    SELECT COUNT(1)
      INTO INT_����ֵ
      FROM ������ͨ_�û���Ϣ B
     WHERE B.ҽԺ���� = STR_ҽԺID
       AND B.ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND (B.����ID = STR_�û�ID OR STR_�û�ID IS NULL)
       AND B.���� = STR_�û�����
       AND B.�Ա� = STR_�û��Ա�
       AND B.�������� = TO_DATE(STR_�û���������, 'yyyy-MM-dd')
       AND (B.�໤��֤������ = STR_�໤��֤������ OR B.֤������ = STR_�û�֤������)
       AND ROWNUM = 1;
  
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := 100301;
      STR_������Ϣ := '�û�δ��ҽԺע��';
      GOTO �˳�;
    END IF;
  
    SELECT B.����ID, TO_CHAR(B.����ʱ��, 'yyyy-MM-dd hh24:mi:ss')
      INTO STR_�û�ID, STR_�û�ע��ʱ��
      FROM ������ͨ_�û���Ϣ B
     WHERE B.ҽԺ���� = STR_ҽԺID
       AND B.ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND (B.����ID = STR_�û�ID OR STR_�û�ID IS NULL)
       AND B.���� = STR_�û�����
       AND B.�Ա� = STR_�û��Ա�
       AND B.�������� = TO_DATE(STR_�û���������, 'yyyy-MM-dd')
       AND (B.�໤��֤������ = STR_�໤��֤������ OR B.֤������ = STR_�û�֤������)
       AND ROWNUM = 1;
  
    STR_SQL := 'select ����ID as HOSP_PATIENT_ID,
                  '''' as HOSP_PATIENT_ID,
                  ''99'' as CARD_TYPE,
                  ����ID as CARD_NO,
                  ''0'' as CARD_STATUS,
                  '''' as CARD_TIME,
                  '''' as LAST_TIME,
                  �ֻ����� as MOBILE,
                  ��ϵ��ַ as ADDRESS                 
   from ������ͨ_�û���Ϣ where ҽԺ����=' || STR_ҽԺID || ' and ƽ̨��ʶ=' ||
               STR_ƽ̨��ʶ || ' and ����ID =' || STR_�û�ID;
  
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL    => STR_SQL,
                               STR_����ǩ => 'RES',
                               STR_�б�ǩ => 'CARD_INFO');
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
      --�����CREATE_TIME�ڵ㡿
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����   => LOB_��Ӧ����,
                                STR_���λ��   => 'CARD_INFO',
                                STR_��ӽڵ��� => 'CREATE_TIME',
                                STR_��ӽڵ�ֵ => STR_�û�ע��ʱ��);
    
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    ELSE
      INT_����ֵ   := 100301;
      STR_������Ϣ := '�û�δ��ҽԺע��';
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '������Ӧ����' || SQLERRM;
      GOTO �˳�;
  END;

  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => DAT_ϵͳʱ��,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  RETURN;

END PR_������ͨ_�û���Ϣ��ѯ;
/

prompt
prompt Creating procedure PR_������ͨ_�û���Ϣע��
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_�û���Ϣע��(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2,
                                           STR_���ܱ��� IN VARCHAR2,
                                           LOB_��Ӧ���� OUT CLOB,
                                           INT_����ֵ   OUT INTEGER,
                                           STR_������Ϣ OUT VARCHAR2) IS

  --�����������
  STR_ҽԺID         VARCHAR2(50);
  STR_֤������       VARCHAR2(50);
  STR_֤������       VARCHAR2(50);
  STR_��֤����       VARCHAR2(50);
  STR_��Ч����       VARCHAR2(50);
  STR_������         VARCHAR2(50);
  STR_����           VARCHAR2(50);
  STR_����           VARCHAR2(50);
  STR_�Ա�           VARCHAR2(50);
  STR_��������       VARCHAR2(50);
  STR_�ֻ�����       VARCHAR2(50);
  STR_��ַ           VARCHAR2(50);
  STR_�໤��֤������ VARCHAR2(50);
  STR_�໤��֤������ VARCHAR2(50);
  STR_�໤������     VARCHAR2(50);

  --��ҵ�������
  STR_SQL      VARCHAR2(1000);
  STR_����ID   VARCHAR2(50);
  DAT_ϵͳʱ�� DATE;
BEGIN
  BEGIN
    --���������������
    STR_ҽԺID         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_֤������       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ID_TYPE');
    STR_֤������       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ID_NO');
    STR_��֤����       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ID_ISSUE_DATE');
    STR_��Ч����       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ID_EFFECT_DATE');
    STR_������         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'CARD_TYPE');
    STR_����           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'CARD_NO');
    STR_����           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'NAME');
    STR_�Ա�           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'SEX');
    STR_��������       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'BIRTHDAY');
    STR_�ֻ�����       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'MOBILE');
    STR_��ַ           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ADDRESS');
    STR_�໤��֤������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PARENT_ID_TYPE');
    STR_�໤��֤������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PARENT_ID_CARD');
    STR_�໤������     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PARENT_NAME');
  
    --����ȡDAT_ϵͳʱ�䡿
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    --����Ϣ��֤��
    --1.����
    IF STR_���� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '������������';
      GOTO �˳�;
    END IF;
    --2.�Ա�
    IF STR_�Ա� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�������Ա�';
      GOTO �˳�;
    END IF;
    --3.�ֻ�����
    IF STR_�ֻ����� IS NULL OR FU_������ͨ_��֤�ֻ���(STR_�ֻ�����) <> 0 THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '��������Ч���ֻ����룡';
      GOTO �˳�;
    END IF;
    --4.��������
    IF STR_�������� IS NULL OR FU_����ת����(STR_��������) IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '��������Ч�ĳ������ڣ�';
      GOTO �˳�;
    END IF;
    --5.֤����Ϣ
    IF (STR_֤������ IS NULL OR STR_֤������ IS NULL) AND
       (STR_�໤��֤������ IS NULL OR STR_�໤��֤������ IS NULL OR STR_�໤������ IS NULL) THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '����д�û�֤����Ϣ��໤��֤����Ϣ��';
      GOTO �˳�;
    END IF;
  
    IF STR_֤������ = '1' AND FU_������ͨ_��֤���֤(STR_֤������) <> 0 THEN
      STR_������Ϣ := '��Ч���û����֤����';
      INT_����ֵ   := 1;
      GOTO �˳�;
    END IF;
    IF STR_�໤��֤������ = '1' AND FU_������ͨ_��֤���֤(STR_�໤��֤������) <> 0 THEN
      STR_������Ϣ := '��Ч�ļ໤��֤������';
      INT_����ֵ   := 1;
      GOTO �˳�;
    END IF;
  
    --����֤�Ƿ���ڸ��û���
    SELECT COUNT(1)
      INTO INT_����ֵ
      FROM ������ͨ_�û���Ϣ B
     WHERE B.ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND B.ҽԺ���� = STR_ҽԺID
       AND B.���� = STR_����
       AND (B.�໤��֤������ = STR_�໤��֤������ OR B.֤������ = STR_֤������);
  
    IF INT_����ֵ <= 0 THEN
      -- �����ɲ���ID��
      PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '30',
                  PRM_��������   => STR_ҽԺID,
                  PRM_��������   => '1',
                  PRM_����Ψһ�� => STR_����ID,
                  PRM_ִ�н��   => INT_����ֵ,
                  PRM_������Ϣ   => STR_������Ϣ);
      IF INT_����ֵ <> 0 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '���ɲ���IDʧ��,ԭ��:' + STR_������Ϣ;
        GOTO �˳�;
      END IF;
    
      -- �����벡����Ϣ��
      INSERT INTO ������Ŀ_������Ϣ
        (��������,
         ����ID,
         �������,
         ����,
         ����,
         �Ա�,
         ��������,
         ����,
         ��ͥ��ַ,
         ������λ,
         �ֻ�����,
         �̶��绰,
         ��������,
         ����ID,
         �Ǽ�ʱ��,
         ƴ����,
         �����,
         ������������,
         ���֤��,
         ����״��,
         ¼���˱���,
         ���ո��˱���,
         һ��ͨΨһ��,
         ũ�ϸ��˱���,
         ҽ�����˱���,
         �ǾӸ��˱���,
         ���Ѹ��˱���,
         ���ݸ��˱���,
         ���������˱���,
         �ԷѼ������˱���,
         ��������)
      VALUES
        (STR_ҽԺID,
         STR_����ID,
         NULL,
         STR_����,
         STR_����,
         STR_�Ա�,
         TO_DATE(STR_��������, 'yyyy-MM-dd'),
         TO_CHAR(FU_�õ�_����_��ȷ��(TO_DATE(STR_��������, 'yyyy-MM-dd'))),
         STR_��ַ,
         NULL,
         STR_�ֻ�����,
         NULL,
         NULL,
         NULL,
         SYSDATE,
         FU_ͨ��_����_ת��_��ƴ(STR_����),
         FU_ͨ��_����_ת��_���(STR_����),
         NULL,
         STR_֤������,
         NULL,
         STR_ҽԺID,
         NULL,
         NULL,
         NULL,
         NULL,
         NULL,
         NULL,
         NULL,
         NULL,
         NULL,
         DECODE(STR_������, '1', STR_����, NULL));
    
      -- �����벡�˸�����Ϣ��
      INSERT INTO ������Ŀ_������Ϣ_����
        (��������,
         ����ID,
         ҽ������,
         �ҳ�����,
         ְҵ,
         ��ʾ��Ϣ,
         ����,
         ѧУ,
         �������,
         �໤������,
         �໤�����֤��,
         �໤���ֻ�����,
         �໤����ϵ��ַ,
         ������Դ)
      VALUES
        (STR_ҽԺID,
         STR_����ID,
         NULL,
         NULL,
         NULL,
         NULL,
         NULL,
         NULL,
         NULL,
         STR_�໤������,
         STR_�໤��֤������,
         NULL,
         NULL,
         '1');
    
    ELSE
      INT_����ֵ   := 100202;
      STR_������Ϣ := 'Ժ�ڶ���û�������������ϵҽԺ����';
    END IF;
  
    -- ��ע�Ს����Ϣ��
    INSERT INTO ������ͨ_�û���Ϣ
      (��ˮ��,
       ƽ̨��ʶ,
       ҽԺ����,
       ����ID,
       �û����,
       ����,
       �Ա�,
       ��������,
       ֤������,
       ֤������,
       ֤����֤����,
       ֤����Ч����,
       �ֻ�����,
       ��ϵ��ַ,
       �໤������,
       �໤��֤������,
       �໤��֤������,
       �û�������,
       �û�����,
       ������,
       ����ʱ��)
    VALUES
      (SEQ_������ͨ_�û���Ϣ_��ˮ��.NEXTVAL,
       STR_ƽ̨��ʶ,
       STR_ҽԺID,
       STR_����ID,
       NULL,
       STR_����,
       STR_�Ա�,
       TO_DATE(STR_��������, 'yyyy-MM-dd'),
       STR_֤������,
       STR_֤������,
       STR_��֤����,
       STR_��Ч����,
       STR_�ֻ�����,
       STR_��ַ,
       STR_�໤������,
       STR_�໤��֤������,
       STR_�໤��֤������,
       STR_������,
       STR_����,
       STR_ƽ̨��ʶ,
       DAT_ϵͳʱ��);
  
    -- �������˳���   
    STR_SQL := 'SELECT ''' || STR_����ID || ''' AS HOSP_PATIENT_ID, 
             ''' || STR_������ || ''' AS CARD_TYPE, 
             ''' || STR_���� || ''' AS CARD_NO,
             '''' as HOSP_MEDICAL_NUM
      FROM DUAL';
  
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL    => STR_SQL,
                               STR_����ǩ => 'RES',
                               STR_�б�ǩ => '');
    INT_����ֵ   := 0;
    STR_������Ϣ := '���׳ɹ�';
  
    COMMIT;
    RETURN;
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '��Ӧ���󱨴�:' || SQLERRM;
      GOTO �˳�;
  END;

  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => DAT_ϵͳʱ��,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  ROLLBACK;
  RETURN;

END PR_������ͨ_�û���Ϣע��;
/

prompt
prompt Creating procedure PR_������ͨ_Ԥ�Һ�
prompt ==============================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_Ԥ�Һ�(STR_������� IN VARCHAR2,
                                        STR_ƽ̨��ʶ IN VARCHAR2,
                                        STR_���ܱ��� IN VARCHAR2,
                                        LOB_��Ӧ���� OUT CLOB,
                                        INT_����ֵ   OUT INTEGER,
                                        STR_������Ϣ OUT VARCHAR2) IS
  --�����������
  STR_ƽ̨������       VARCHAR2(50);
  STR_����ID           VARCHAR2(50);
  STR_�Һ�����ID       VARCHAR2(50);
  STR_�Ƿ�ΪԤԼ�Һ�   VARCHAR2(50);
  STR_��α�ʶ         VARCHAR2(50);
  STR_�Ű����         VARCHAR2(50);
  STR_ҽԺID           VARCHAR2(50);
  STR_����ID           VARCHAR2(50);
  STR_ҽ��ID           VARCHAR2(50);
  STR_��������         VARCHAR2(50);
  STR_ʱ��             VARCHAR2(50);
  STR_��ʱ��ʼʱ��     VARCHAR2(50);
  STR_��ʱ����ʱ��     VARCHAR2(50);
  STR_�Һŷ���         VARCHAR2(50);
  STR_���Ʒ���         VARCHAR2(50);
  STR_�Һ�����         VARCHAR2(50);
  STR_����֤������     VARCHAR2(50);
  STR_����֤������     VARCHAR2(50);
  STR_���߿�����       VARCHAR2(50);
  STR_���߿���         VARCHAR2(50);
  STR_��������         VARCHAR2(50);
  STR_�����Ա�         VARCHAR2(50);
  STR_���߳�������     VARCHAR2(50);
  STR_�������ڵ�       VARCHAR2(50);
  STR_�����ֻ�����     VARCHAR2(50);
  STR_�Һ���֤������   VARCHAR2(50);
  STR_�Һ������֤���� VARCHAR2(50);
  STR_�Һ�������       VARCHAR2(50);
  STR_�Һ����ֻ�����   VARCHAR2(50);
  STR_��ϯ����         VARCHAR2(50);
  STR_�µ�ʱ��         VARCHAR2(50);

  --��ҵ�������
  STR_SQL      VARCHAR2(1000);
  STR_ԤԼ���� VARCHAR2(50);
  STR_����״�� VARCHAR2(50);
  STR_��ͥ��ַ VARCHAR2(50);
  STR_������λ VARCHAR2(50);

  NUM_�Һŷ�   NUMBER(18, 3);
  NUM_����   NUMBER(18, 3);
  STR_������� VARCHAR2(50);
  NUM_Ӧ����� NUMBER(18, 3);

  STR_�Ű��¼ID   VARCHAR2(50);
  DAT_�Ű�����     DATE;
  STR_�Һſ��ұ��� VARCHAR2(50);
  STR_�Һſ������� VARCHAR2(50);
  STR_�Һſ���λ�� VARCHAR2(50);
  STR_�Һ�ҽ������ VARCHAR2(50);
  STR_�Һ�ҽ������ VARCHAR2(50);
  STR_�Һ����ͱ��� VARCHAR2(50);
  STR_�Һ��������� VARCHAR2(50);
  STR_ԤԼʱ�α��� VARCHAR2(50);
  STR_ԤԼʱ�ο�ʼ VARCHAR2(50);
  STR_ԤԼʱ�ν��� VARCHAR2(50);

  DAT_��������ʱ�� DATE;
  DAT_ϵͳʱ��     DATE;
  STR_������       VARCHAR2(50);
  STR_������ע     VARCHAR2(50);

  STR_�û�����     VARCHAR2(50);
  STR_�û��Ա�     VARCHAR2(50);
  STR_�û��������� VARCHAR2(50);
  STR_�û����֤�� VARCHAR2(50);

BEGIN
  BEGIN
  
    --���������������
    STR_ƽ̨������       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ORDER_ID');
    STR_����ID           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_PATIENT_ID');
    STR_�Һ�����ID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'CHANNEL_ID');
    STR_�Ƿ�ΪԤԼ�Һ�   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'IS_REG');
    STR_��α�ʶ         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REG_ID');
    STR_�Ű����         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REG_LEVEL');
    STR_ҽԺID           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_����ID           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DEPT_ID');
    STR_ҽ��ID           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DOCTOR_ID');
    STR_��������         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REG_DATE');
    STR_ʱ��             := FU_������ͨ_�ڵ�ֵ(STR_�������, 'TIME_FLAG');
    STR_��ʱ��ʼʱ��     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'BEGIN_TIME');
    STR_��ʱ����ʱ��     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'END_TIME');
    STR_�Һŷ���         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REG_FEE');
    STR_���Ʒ���         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'TREAT_FEE');
    STR_�Һ�����         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REG_TYPE'); --1���� 2��Ů 3����
    STR_����֤������     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'IDCARD_TYPE');
    STR_����֤������     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'IDCARD_NO');
    STR_���߿�����       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'CARD_TYPE');
    STR_���߿���         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'CARD_NO');
    STR_��������         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'NAME');
    STR_�����Ա�         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'SEX');
    STR_���߳�������     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'BIRTHDAY');
    STR_�������ڵ�       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ADDRESS');
    STR_�����ֻ�����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'MOBILE');
    STR_�Һ���֤������   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'OPER_IDCARD_TYPE');
    STR_�Һ������֤���� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'OPER_IDCARD_NO');
    STR_�Һ�������       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'OPER_NAME');
    STR_�Һ����ֻ�����   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'OPER_MOBILE');
    STR_��ϯ����         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'AGENT_ID');
    STR_�µ�ʱ��         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ORDER_TIME');
  
    --����֤�����Ű���Ϣ��
    IF STR_����ID IS NOT NULL THEN
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM �������_�����Ű��¼
       WHERE �������� = STR_ҽԺID
         AND ���ұ��� = STR_����ID
         AND �Ű����� = TO_DATE(STR_��������, 'yyyy-MM-dd');
    
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 200706;
        STR_������Ϣ := '���Ҳ�����';
        GOTO �˳�;
      END IF;
    END IF;
  
    --����֤ҽ���Ű���Ϣ��
    IF STR_ҽ��ID IS NOT NULL THEN
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM �������_�����Ű��¼
       WHERE �������� = STR_ҽԺID
         AND ҽ������ = STR_ҽ��ID
         AND �Ű����� = TO_DATE(STR_��������, 'yyyy-MM-dd');
    
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 200707;
        STR_������Ϣ := 'ҽ��������';
        GOTO �˳�;
      END IF;
    END IF;
  
    --����֤ƽ̨�����š�
    SELECT COUNT(1)
      INTO INT_����ֵ
      FROM ������ͨ_����
     WHERE ҽԺ���� = STR_ҽԺID
       AND ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND ƽ̨������ = STR_ƽ̨������;
  
    IF INT_����ֵ > 0 THEN
      INT_����ֵ   := 200708;
      STR_������Ϣ := 'ƽ̨�������Ѵ���';
      GOTO �˳�;
    END IF;
  
    -- ����ȡDAT_ϵͳʱ�䡿
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    --����ȡ�Ű���Ϣ��
    BEGIN
      SELECT D.��¼ID,
             D.�Ű�����,
             D.���ұ���,
             D.��������,
             D.����λ��,
             D.ҽ������,
             D.ҽ������,
             D.�Һ����ͱ���,
             D.�Һ���������,
             S.ʱ�α���,
             S.��ʼʱ��,
             S.����ʱ��
        INTO STR_�Ű��¼ID,
             DAT_�Ű�����,
             STR_�Һſ��ұ���,
             STR_�Һſ�������,
             STR_�Һſ���λ��,
             STR_�Һ�ҽ������,
             STR_�Һ�ҽ������,
             STR_�Һ����ͱ���,
             STR_�Һ���������,
             STR_ԤԼʱ�α���,
             STR_ԤԼʱ�ο�ʼ,
             STR_ԤԼʱ�ν���
        FROM �������_���Ű�ʱ�α� S, �������_�����Ű��¼ D
       WHERE S.�������� = D.��������
         AND S.�Ű���� = D.�Ű����
         AND S.��¼ID = D.��¼ID
         AND D.�������� = STR_ҽԺID
         AND S.�հ�α�ʶ = STR_��α�ʶ
         AND TO_DATE(TO_CHAR(D.�Ű�����, 'yyyy-MM-dd'), 'yyyy-MM-dd') >=
             TO_DATE(TO_CHAR(SYSDATE, 'yyyy-MM-dd'), 'yyyy-MM-dd');
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 200705;
        STR_������Ϣ := '��Ч���Ű�';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
    --����ȡ�Ű���Ϣ������
  
    --����ȡ�Һ�������Ϣ��
    BEGIN
      SELECT ���ͱ���, ��������, �Һŷ�, ����, �������
        INTO STR_�Һ����ͱ���,
             STR_�Һ���������,
             NUM_�Һŷ�,
             NUM_����,
             STR_�������
        FROM ������Ŀ_�Һ�����
       WHERE �������� = STR_ҽԺID
         AND ���ͱ��� = STR_�Һ����ͱ���
         AND ��Ч״̬ = '��Ч'
         AND ɾ����־ = '0';
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'δ�ҵ���Ч�ĹҺ����ͣ�';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
    --����ȡ�Һ�������Ϣ������
  
    IF TO_NUMBER(STR_�Һŷ���) <> NUM_�Һŷ� OR TO_NUMBER(STR_���Ʒ���) <> NUM_���� THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�ҷ�������Ű�������ò���';
      GOTO �˳�;
    END IF;
  
    --��ע����ȡ������Ϣ��ʼ��
    BEGIN
    
      --����֤�Ƿ���ڸ��û���
      BEGIN
        SELECT B.����ID
          INTO STR_����ID
          FROM ������ͨ_�û���Ϣ B
         WHERE B.ƽ̨��ʶ = STR_ƽ̨��ʶ
           AND B.ҽԺ���� = STR_ҽԺID
           AND (B.����ID = STR_����ID OR STR_����ID IS NULL)
           AND B.���� = STR_��������
           AND (B.�໤��֤������ = STR_�Һ������֤���� OR B.֤������ = STR_����֤������);
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          INT_����ֵ := 0;
        WHEN OTHERS THEN
          INT_����ֵ   := 1;
          STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
          GOTO �˳�;
      END;
    
      --2. ����ID�����ڻ�ΪNULLʱ ��ע��
      IF INT_����ֵ = 0 OR STR_����ID IS NULL THEN
        PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '30',
                    PRM_��������   => STR_ҽԺID,
                    PRM_��������   => '1',
                    PRM_����Ψһ�� => STR_����ID,
                    PRM_ִ�н��   => INT_����ֵ,
                    PRM_������Ϣ   => STR_������Ϣ);
      
        IF INT_����ֵ <> 0 THEN
          INT_����ֵ   := 1;
          STR_������Ϣ := '��ȡ����IDʧ��';
          GOTO �˳�;
        END IF;
      
        --����Ϣ��֤��
        --1.����
        IF STR_�������� IS NULL THEN
          INT_����ֵ   := 1;
          STR_������Ϣ := '������������';
          GOTO �˳�;
        END IF;
        --2.�Ա�
        IF STR_�����Ա� IS NULL THEN
          INT_����ֵ   := 1;
          STR_������Ϣ := '�������Ա�';
          GOTO �˳�;
        END IF;
        --3.�ֻ�����
        IF STR_�����ֻ����� IS NULL OR FU_������ͨ_��֤�ֻ���(STR_�����ֻ�����) <> 0 THEN
          INT_����ֵ   := 1;
          STR_������Ϣ := '��������Ч���ֻ����룡';
          GOTO �˳�;
        END IF;
        --4.��������
        IF STR_���߳������� IS NULL OR FU_����ת����(STR_���߳�������) IS NULL THEN
          INT_����ֵ   := 1;
          STR_������Ϣ := '��������Ч�ĳ������ڣ�';
          GOTO �˳�;
        END IF;
        --5.֤����Ϣ
        IF STR_�Һ����� = '2' THEN
          --��Ů
          IF STR_�Һ������֤���� IS NULL OR FU_������ͨ_��֤���֤(STR_�Һ������֤����) <> 0 THEN
            INT_����ֵ   := 1;
            STR_������Ϣ := '����ȷ��д�Һ���֤����Ϣ��';
            GOTO �˳�;
          END IF;
        ELSE
          --���˻�����
          IF STR_����֤������ IS NULL OR FU_������ͨ_��֤���֤(STR_����֤������) <> 0 THEN
            INT_����ֵ   := 1;
            STR_������Ϣ := '����ȷ��д����֤����Ϣ��';
            GOTO �˳�;
          END IF;
        END IF;
      
        -- 2.1. ���벡�˻�����Ϣ
        INSERT INTO ������Ŀ_������Ϣ
          (��������,
           ����ID,
           ����,
           ����,
           �Ա�,
           ��������,
           ����,
           ��ͥ��ַ,
           �ֻ�����,
           ƴ����,
           �����,
           �Ǽ�ʱ��,
           ¼���˱���,
           ���֤��)
        VALUES
          (STR_ҽԺID,
           STR_����ID,
           STR_���߿���,
           STR_��������,
           STR_�����Ա�,
           TO_DATE(STR_���߳�������, 'yyyy-MM-dd'),
           FU_�õ�_����(TO_DATE(STR_���߳�������, 'yyyy-MM-dd')),
           STR_�������ڵ�,
           STR_�����ֻ�����,
           FU_ͨ��_����_ת��_��ƴ(STR_��������),
           FU_ͨ��_����_ת��_���(STR_��������),
           SYSDATE,
           STR_ҽԺID,
           STR_����֤������);
      
        INT_����ֵ := SQL%ROWCOUNT;
        IF INT_����ֵ = 0 THEN
          INT_����ֵ   := 200703;
          STR_������Ϣ := '�û�����ʧ��';
          GOTO �˳�;
        END IF;
      
        --2.2. ���벡�˸�����Ϣ
        INSERT INTO ������Ŀ_������Ϣ_����
          (��������,
           ����ID,
           �໤������,
           �໤�����֤��,
           �໤���ֻ�����,
           �໤����ϵ��ַ,
           ������Դ)
        VALUES
          (STR_ҽԺID,
           STR_����ID,
           DECODE(STR_�Һ�����, '2', STR_�Һ�������, NULL),
           DECODE(STR_�Һ�����, '2', STR_�Һ������֤����, NULL),
           DECODE(STR_�Һ�����, '2', STR_�Һ����ֻ�����, NULL),
           NULL,
           '1');
        INT_����ֵ := SQL%ROWCOUNT;
        IF INT_����ֵ = 0 THEN
          INT_����ֵ   := 200703;
          STR_������Ϣ := '�û�����ʧ��';
          GOTO �˳�;
        END IF;
      
        -- ��ע�Ს����Ϣ��
        INSERT INTO ������ͨ_�û���Ϣ
          (��ˮ��,
           ƽ̨��ʶ,
           ҽԺ����,
           ����ID,
           �û����,
           ����,
           �Ա�,
           ��������,
           ֤������,
           ֤������,
           ֤����֤����,
           ֤����Ч����,
           �ֻ�����,
           ��ϵ��ַ,
           �໤������,
           �໤��֤������,
           �໤��֤������,
           �û�������,
           �û�����,
           ������,
           ����ʱ��)
        VALUES
          (SEQ_������ͨ_�û���Ϣ_��ˮ��.NEXTVAL,
           STR_ƽ̨��ʶ,
           STR_ҽԺID,
           STR_����ID,
           STR_�Һ�����,
           STR_��������,
           STR_�����Ա�,
           TO_DATE(STR_���߳�������, 'yyyy-MM-dd'),
           STR_����֤������,
           STR_����֤������,
           NULL,
           NULL,
           STR_�����ֻ�����,
           STR_�������ڵ�,
           STR_�Һ�������,
           STR_�Һ���֤������,
           STR_�Һ������֤����,
           STR_���߿�����,
           STR_���߿���,
           STR_ƽ̨��ʶ,
           DAT_ϵͳʱ��);
      
        INT_����ֵ := SQL%ROWCOUNT;
        IF INT_����ֵ = 0 THEN
          INT_����ֵ   := 200703;
          STR_������Ϣ := '�û�����ʧ��';
          GOTO �˳�;
        END IF;
      
      ELSE
      
        --���䲡����Ϣ
        SELECT A.����, A.�Ա�, A.��������, A.֤������
          INTO STR_�û�����,
               STR_�û��Ա�,
               STR_�û���������,
               STR_�û����֤��
          FROM ������ͨ_�û���Ϣ A
         WHERE A.ƽ̨��ʶ = STR_ƽ̨��ʶ
           AND A.ҽԺ���� = STR_ҽԺID
           AND A.����ID = STR_����ID;
      
        IF STR_�û����� <> STR_�������� OR STR_�û��Ա� <> STR_�����Ա� OR
           STR_�û��������� <> STR_���߳������� OR
           (STR_����֤������ IS NOT NULL AND STR_�û����֤�� <> STR_����֤������) THEN
          INT_����ֵ   := 200711;
          STR_������Ϣ := '�����������Ϣ��ҽԺ������ƥ��';
          GOTO �˳�;
        END IF;
      END IF;
    EXCEPTION
      WHEN OTHERS THEN
        INT_����ֵ   := '200703';
        STR_������Ϣ := '�û�����ʧ��';
        GOTO �˳�;
    END;
    --��ע����ȡ������Ϣ������
  
    --�����ɶ����š�
    PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '6001',
                PRM_��������   => STR_ҽԺID,
                PRM_��������   => '1',
                PRM_����Ψһ�� => STR_������,
                PRM_ִ�н��   => INT_����ֵ,
                PRM_������Ϣ   => STR_������Ϣ);
    IF INT_����ֵ <> 0 THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '����������ʧ��!';
      GOTO �˳�;
    END IF;
    -- �����ɽ�
    NUM_Ӧ����� := STR_�Һŷ��� + STR_���Ʒ���;
  
    -- �����ɹ���ʱ�䡿
    SELECT SYSDATE + (1 / (24 * 60)) * 30 INTO DAT_��������ʱ�� FROM DUAL;
  
    -- �����ɶ�����ע��
    STR_������ע := '����30���������֧���������Զ�ȡ��';
  
    --������ԤԼ���š�
    SELECT SEQ_�������_ԤԼ�Һ�_ΨһID.NEXTVAL
      INTO STR_ԤԼ����
      FROM DUAL;
  
    BEGIN
    
      -- ����ԤԼ��¼
      INSERT INTO �������_ԤԼ�Һ�
        (��������,
         ����ID,
         ����,
         �Ա�,
         ��������,
         ����״��,
         ��ϵ�绰,
         ��ͥ��ַ,
         ������λ,
         ���֤��,
         ƴ����,
         �����,
         �Һſ��ұ���,
         �Һſ�������,
         �Һſ���λ��,
         �Һ�ҽ������,
         �Һ�ҽ������,
         �Һ����ͱ���,
         �Һ���������,
         �����,
         ԤԼʱ��,
         ȥ���־,
         �Һ����,
         ����ID,
         ԤԼ����,
         ��¼�˱���,
         ��¼ʱ��,
         �Ű�ID,
         ֧����־,
         �Һŷ�,
         ����,
         �������,
         ��ʱʱ��,
         ԤԼʱ�α���,
         ԤԼʱ�ο�ʼ,
         ԤԼʱ�ν���,
         �հ�α�ʶ)
      VALUES
        (STR_ҽԺID,
         STR_ԤԼ����,
         STR_��������,
         STR_�����Ա�,
         TO_DATE(STR_���߳�������, 'yyyy-MM-dd'),
         STR_����״��,
         STR_�����ֻ�����,
         STR_��ͥ��ַ,
         STR_������λ,
         STR_����֤������,
         FU_ͨ��_����_ת��_��ƴ(STR_��������),
         FU_ͨ��_����_ת��_���(STR_��������),
         STR_�Һſ��ұ���,
         STR_�Һſ�������,
         STR_�Һſ���λ��,
         STR_�Һ�ҽ������,
         STR_�Һ�ҽ������,
         STR_�Һ����ͱ���,
         STR_�Һ���������,
         NULL,
         DAT_�Ű�����,
         'ռ��',
         NULL,
         STR_����ID,
         '����ԤԼ',
         STR_ҽԺID,
         DAT_ϵͳʱ��,
         STR_�Ű��¼ID,
         '��',
         NUM_�Һŷ�,
         NUM_����,
         STR_�������,
         DAT_��������ʱ��,
         STR_ԤԼʱ�α���,
         TO_DATE(TO_CHAR(DAT_�Ű�����, 'yyyy-mm-dd') || ' ' || STR_ԤԼʱ�ο�ʼ,
                 'yyyy-mm-dd hh24:mi:ss'),
         TO_DATE(TO_CHAR(DAT_�Ű�����, 'yyyy-mm-dd') || ' ' || STR_ԤԼʱ�ν���,
                 'yyyy-mm-dd hh24:mi:ss'),
         STR_��α�ʶ);
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '����ԤԼ��¼ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      -- ���붩��
      INSERT INTO ������ͨ_����
        (��ˮ��,
         ƽ̨��ʶ,
         ҽԺ����,
         ����ID,
         ��������,
         ƽ̨������,
         ��������,
         ƽ̨����ʱ��,
         ҽԺ������,
         �Һŷ���,
         ���Ʒ���,
         ����ʱ��,
         ����״̬,
         �Һ�����,
         �Һ�����,
         ������,
         ����ʱ��,
         ������,
         ����ʱ��)
      VALUES
        (SEQ_������ͨ_����_��ˮ��.NEXTVAL,
         STR_ƽ̨��ʶ,
         STR_ҽԺID,
         STR_����ID,
         STR_ԤԼ����,
         STR_ƽ̨������,
         'ԤԼ�Һ�',
         TO_DATE(STR_�µ�ʱ��, 'yyyy-MM-dd hh24:mi:ss'),
         STR_������,
         TO_NUMBER(STR_�Һŷ���),
         TO_NUMBER(STR_���Ʒ���),
         DAT_��������ʱ��,
         '��֧��',
         STR_�Һ�����ID,
         STR_�Һ�����,
         STR_ƽ̨��ʶ,
         DAT_ϵͳʱ��,
         STR_ƽ̨��ʶ,
         DAT_ϵͳʱ��);
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '���涩��ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      -- ���붩����ϸ
      -- �Һŷ�
      INSERT INTO ������ͨ_������ϸ
        (��ˮ��,
         ������,
         ��Ŀ����,
         ��Ŀ����,
         ����,
         ��λ,
         ����,
         �ܽ��,
         �������)
      VALUES
        (SEQ_������ͨ_������ϸ_��ˮ��.NEXTVAL,
         STR_������,
         '�Һŷ�',
         '�Һŷ�',
         1,
         '��',
         NUM_�Һŷ�,
         NUM_�Һŷ�,
         STR_�������);
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '���涩����ϸʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      -- ����
      INSERT INTO ƽ̨�ӿ�_������ϸ
        (��ˮ��,
         ������,
         ��Ŀ����,
         ��Ŀ����,
         ����,
         ��λ,
         ����,
         �ܽ��,
         �������)
      VALUES
        (SEQ_������ͨ_������ϸ_��ˮ��.NEXTVAL,
         STR_������,
         '����',
         '����',
         1,
         '��',
         NUM_����,
         NUM_����,
         STR_�������);
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '���涩����ϸʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
     
    
    EXCEPTION
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
      
    END;
  
    STR_SQL      := 'select ''' || STR_������ || ''' as HOSP_ORDER_ID,''' ||
                    STR_����ID || ''' as HOSP_PATIENT_ID,
               '''' as HOSP_SERIAL_NUM,
               '''' as HOSP_MEDICAL_NUM,
               '''' as HOSP_GETREG_DATE,
               '''' as HOSP_SEE_DOCT_ADDR,
               '''' as HOSP_CARD_NO,
               ''' || STR_������ע ||
                    ''' as HOSP_REMARK,
               ''0'' as IS_CONCESSIONS
               from dual';
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', '');
  
    INT_����ֵ   := 0;
    STR_������Ϣ := '���׳ɹ�';
  
    COMMIT;
    RETURN;
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
    
  END;

  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_ҽԺID,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => DAT_ϵͳʱ��,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  ROLLBACK;
  RETURN;

END PR_������ͨ_Ԥ�Һ�;
/

prompt
prompt Creating procedure PR_������ͨ_���ߵ���
prompt ===============================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_���ߵ���(STR_���ܺ�   IN VARCHAR2,
                                         STR_������� IN VARCHAR2,
                                         LOB_��Ӧ���� OUT CLOB,
                                         RES_CODE     OUT INTEGER,
                                         RES_MSG      OUT VARCHAR2) IS
  STR_ƽ̨��ʶ VARCHAR2(10) := '12320';
BEGIN
  BEGIN
    IF STR_���ܺ� = '1001' THEN
      LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����('SELECT SYSDATE FROM DUAL', 'RES', '');
      RES_CODE     := '0';
      RES_MSG      := '���׳ɹ�';
    ELSIF STR_���ܺ� = '1002' THEN
      PR_������ͨ_�û���Ϣע��(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '1003' THEN
      PR_������ͨ_�û���Ϣ��ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '1004' THEN
      PR_������ͨ_ҽԺ��Ϣ��ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '1005' THEN
      PR_������ͨ_�û�����֤(STR_�������, LOB_��Ӧ����, RES_CODE, RES_MSG);
    ELSIF STR_���ܺ� = '2001' THEN
      PR_������ͨ_���Ҳ�ѯ(STR_������� => STR_�������,
                   STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                   STR_���ܱ��� => STR_���ܺ�,
                   LOB_��Ӧ���� => LOB_��Ӧ����,
                   INT_����ֵ   => RES_CODE,
                   STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2002' THEN
      PR_������ͨ_ҽ����ѯ(STR_������� => STR_�������,
                   STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                   STR_���ܱ��� => STR_���ܺ�,
                   LOB_��Ӧ���� => LOB_��Ӧ����,
                   INT_����ֵ   => RES_CODE,
                   STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2003' THEN
      PR_������ͨ_�Ű���Ϣ��ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2004' THEN
      PR_������ͨ_�Ű��ʱ��ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2005' THEN
      PR_������ͨ_��Դ����(STR_�������, LOB_��Ӧ����, RES_CODE, RES_MSG);
    ELSIF STR_���ܺ� = '2006' THEN
      PR_������ͨ_������Դ����(STR_�������, LOB_��Ӧ����, RES_CODE, RES_MSG);
    ELSIF STR_���ܺ� = '2007' THEN
      --�Һŷ� ���Ʒ�
      PR_������ͨ_Ԥ�Һ�(STR_������� => STR_�������,
                  STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                  STR_���ܱ��� => STR_���ܺ�,
                  LOB_��Ӧ���� => LOB_��Ӧ����,
                  INT_����ֵ   => RES_CODE,
                  STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2008' THEN
      --ҽԺ֧������
      PR_������ͨ_�Һ�֧��(STR_������� => STR_�������,
                   STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                   STR_���ܱ��� => STR_���ܺ�,
                   LOB_��Ӧ���� => LOB_��Ӧ����,
                   INT_����ֵ   => RES_CODE,
                   STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2009' THEN
      PR_������ͨ_ȡ���Һ�(STR_������� => STR_�������,
                   STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                   STR_���ܱ��� => STR_���ܺ�,
                   LOB_��Ӧ���� => LOB_��Ӧ����,
                   INT_����ֵ   => RES_CODE,
                   STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2010' THEN
      --Ժ���� ƽ̨��
      PR_������ͨ_�˿�Һ�(STR_������� => STR_�������,
                   STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                   STR_���ܱ��� => STR_���ܺ�,
                   LOB_��Ӧ���� => LOB_��Ӧ����,
                   INT_����ֵ   => RES_CODE,
                   STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2011' THEN
      PR_������ͨ_ȡ��(STR_������� => STR_�������,
                   STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                   STR_���ܱ��� => STR_���ܺ�,
                   LOB_��Ӧ���� => LOB_��Ӧ����,
                   INT_����ֵ   => RES_CODE,
                   STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2012' THEN
      PR_������ͨ_�Һż�¼��ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2020' THEN
      PR_������ͨ_ҽ���������ݲ�ѯ(STR_������� => STR_�������,
                       STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                       STR_���ܱ��� => STR_���ܺ�,
                       LOB_��Ӧ���� => LOB_��Ӧ����,
                       INT_����ֵ   => RES_CODE,
                       STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '3001' THEN
      PR_������ͨ_�ɷѼ�¼��ѯ(STR_������� => STR_�������,
                       STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                       STR_���ܱ��� => STR_���ܺ�,
                       LOB_��Ӧ���� => LOB_��Ӧ����,
                       INT_����ֵ   => RES_CODE,
                       STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '3002' THEN
      PR_������ͨ_�ɷ���ϸ��ѯ(STR_������� => STR_�������,
                       STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                       STR_���ܱ��� => STR_���ܺ�,
                       LOB_��Ӧ���� => LOB_��Ӧ����,
                       INT_����ֵ   => RES_CODE,
                       STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '3003' THEN
      PR_������ͨ_���ɷѼ�¼֧��(STR_�������, LOB_��Ӧ����, RES_CODE, RES_MSG);
    ELSIF STR_���ܺ� = '3004' THEN
      PR_������ͨ_�ɷѶ�����ѯ(STR_�������, LOB_��Ӧ����, RES_CODE, RES_MSG);
    ELSIF STR_���ܺ� = '4001' THEN
      PR_������ͨ_�Ŷ��б��ѯ(STR_�������, LOB_��Ӧ����, RES_CODE, RES_MSG);
    ELSIF STR_���ܺ� = '8001' THEN
      PR_������ͨ_�������б��ѯ(STR_������� => STR_�������,
                       STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                       STR_���ܱ��� => STR_���ܺ�,
                       LOB_��Ӧ���� => LOB_��Ӧ����,
                       INT_����ֵ   => RES_CODE,
                       STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '8002' THEN
      PR_������ͨ_��ͨ���鱨���ѯ(STR_������� => STR_�������,
                       STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                       STR_���ܱ��� => STR_���ܺ�,
                       LOB_��Ӧ���� => LOB_��Ӧ����,
                       INT_����ֵ   => RES_CODE,
                       STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '8003' THEN
      PR_������ͨ_��ͨ���鱨���ѯ(STR_������� => STR_�������,
                       STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                       STR_���ܱ��� => STR_���ܺ�,
                       LOB_��Ӧ���� => LOB_��Ӧ����,
                       INT_����ֵ   => RES_CODE,
                       STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '8004' THEN
      PR_������ͨ_��鱨���ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '9003' THEN
      PR_������ͨ_ϵͳ������ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSE
      RES_CODE := '-1';
      RES_MSG  := '���ܺŴ���';
    END IF;
  
  END;

END PR_������ͨ_���ߵ���;
/


prompt Done
spool off
set define on
