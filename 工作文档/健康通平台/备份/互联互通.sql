prompt PL/SQL Developer Export User Objects for user CLOUDHIS@47.104.4.221:9900/YKEY
prompt Created by syyyhl on 2020-07-02
set define off
spool ������ͨ.log

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
  ��ˮ��      NUMBER not null,
  ƽ̨��ʶ     VARCHAR2(50),
  ҽԺ����     VARCHAR2(50),
  ����id     VARCHAR2(50),
  ���ﲡ����    VARCHAR2(50),
  ��������     VARCHAR2(50),
  ����״̬     VARCHAR2(50),
  ҽԺ������    VARCHAR2(50),
  ƽ̨������    VARCHAR2(50),
  ����ʱ��     DATE,
  �Һŷ���     NUMBER(18,3),
  ���Ʒ���     NUMBER(18,3),
  �Һ�����     VARCHAR2(50),
  �Һ�����     VARCHAR2(50),
  ԤԼ�Һ�����   VARCHAR2(50),
  ҽԺ֧����    VARCHAR2(50),
  ƽ̨֧����    VARCHAR2(50),
  ֧��ʱ��     DATE,
  ƽ̨������ˮ��  VARCHAR2(50),
  ֧������     VARCHAR2(50),
  �ܽ��      NUMBER(18,3),
  Ӧ�����     NUMBER(18,3),
  ʵ�����     NUMBER(18,3),
  ҽ��ͳ��֧����� NUMBER(18,3),
  ҽԺ�˿��    VARCHAR2(50),
  ƽ̨�˿��    VARCHAR2(50),
  �˿�ʱ��     DATE,
  ƽ̨�˿���ˮ��  VARCHAR2(50),
  �˿���     NUMBER(18,3),
  �˿�ԭ��     VARCHAR2(100),
  �˿��־     VARCHAR2(50),
  ��������     VARCHAR2(50),
  ����ʱ��     DATE,
  ȡ��ʱ��     DATE,
  ȡ��ԭ��     VARCHAR2(100),
  ҽԺ�����    VARCHAR2(50),
  ������      VARCHAR2(50),
  ����ʱ��     DATE,
  ������      VARCHAR2(50),
  ����ʱ��     DATE,
  �����      INTEGER,
  ��ע       VARCHAR2(1000),
  ״̬       VARCHAR2(50)
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
  is 'ԤԼ�Һű������ֵ����������ϸ���շ����';
comment on column CLOUDHIS.������ͨ_����.����״̬
  is '�����ţ���֧������֧�������˿��ȡ������ɾ����';
comment on column CLOUDHIS.������ͨ_����.ҽԺ������
  is 'ҽԺ������';
comment on column CLOUDHIS.������ͨ_����.ƽ̨������
  is 'ƽ̨������';
comment on column CLOUDHIS.������ͨ_����.����ʱ��
  is '����ʱ��';
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
  is '��Ʊ���';
comment on column CLOUDHIS.������ͨ_����.ƽ̨֧����
  is 'ƽ̨֧����';
comment on column CLOUDHIS.������ͨ_����.֧��ʱ��
  is '֧��ʱ��';
comment on column CLOUDHIS.������ͨ_����.ƽ̨������ˮ��
  is 'ƽ̨������ˮ��';
comment on column CLOUDHIS.������ͨ_����.֧������
  is '1-5ƽ̨��6����';
comment on column CLOUDHIS.������ͨ_����.�ܽ��
  is '�ܽ��';
comment on column CLOUDHIS.������ͨ_����.Ӧ�����
  is 'Ӧ�����';
comment on column CLOUDHIS.������ͨ_����.ʵ�����
  is 'ʵ�����';
comment on column CLOUDHIS.������ͨ_����.ҽ��ͳ��֧�����
  is 'ҽ��ͳ��֧�����';
comment on column CLOUDHIS.������ͨ_����.ҽԺ�˿��
  is 'ҽԺ�˿��';
comment on column CLOUDHIS.������ͨ_����.ƽ̨�˿��
  is 'ƽ̨�˿��';
comment on column CLOUDHIS.������ͨ_����.�˿�ʱ��
  is '�˿�ʱ��';
comment on column CLOUDHIS.������ͨ_����.ƽ̨�˿���ˮ��
  is 'ƽ̨�˿���ˮ��';
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
comment on column CLOUDHIS.������ͨ_����.ҽԺ�����
  is 'ҽԺ�����';
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
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

prompt
prompt Creating table ������ͨ_������ϸ
prompt ========================
prompt
create table CLOUDHIS.������ͨ_������ϸ
(
  ��ˮ��  NUMBER not null,
  ������  VARCHAR2(50),
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
  Ψһ���� VARCHAR2(50)
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
comment on table CLOUDHIS.������ͨ_������ϸ
  is '������ͨ_������ϸ';
comment on column CLOUDHIS.������ͨ_������ϸ.��ˮ��
  is '��ˮ��';
comment on column CLOUDHIS.������ͨ_������ϸ.������
  is '������';
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
prompt Creating table ������ͨ_ƽ̨����
prompt ========================
prompt
create table CLOUDHIS.������ͨ_ƽ̨����
(
  ��ˮ��    VARCHAR2(50) not null,
  ƽ̨��ʶ   VARCHAR2(50),
  ƽ̨����   VARCHAR2(50),
  �û�id   VARCHAR2(50),
  ��֤��Կ   VARCHAR2(50),
  ҽԺid   VARCHAR2(50),
  ��������   VARCHAR2(50),
  url��ַ  VARCHAR2(50),
  ����     VARCHAR2(50),
  ������    VARCHAR2(50),
  ֧����ʽ   VARCHAR2(50),
  �������   NUMBER(18,3),
  ����ԤԼ���� VARCHAR2(1000),
  ��Ч״̬   VARCHAR2(50),
  ������    VARCHAR2(50),
  ����ʱ��   DATE,
  ������    VARCHAR2(50),
  ����ʱ��   DATE
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
comment on table CLOUDHIS.������ͨ_ƽ̨����
  is '������ͨ_ƽ̨����';
comment on column CLOUDHIS.������ͨ_ƽ̨����.��ˮ��
  is '��ˮ��';
comment on column CLOUDHIS.������ͨ_ƽ̨����.ƽ̨��ʶ
  is 'ƽ̨��ʶ';
comment on column CLOUDHIS.������ͨ_ƽ̨����.ƽ̨����
  is 'ƽ̨����';
comment on column CLOUDHIS.������ͨ_ƽ̨����.�û�id
  is '�û�ID';
comment on column CLOUDHIS.������ͨ_ƽ̨����.��֤��Կ
  is '��֤��Կ';
comment on column CLOUDHIS.������ͨ_ƽ̨����.ҽԺid
  is 'ƽ̨��ҽԺ��Ψһ��ʶ';
comment on column CLOUDHIS.������ͨ_ƽ̨����.��������
  is 'his��ҽԺ��Ψһ��ʶ';
comment on column CLOUDHIS.������ͨ_ƽ̨����.url��ַ
  is 'url��ַ';
comment on column CLOUDHIS.������ͨ_ƽ̨����.����
  is '����';
comment on column CLOUDHIS.������ͨ_ƽ̨����.������
  is '������';
comment on column CLOUDHIS.������ͨ_ƽ̨����.֧����ʽ
  is '֧����ʽ';
comment on column CLOUDHIS.������ͨ_ƽ̨����.�������
  is 'his��ƽ̨����Ļ������';
comment on column CLOUDHIS.������ͨ_ƽ̨����.����ԤԼ����
  is '��������ƽ̨ԤԼ�Ŀ���';
comment on column CLOUDHIS.������ͨ_ƽ̨����.��Ч״̬
  is '1��Ч��0��Ч';
comment on column CLOUDHIS.������ͨ_ƽ̨����.������
  is '������';
comment on column CLOUDHIS.������ͨ_ƽ̨����.����ʱ��
  is '����ʱ��';
comment on column CLOUDHIS.������ͨ_ƽ̨����.������
  is '������';
comment on column CLOUDHIS.������ͨ_ƽ̨����.����ʱ��
  is '����ʱ��';
alter table CLOUDHIS.������ͨ_ƽ̨����
  add constraint PK_������ͨ_ƽ̨���� primary key (��ˮ��)
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
comment on table CLOUDHIS.������ͨ_�û���Ϣ
  is '������ͨ_�û���Ϣ';
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
start with 61
increment by 1
cache 10;

prompt
prompt Creating sequence SEQ_������ͨ_������ϸ_��ˮ��
prompt ===================================
prompt
create sequence CLOUDHIS.SEQ_������ͨ_������ϸ_��ˮ��
minvalue 1
maxvalue 9999999999
start with 131
increment by 1
cache 10;

prompt
prompt Creating sequence SEQ_������ͨ_�û���Ϣ_��ˮ��
prompt ===================================
prompt
create sequence CLOUDHIS.SEQ_������ͨ_�û���Ϣ_��ˮ��
minvalue 1
maxvalue 9999999999
start with 41
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
prompt Creating function FU_������ͨ_�⹹���֤
prompt ===============================
prompt
CREATE OR REPLACE FUNCTION CLOUDHIS.FU_������ͨ_�⹹���֤(STR_���֤�� IN VARCHAR2,

                                         DAT_�������� OUT DATE,
                                         STR_����     OUT VARCHAR2,
                                         STR_�Ա�     OUT VARCHAR2,
                                         STR_������Ϣ OUT VARCHAR2)
  RETURN INTEGER IS
BEGIN
  IF FU_������ͨ_��֤���֤(STR_���֤��) <> 0 THEN
    STR_������Ϣ := '��Ч�����֤����';
    RETURN(-1);
  ELSE
    SELECT DECODE(MOD(TO_NUMBER(SUBSTR(STR_���֤��, 17, 1)), 2), 0, '2', '1'),
           TO_DATE(SUBSTR(STR_���֤��, 7, 8), 'yyyy-mm-dd')
      INTO STR_�Ա�,
           DAT_��������
      FROM DUAL;
    STR_����     := FU_�õ�_����(DAT_��������);
    STR_������Ϣ := '�ɹ���';
    RETURN(0);
  END IF;
EXCEPTION
  WHEN OTHERS THEN
    STR_������Ϣ := '�⹹���֤��ʧ�ܣ��������֤�Ƿ���ȷ';
    STR_�Ա�     := NULL;
    DAT_�������� := NULL;
    STR_����     := NULL;
    RETURN(-1);
END FU_������ͨ_�⹹���֤;
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
prompt Creating function FU_������ͨ_��֤�Ա�
prompt ==============================
prompt
CREATE OR REPLACE FUNCTION CLOUDHIS.FU_������ͨ_��֤�Ա�(STR_�Ա� IN VARCHAR2) RETURN VARCHAR2 IS
BEGIN
  IF STR_�Ա� IS NULL THEN
    RETURN('-1');
  END IF;
  --Ů
  IF STR_�Ա� = '0' THEN
    RETURN '2';
    --��
  ELSIF STR_�Ա� = '1' THEN
    RETURN '1';
    --δ֪
  ELSIF STR_�Ա� = '2' OR STR_�Ա� = '1' THEN
    RETURN '0';
  ELSE
    RETURN('-1');
  END IF;
END FU_������ͨ_��֤�Ա�;
/

prompt
prompt Creating function FU_������ͨ_ҽԺIDת��
prompt ================================
prompt
CREATE OR REPLACE FUNCTION CLOUDHIS.FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ VARCHAR2,
                                          STR_ת��ֵ   VARCHAR2,
                                          STR_ת������ VARCHAR2) --1��ҽԺIDתΪ��������    2�ɻ�������תΪҽԺID
 RETURN VARCHAR2 IS
  STR_��ȡֵ VARCHAR2(100);
BEGIN

  BEGIN
    SELECT decode(STR_ת������, '1', ��������, ҽԺID)
      INTO STR_��ȡֵ
      FROM ������ͨ_ƽ̨����
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND decode(STR_ת������, '1', ҽԺID, ��������) = STR_ת��ֵ
       AND ��Ч״̬ = '1'
       AND ROWNUM = 1;
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      STR_��ȡֵ := '';
    WHEN OTHERS THEN
      STR_��ȡֵ := '';
  END;
  RETURN(STR_��ȡֵ);
END FU_������ͨ_ҽԺIDת��;
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
prompt Creating procedure PR_������ͨ_��Դ����
prompt ===============================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_��Դ����(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2, --2006
                                           STR_���ܱ��� IN VARCHAR2,
                                           LOB_��Ӧ���� OUT CLOB,
                                           INT_����ֵ   OUT INTEGER,
                                           STR_������Ϣ OUT VARCHAR2) IS

  STR_ҽԺID     VARCHAR2(50);
  STR_��Դ����ID VARCHAR2(50);

  STR_ԤԼ���� VARCHAR2(50);
  STR_�������� VARCHAR2(50);
  
BEGIN
  BEGIN
    STR_ҽԺID     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_��Դ����ID := FU_������ͨ_�ڵ�ֵ(STR_�������, 'LOCK_ID');
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    IF STR_��Դ����ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���Դ����ID';
      GOTO �˳�;
    END IF;
    
    STR_��������:=FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ,STR_ҽԺID,'1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    BEGIN
      SELECT ��������
        INTO STR_ԤԼ����
        FROM ������ͨ_����
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ҽԺ���� = STR_��������
         AND ƽ̨������ = STR_��Դ����ID
         and ����״̬ = '������';
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 200601;
        STR_������Ϣ := 'δ��ѯ����Դ����ID';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '��֤��Դ����ID����';
        GOTO �˳�;
    END;
  
    UPDATE ������ͨ_����
       SET ����״̬ = '��ȡ��'
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND ҽԺ���� = STR_��������
       AND ƽ̨������ = STR_��Դ����ID;
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '���¶�����¼ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
    UPDATE �������_ԤԼ�Һ�
       SET ȥ���־ = 'ȡ��'
     WHERE �������� = STR_��������
       AND ����ID = STR_ԤԼ����;
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '����ԤԼ��¼ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
    LOB_��Ӧ���� := '<RES></RES>';
    INT_����ֵ   := 0;
    STR_������Ϣ := '���׳ɹ�';
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '������Ӧ����:' || SQLERRM;
      GOTO �˳�;
  END;
  <<�˳�>>
-- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_��������,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);
  RETURN;
END PR_������ͨ_��Դ����;
/

prompt
prompt Creating procedure PR_������ͨ_��Դ����
prompt ===============================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_��Դ����(STR_������� IN VARCHAR2,
                                         STR_ƽ̨��ʶ IN VARCHAR2, --2005
                                         STR_���ܱ��� IN VARCHAR2,
                                         LOB_��Ӧ���� OUT CLOB,
                                         INT_����ֵ   OUT INTEGER,
                                         STR_������Ϣ OUT VARCHAR2) IS

  --���̶�������
  STR_��Դ����ID   VARCHAR2(50);
  STR_�Һ�����ID   VARCHAR2(50);
  STR_�Ű�ID       VARCHAR2(50);
  STR_ҽԺID       VARCHAR2(50);
  STR_����ID       VARCHAR2(50);
  STR_ҽ��ID       VARCHAR2(50);
  STR_��������     VARCHAR2(50);
  STR_ʱ��         VARCHAR2(50);
  STR_��ʱ��ʼʱ�� VARCHAR2(50);
  STR_��ʱ����ʱ�� VARCHAR2(50);
  STR_�Һŷ���     VARCHAR2(50);
  STR_���Ʒ���     VARCHAR2(50);
  STR_��ϯ����     VARCHAR2(50);

  --��ҵ�������
  INT_�޺���   INTEGER;
  INT_�ѹҺ��� INTEGER;

  DAT_��������ʱ�� DATE;
  DAT_ϵͳʱ��     DATE;
  STR_ԤԼ����     VARCHAR2(50);
  STR_��������     VARCHAR2(50);

BEGIN
  BEGIN
    --���̶�������
    STR_��Դ����ID   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'LOCK_ID');
    STR_�Һ�����ID   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'CHANNEL_ID');
    STR_�Ű�ID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REG_ID');
    STR_ҽԺID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_����ID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DEPT_ID');
    STR_ҽ��ID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DOCTOR_ID');
    STR_��������     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REG_DATE');
    STR_ʱ��         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'TIME_FLAG');
    STR_��ʱ��ʼʱ�� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'BEGIN_TIME');
    STR_��ʱ����ʱ�� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'END_TIME');
    STR_�Һŷ���     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REG_FEE');
    STR_���Ʒ���     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'TREAT_FEE');
    STR_��ϯ����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'AGENT_ID');
  
    -- ����ȡDAT_ϵͳʱ�䡿
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    --��������֤��
    IF STR_��Դ����ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���Դ����ID';
      GOTO �˳�;
    END IF;
    IF STR_�Һ�����ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��Һ�����ID';
      GOTO �˳�;
    END IF;
    IF STR_�Ű�ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���Ű�ID';
      GOTO �˳�;
    END IF;
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫�����ID';
      GOTO �˳�;
    END IF;
    IF STR_ҽ��ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽ��ID';
      GOTO �˳�;
    END IF;
    IF STR_�������� IS NULL OR FU_����ת����(STR_��������) IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫����Ч�ĳ�������';
      GOTO �˳�;
    END IF;
    IF TRUNC(SYSDATE) > TO_DATE(STR_��������, 'yyyy-MM-dd') THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫����Ч�ĳ�������';
      GOTO �˳�;
    END IF;
    IF STR_�Һŷ��� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��Һŷ���';
      GOTO �˳�;
    END IF;
    IF STR_���Ʒ��� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫�����Ʒ���';
      GOTO �˳�;
    END IF;
  
    STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    --����֤ƽ̨�����š�
    SELECT COUNT(1)
      INTO INT_����ֵ
      FROM ������ͨ_����
     WHERE ҽԺ���� = STR_��������
       AND ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND ƽ̨������ = STR_��Դ����ID;
  
    IF INT_����ֵ > 0 THEN
      INT_����ֵ   := 200504;
      STR_������Ϣ := '����ID�Ѵ���';
      GOTO �˳�;
    END IF;
  
    --����֤�Ű�ID��
    BEGIN
      SELECT A.�޺���, A.�ѹҺ���
        INTO INT_�޺���, INT_�ѹҺ���
        FROM �������_���Ű�ʱ�α� A, �������_�����Ű��¼ B
       WHERE A.�������� = B.��������
         AND A.��¼ID = B.��¼ID
         AND A.�������� = STR_��������
         AND B.�Ű����� = TO_DATE(STR_��������, 'yyyy-MM-dd')
         AND A.�հ�α�ʶ = STR_�Ű�ID;
      IF INT_�޺��� >= 0 THEN
        IF INT_�ѹҺ��� >= INT_�޺��� THEN
          INT_����ֵ   := 200505;
          STR_������Ϣ := '���Ű�Һ���������ʣ���Դ��';
          GOTO �˳�;
        ELSE
          SELECT COUNT(1)
            INTO INT_����ֵ
            FROM �������_ԤԼ�Һ�
           WHERE �������� = STR_��������
             AND �հ�α�ʶ = STR_�Ű�ID
             AND ȥ���־ = 'ռ��'
             AND ��ʱʱ�� > SYSDATE;
          IF INT_����ֵ >= INT_�޺��� - INT_�ѹҺ��� THEN
            INT_����ֵ   := 200506;
            STR_������Ϣ := '���Ű��µĵ�ǰ��Դ�ѱ�ռ��';
            GOTO �˳�;
          END IF;
        END IF;
      END IF;
    
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 200501;
        STR_������Ϣ := '��Ч���Ű�';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '��֤�Ű౨��';
        GOTO �˳�;
    END;
  
    --����֤�����Ű���Ϣ��
    IF STR_����ID IS NOT NULL THEN
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM �������_�����Ű��¼ T, �������_���Ű�ʱ�α� TT
       WHERE T.�������� = TT.��������
         AND T.��¼ID = TT.��¼ID
         AND T.�������� = STR_��������
         AND T.���ұ��� = STR_����ID
         AND TT.�հ�α�ʶ = STR_�Ű�ID
         AND T.�Ű����� = TO_DATE(STR_��������, 'yyyy-MM-dd');
    
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 200501;
        STR_������Ϣ := '��Ч���Ű�';
        GOTO �˳�;
      END IF;
    END IF;
  
    --����֤ҽ���Ű���Ϣ��
    IF STR_ҽ��ID IS NOT NULL THEN
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM �������_�����Ű��¼ T, �������_���Ű�ʱ�α� TT
       WHERE T.�������� = TT.��������
         AND T.��¼ID = TT.��¼ID
         AND T.�������� = STR_��������
         AND T.ҽ������ = STR_ҽ��ID
         AND T.����״̬ = '1'
         AND TT.�հ�α�ʶ = STR_�Ű�ID
         AND T.�Ű����� = TO_DATE(STR_��������, 'yyyy-MM-dd');
    
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 200501;
        STR_������Ϣ := '��Ч���Ű�';
        GOTO �˳�;
      END IF;
    END IF;
  
    -- �����ɹ���ʱ�䡿
    SELECT SYSDATE + (1 / (24 * 60)) * 15 INTO DAT_��������ʱ�� FROM DUAL;
  
    --������ԤԼ���š�
    SELECT SEQ_�������_ԤԼ�Һ�_ΨһID.NEXTVAL
      INTO STR_ԤԼ����
      FROM DUAL;
  
    -- ����ԤԼ��¼
    INSERT INTO �������_ԤԼ�Һ�
      (��������,
       ����ID,
       ȥ���־,
       ԤԼ����,
       ��¼�˱���,
       ��¼ʱ��,
       ֧����־,
       ��ʱʱ��,
       �հ�α�ʶ)
    VALUES
      (STR_��������,
       STR_ԤԼ����,
       'ռ��',
       '����ԤԼ',
       STR_ƽ̨��ʶ,
       DAT_ϵͳʱ��,
       '��',
       DAT_��������ʱ��,
       STR_�Ű�ID);
  
    -- ���붩��
    INSERT INTO ������ͨ_����
      (��ˮ��,
       ƽ̨��ʶ,
       ҽԺ����,
       ��������,
       ƽ̨������,
       ��������,
       ����ʱ��,
       ����״̬,
       �Һ�����,
       ������,
       ����ʱ��,
       ������,
       ����ʱ��)
    VALUES
      (SEQ_������ͨ_����_��ˮ��.NEXTVAL,
       STR_ƽ̨��ʶ,
       STR_��������,
       STR_ԤԼ����,
       STR_��Դ����ID,
       'ԤԼ�Һ�',
       DAT_��������ʱ��,
       '������',
       STR_�Һ�����ID,
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
  
    LOB_��Ӧ���� := '<RES></RES>';
    INT_����ֵ   := 0;
    STR_������Ϣ := '���׳ɹ�';
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '������Ӧ����:' || SQLERRM;
      GOTO �˳�;
    
  END;

  <<�˳�>>
-- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_��������,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => DAT_ϵͳʱ��,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);
  RETURN;

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

  STR_�������� VARCHAR2(50);
BEGIN
  BEGIN
  
    --�����������
    STR_ҽԺID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_��鱨�浥�� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REPORT_ID');
    STR_�û�Ժ��ID   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_PATIENT_ID');
  
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    IF STR_��鱨�浥�� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���鱨�浥��';
      GOTO �˳�;
    END IF;
  
    STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    STR_SQL := 'SELECT S.�������� AS HOS_ID,
                           S.����ID AS HOSP_PATIENT_ID,
                           '''' AS PATIENT_IDCARD_TYPE,
                           (SELECT A.���֤��
                              FROM ������Ŀ_������Ϣ A
                             WHERE A.�������� = S.��������
                               AND A.����ID = S.����ID) AS PATIENT_IDCARD_NO,
                           '''' AS PATIENT_CARD_TYPE,
                           '''' AS PATIENT_CARD_NO,
                           (SELECT A.����
                              FROM ������Ŀ_������Ϣ A
                             WHERE A.�������� = S.��������
                               AND A.����ID = S.����ID) AS PATIENT_NAME,
                           (SELECT A.�Ա�
                              FROM ������Ŀ_������Ϣ A
                             WHERE A.�������� = S.��������
                               AND A.����ID = S.����ID) AS PATIENT_SEX,
                           (SELECT A.����
                              FROM ������Ŀ_������Ϣ A
                             WHERE A.�������� = S.��������
                               AND A.����ID = S.����ID) AS PATIENT_AGE,
                           '''' AS VISIT_NUMBER,
                           ''�Է�'' AS MEDICAL_INSURANNCE_TYPE,
                           J.�걾���� AS SPECIMEN_NAME,
                           J.������ AS SPECIMEN_ID,
                           S.��Ŀ���� AS ITEM_NAME,
                           '''' AS COMPLAINT,
                           J.������ AS DIAGNOSIS,
                           J.������� AS SEEN,
                           J.���ֱ������� AS "CONTENT",
                           TO_CHAR(J.����ʱ��, ''YYYY-MM-DD HH24:MI:SS'') AS REPORT_TIME,
                           (SELECT B.��������
                              FROM ������Ŀ_�������� B
                             WHERE B.�������� = S.��������
                               AND B.���ұ��� = S.ִ�п��ұ���) AS DEPT_NAME,
                           (SELECT C.��Ա����
                              FROM ������Ŀ_��Ա���� C
                             WHERE C.�������� = S.��������
                               AND C.��Ա���� = S.ҽ������) AS DOCTOR_NAME,
                           (SELECT C.��Ա����
                              FROM ������Ŀ_��Ա���� C
                             WHERE C.�������� = S.��������
                               AND C.��Ա���� = J.���ҽ������) AS REVIEW_NAME,
                           TO_CHAR(J.���ʱ��, ''YYYY-MM-DD HH24:MI:SS'') AS REVIEW_TIME,
                           '''' AS REMARK
                      FROM ������_���� S, ������_��� J
                     WHERE S.�������� = J.��������
                       AND S.���뵥ID = J.���뵥ID     
                       AND S.��������=''' || STR_�������� || '''
                       AND J.���浥�� =''' || STR_��鱨�浥�� || '''';
  
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
      INT_����ֵ   := 99;
      STR_������Ϣ := '��Ӧ���󱨴�:' || SQLERRM;
      GOTO �˳�;
  END;

  <<�˳�>>
-- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_��������,
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
                                             STR_ƽ̨��ʶ IN VARCHAR2, --8001
                                             STR_���ܱ��� IN VARCHAR2,
                                             LOB_��Ӧ���� OUT CLOB,
                                             INT_����ֵ   OUT INTEGER,
                                             STR_������Ϣ OUT VARCHAR2) IS

  STR_SQL VARCHAR2(2000);
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

  STR_�������� VARCHAR2(50);
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
  
    --��������֤��
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := '1';
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    IF STR_��ʼ���� IS NULL OR FU_����ת����(STR_��ʼ����) IS NULL THEN
      INT_����ֵ   := '1';
      STR_������Ϣ := '�봫����Ч����ʼ����';
      GOTO �˳�;
    END IF;
    IF STR_�������� IS NULL OR FU_����ת����(STR_��������) IS NULL THEN
      INT_����ֵ   := '1';
      STR_������Ϣ := '�봫����Ч�Ľ�������';
      GOTO �˳�;
    END IF;
  
    SELECT MONTHS_BETWEEN(TO_DATE(STR_��������, 'yyyy-MM-dd'),
                          TO_DATE(STR_��ʼ����, 'yyyy-MM-dd'))
      INTO INT_����ֵ
      FROM DUAL;
    IF INT_����ֵ > 3 OR INT_����ֵ < 0 THEN
      INT_����ֵ   := '1';
      STR_������Ϣ := '�봫��С��3���µ����ڷ�Χ';
      GOTO �˳�;
    END IF;
  
    IF STR_�û�֤������ IS NULL AND STR_�û����� IS NULL THEN
      INT_����ֵ   := '1';
      STR_������Ϣ := '�봫��֤������򿨺�';
      GOTO �˳�;
    END IF;
    IF STR_�û����� IS NULL THEN
      INT_����ֵ   := '1';
      STR_������Ϣ := '�봫���û�����';
      GOTO �˳�;
    END IF;
    STR_�û��Ա� := FU_������ͨ_��֤�Ա�(STR_�û��Ա�);
    IF STR_�û��Ա� = '-1' THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���û��Ա�';
      GOTO �˳�;
    END IF;
  
    STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    IF STR_�û�Ժ��ID IS NOT NULL THEN
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM ������ͨ_�û���Ϣ
       WHERE ����ID = STR_�û�Ժ��ID;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := '800102';
        STR_������Ϣ := '�û�������';
        GOTO �˳�;
      END IF;
    ELSE
      BEGIN
        SELECT T.����ID
          INTO STR_�û�Ժ��ID
          FROM ������ͨ_�û���Ϣ T
         WHERE T.ƽ̨��ʶ = STR_ƽ̨��ʶ
           AND T.ҽԺ���� = STR_��������
           AND T.���� = STR_�û�����
           AND T.�Ա� = STR_�û��Ա�
           AND (T.֤������ = STR_�û�֤������ OR STR_�û�֤������ IS NULL)
           AND (T.�û����� = STR_�û����� OR STR_�û����� IS NULL)
           AND ROWNUM = 1;
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          INT_����ֵ   := '800102';
          STR_������Ϣ := '�û�������';
          GOTO �˳�;
        WHEN OTHERS THEN
          INT_����ֵ   := 99;
          STR_������Ϣ := 'ƥ���û���Ϣʱ����';
          GOTO �˳�;
      END;
    END IF;
  
    STR_SQL := 'SELECT J.���浥�� AS REPORT_ID,
                     J.������ AS DIAGNOSIS,
                     S.��Ŀ���� AS ITEM_NAME,
                     J.�걾���� AS SPECIMEN_NAME,
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
                 AND S.��������=' || STR_�������� || ' AND S.����ID=''' ||
               STR_�û�Ժ��ID || ''' AND J.����ʱ�� BETWEEN TO_DATE(''' || STR_��ʼ���� ||
               ''',''yyyy-MM-dd'') AND TO_DATE(''' || STR_�������� ||
               ''',''yyyy-MM-dd'')';
  
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'REPORT_INFO', 'REPORT_INFO');
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
    
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'HOS_ID',
                                STR_ҽԺID);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'HOSP_PATIENT_ID',
                                STR_�û�Ժ��ID);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'PATIENT_IDCARD_TYPE',
                                STR_�û�֤������);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'PATIENT_IDCARD_NO',
                                STR_�û�֤������);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'PATIENT_CARD_TYPE',
                                STR_�û�������);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'PATIENT_CARD_NO',
                                STR_�û�����);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'PATIENT_NAME',
                                STR_�û�����);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'PATIENT_SEX',
                                STR_�û��Ա�);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'PATIENT_AGE',
                                STR_�û�����);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'REPORT_INFO',
                                'VISIT_NUMBER',
                                '');
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����,
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
      INT_����ֵ   := 99;
      STR_������Ϣ := '��Ӧ���󱨴�' || SQLERRM;
      GOTO �˳�;
    
  END;

  <<�˳�>>
-- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_��������,
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
prompt Creating procedure PR_������ͨ_���鱨���ѯ
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_���鱨���ѯ(STR_������� IN VARCHAR2,
                                             STR_ƽ̨��ʶ IN VARCHAR2, --8002
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
  STR_�������� VARCHAR2(50);

BEGIN
  BEGIN
    --���������������
    STR_ҽԺID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_���鱨�浥�� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REPORT_ID');
    STR_�û�Ժ��ID   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_PATIENT_ID');
  
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    IF STR_���鱨�浥�� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫����鱨�浥��';
      GOTO �˳�;
    END IF;
    
    STR_��������:=FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ,STR_ҽԺID,'1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    STR_SQL := 'SELECT S.����ID AS HOSP_PATIENT_ID,
                           '''' AS PATIENT_IDCARD_TYPE,
                           (SELECT A.���֤��
                              FROM ������Ŀ_������Ϣ A
                             WHERE A.�������� = S.��������
                               AND A.����ID = S.����ID) AS PATIENT_IDCARD_NO,
                           '''' AS PATIENT_CARD_TYPE,
                           '''' AS PATIENT_CARD_NO,
                           (SELECT A.����
                              FROM ������Ŀ_������Ϣ A
                             WHERE A.�������� = S.��������
                               AND A.����ID = S.����ID) AS PATIENT_NAME,
                           (SELECT A.�Ա�
                              FROM ������Ŀ_������Ϣ A
                             WHERE A.�������� = S.��������
                               AND A.����ID = S.����ID) AS PATIENT_SEX,
                           (SELECT A.����
                              FROM ������Ŀ_������Ϣ A
                             WHERE A.�������� = S.��������
                               AND A.����ID = S.����ID) AS PATIENT_AGE,
                           '''' AS VISIT_NUMBER,
                           ''�Է�'' AS MEDICAL_INSURANNCE_TYPE,
                           J.������ AS DIAGNOSIS,
                           S.��Ŀ���� AS ITEM_NAME,
                           J.�������� AS SPECIMEN_NAME,
                           J.������ AS SPECIMEN_ID,                                           
                           TO_CHAR(J.����ʱ��, ''YYYY-MM-DD HH24:MI:SS'') AS REPORT_TIME,
                           (SELECT B.��������
                              FROM ������Ŀ_�������� B
                             WHERE B.�������� = S.��������
                               AND B.���ұ��� = S.ִ�п��ұ���) AS DEPT_NAME,
                           (SELECT C.��Ա����
                              FROM ������Ŀ_��Ա���� C
                             WHERE C.�������� = S.��������
                               AND C.��Ա���� = S.ҽ������) AS DOCTOR_NAME,
                           (SELECT C.��Ա����
                              FROM ������Ŀ_��Ա���� C
                             WHERE C.�������� = S.��������
                               AND C.��Ա���� = J.���ҽ������) AS REVIEW_NAME,
                           TO_CHAR(J.���ʱ��, ''YYYY-MM-DD HH24:MI:SS'') AS REVIEW_TIME,
                           '''' AS REMARK
                      FROM ������_���� S, ������_��� J
                     WHERE S.�������� = J.��������
                       AND S.���뵥ID = J.���뵥ID     
                       AND S.��������=''' || STR_�������� || '''
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
               WHERE M.�������� = ''' || STR_�������� || '''
                 AND M.���浥ID = ''' || STR_���鱨�浥�� || '''';
      
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
      INT_����ֵ   := 99;
      STR_������Ϣ := '��Ӧ���󱨴�:' || SQLERRM;
      GOTO �˳�;
    
  END;

  <<�˳�>>
-- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_��������,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  RETURN;

END PR_������ͨ_���鱨���ѯ;
/

prompt
prompt Creating procedure PR_������ͨ_�ɷѵ����ڽɷ�
prompt ==================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_�ɷѵ����ڽɷ�(STR_��������   IN VARCHAR2,
                                            STR_����ID     IN VARCHAR2,
                                            STR_���ﲡ���� IN VARCHAR2,
                                            STR_��Ʊ��     IN VARCHAR2,
                                            STR_�ɷ�����   VARCHAR2, --1��ʾ�ɷ�    2��ʾ�˷�
                                            NUM_�ɷѽ��   NUMBER) IS
  PRAGMA AUTONOMOUS_TRANSACTION; --�������ﲻӰ��������
  STR_ƽ̨��ʶ VARCHAR2(10) := 12320;
BEGIN

  BEGIN
    IF STR_�ɷ����� = '1' THEN   
      UPDATE ������ͨ_���� T
         SET T.����״̬         = '��֧��',
             T.ʵ�����         = NUM_�ɷѽ��,
             T.ҽ��ͳ��֧����� = 0,
             T.ҽԺ֧����       = STR_��Ʊ��,
             T.֧��ʱ��         = SYSDATE,
             T.֧������         = '6', --����֧��
             T.������           = STR_ƽ̨��ʶ,
             T.����ʱ��         = SYSDATE
       WHERE T.ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND T.ҽԺ���� = STR_��������
         AND T.����ID = STR_����ID
         AND T.���ﲡ���� = STR_���ﲡ����
         AND T.�������� = '����ɷ�'
         AND T.����״̬ = '��֧��';
    ELSE
      UPDATE ������ͨ_���� T
         SET T.����״̬ = '���˿�'
       WHERE T.ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND T.ҽԺ���� = STR_��������
         AND T.����ID = STR_����ID
         AND T.���ﲡ���� = STR_���ﲡ����
         AND T.�������� = '����ɷ�'
         AND T.����״̬ = '��֧��';
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;
      RETURN;
    
  END;
  COMMIT;

  RETURN;
END PR_������ͨ_�ɷѵ����ڽɷ�;
/

prompt
prompt Creating procedure PR_������ͨ_�ɷѵ�֧��
prompt ================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_�ɷѵ�֧��(STR_������� IN VARCHAR2,
                                          STR_ƽ̨��ʶ IN VARCHAR2,
                                          STR_���ܱ��� IN VARCHAR2, --3003
                                          LOB_��Ӧ���� OUT CLOB,
                                          INT_����ֵ   OUT INTEGER,
                                          STR_������Ϣ OUT VARCHAR2) IS

  STR_SQL VARCHAR2(1000);
  --���̶�������
  STR_ҽԺID           VARCHAR2(50);
  STR_ƽ̨������       VARCHAR2(50);
  STR_ҽԺ������       VARCHAR2(50);
  STR_��ˮ��           VARCHAR2(50);
  STR_��������         VARCHAR2(50);
  STR_����ʱ��         VARCHAR2(50);
  STR_֧������ID       VARCHAR2(50);
  STR_�ܽ��           VARCHAR2(50);
  STR_Ӧ�����         VARCHAR2(50);
  STR_�����Ը����     VARCHAR2(50);
  STR_ҽ��ͳ��֧����� VARCHAR2(50);
  STR_������Ӧ����     VARCHAR2(50);
  STR_������Ӧ����     VARCHAR2(50);
  STR_�̻���           VARCHAR2(50);
  STR_�ն˺�           VARCHAR2(50);
  STR_���п���         VARCHAR2(50);
  STR_������֧���ʺ�   VARCHAR2(50);
  STR_����ԱID         VARCHAR2(50);
  STR_�վݺ�           VARCHAR2(50);

  --��ҵ�������
  DAT_ϵͳʱ��     DATE;
  CUR_Ԥ����Ϣ     SYS_REFCURSOR;
  STR_Ԥ������ϸ VARCHAR2(4000);

  STR_ִ�п��ұ���   VARCHAR(50);
  NUM_�����ܶ�       NUMBER(18, 3);
  NUM_�Ը��ܶ�       NUMBER(18, 3);
  NUM_�Ż��ܶ�       NUMBER(18, 3);
  NUM_Ӧ���ܶ�       NUMBER(18, 3);
  NUM_�����ܶ�       NUMBER(18, 3);
  NUM_ʵ���ܶ�       NUMBER(18, 3);
  NUM_�����ܶ�       NUMBER(18, 3);
  NUM_������֧���ܶ� NUMBER(18, 3);

  INT_С��λ��       INTEGER;
  STR_���뷽ʽ       VARCHAR2(50);
  STR_�շ�ֱ�ӿۿ�� VARCHAR2(50);
  STR_��ִ�п��ҷ�Ʊ VARCHAR2(50);

  STR_�շ���� VARCHAR2(50);
  STR_����ID   VARCHAR2(50);
  STR_�Һ���� VARCHAR2(50);
  STR_ҽ����   VARCHAR2(50);
  STR_��Ʊ��   VARCHAR2(50);
  STR_��Ʊ��� VARCHAR2(50);

  STR_��������     VARCHAR2(50);
  STR_֧����ʽ     VARCHAR2(50);
  STR_�������ͱ��� VARCHAR2(50);
  STR_������������ VARCHAR2(50);
  STR_���ﲡ����   VARCHAR2(50);
  NUM_�������     NUMBER(10, 3);

  STR_�������� VARCHAR2(50);
BEGIN
  BEGIN
    --���̶�����������
    STR_ҽԺID           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_ƽ̨������       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ORDER_ID');
    STR_ҽԺ������       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_SEQUENCE');
    STR_��ˮ��           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'SERIAL_NUM');
    STR_��������         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_DATE');
    STR_����ʱ��         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_TIME');
    STR_֧������ID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_CHANNEL_ID');
    STR_�ܽ��           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_TOTAL_FEE');
    STR_Ӧ�����         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_BEHOOVE_FEE');
    STR_�����Ը����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_ACTUAL_FEE');
    STR_ҽ��ͳ��֧����� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_MI_FEE');
    STR_������Ӧ����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_RES_CODE');
    STR_������Ӧ����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_RES_DESC');
    STR_�̻���           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'MERCHANT_ID');
    STR_�ն˺�           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'TERMINAL_ID');
    STR_���п���         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'BANK_NO');
    STR_������֧���ʺ�   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAY_ACCOUNT');
    STR_����ԱID         := FU_������ͨ_�ڵ�ֵ(STR_�������, 'OPERATOR_ID');
    STR_�վݺ�           := FU_������ͨ_�ڵ�ֵ(STR_�������, 'RECEIPT_ID');
  
    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
    STR_��������     := '����ɷ�';
    STR_�������ͱ��� := '1';
    STR_������������ := '�ֽ�';
  
    --��������֤��
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    IF STR_ƽ̨������ IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ƽ̨������';
      GOTO �˳�;
    END IF;
    IF STR_ҽԺ������ IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫�����ǼǺ�';
      GOTO �˳�;
    END IF;
    IF STR_��ˮ�� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫����ˮ��';
      GOTO �˳�;
    END IF;
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫�뽻������';
      GOTO �˳�;
    END IF;
    IF STR_����ʱ�� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫�뽻��ʱ��';
      GOTO �˳�;
    END IF;
    IF STR_֧������ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��֧������ID';
      GOTO �˳�;
    END IF;
    IF STR_�ܽ�� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���ܽ��';
      GOTO �˳�;
    END IF;
    IF STR_Ӧ����� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��Ӧ�����';
      GOTO �˳�;
    END IF;
    IF STR_�����Ը���� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫������Ը����';
      GOTO �˳�;
    END IF;
    IF STR_ҽ��ͳ��֧����� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽ��ͳ��֧�����';
      GOTO �˳�;
    END IF;
  
    STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    -- ��ϵͳ������
    BEGIN
      SELECT ֵ
        INTO STR_���뷽ʽ
        FROM ������Ŀ_���������б�
       WHERE �������� = '53'
         AND �������� = STR_��������;
    EXCEPTION
      WHEN OTHERS THEN
        STR_���뷽ʽ := '2';
    END;
  
    BEGIN
      SELECT TO_NUMBER(ֵ)
        INTO INT_С��λ��
        FROM ������Ŀ_���������б�
       WHERE �������� = '52'
         AND �������� = STR_��������;
    EXCEPTION
      WHEN OTHERS THEN
        INT_С��λ�� := 2;
    END;
  
    BEGIN
      SELECT ֵ
        INTO STR_�շ�ֱ�ӿۿ��
        FROM ������Ŀ_���������б�
       WHERE �������� = '164'
         AND �������� = STR_��������;
    EXCEPTION
      WHEN OTHERS THEN
        STR_�շ�ֱ�ӿۿ�� := '��';
    END;
  
    BEGIN
      SELECT ֵ
        INTO STR_��ִ�п��ҷ�Ʊ
        FROM ������Ŀ_���������б�
       WHERE �������� = '50'
         AND �������� = STR_��������;
    EXCEPTION
      WHEN OTHERS THEN
        STR_��ִ�п��ҷ�Ʊ := '0';
    END;
  
    BEGIN
      SELECT �������, ֧����ʽ
        INTO NUM_�������, STR_֧����ʽ
        FROM ������ͨ_ƽ̨����
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ��Ч״̬ = '1';
    EXCEPTION
      WHEN OTHERS THEN
        NUM_������� := 100;
    END;
  
    --����֤������
    BEGIN
      SELECT ��������, ����ID, ���ﲡ����
        INTO STR_�շ����, STR_����ID, STR_���ﲡ����
        FROM ������ͨ_����
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ҽԺ���� = STR_��������
         AND ҽԺ������ = STR_ҽԺ������
         AND Ӧ����� = TO_NUMBER(STR_Ӧ�����) / NUM_�������
         AND �������� = STR_��������
         AND ����״̬ = '��֧��';
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 300301;
        STR_������Ϣ := '�ɷѶ���������';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
  
    -- ����֤ҽ��״̬��
    SELECT COUNT(1)
      INTO INT_����ֵ
      FROM �������_����ҽ����ϸ M, �������_����ҽ�� Y
     WHERE M.�������� = Y.��������
       AND M.����ID = Y.����ID
       AND M.���ﲡ���� = Y.���ﲡ����
       AND M.��� = Y.���
       AND M.ҽ���� = Y.ҽ����
       AND M.�������� = STR_��������
       AND M.����ID = STR_����ID
       AND M.�շ���� = STR_�շ����
       AND Y.�շ�״̬ = '����δ�շ�'
       AND Y.���۷�ʽ <> '�˷��Զ�����';
  
    IF INT_����ֵ <= 0 THEN
      INT_����ֵ   := 300303;
      STR_������Ϣ := '�ɷѶ����ѹر�';
      GOTO �˳�;
    END IF;
  
    -- ����֤����״̬��
    SELECT COUNT(1)
      INTO INT_����ֵ
      FROM �������_����ҽ����ϸ M, �������_���ﴦ�� C
     WHERE M.�������� = C.��������
       AND M.����ID = C.����ID
       AND M.���ﲡ���� = C.���ﲡ����
       AND M.��� = C.���
       AND M.ҽ���� = C.ҽ����
       AND M.��ˮ�� = C.ҽ����ˮ��
       AND M.�������� = STR_��������
       AND M.����ID = STR_����ID
       AND M.�շ���� = STR_�շ����;
  
    IF INT_����ֵ > 0 THEN
      INT_����ֵ   := 300303;
      STR_������Ϣ := '�ɷѶ����ѹر�';
      GOTO �˳�;
    END IF;
  
    -- ����֤ҽ����ϸ��
    BEGIN
      SELECT DISTINCT �Һ����, ҽ����
        INTO STR_�Һ����, STR_ҽ����
        FROM �������_����ҽ����ϸ
       WHERE �������� = STR_��������
         AND ����ID = STR_����ID
         AND �շ���� = STR_�շ����;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 300301;
        STR_������Ϣ := '�ɷѶ���������';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
  
    -- ���ɷ�Ʊ��
    SELECT FU_����_��ȡ��ǰƱ�ݺ�(STR_��������, STR_ƽ̨��ʶ, '1')
      INTO STR_��Ʊ��
      FROM DUAL;
  
    IF STR_��Ʊ�� = '�뵽����������Ʊ��' THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '�ò���Ա�޷�Ʊ��,��֪ͨ����������Ʊ��!';
      GOTO �˳�;
    END IF;
  
    -- ���ɷ�Ʊ���
    SELECT SEQ_�������_��Ʊ�Ǽ�_��Ʊ���.NEXTVAL
      INTO STR_��Ʊ���
      FROM DUAL;
  
    -- �����ܴ���
    BEGIN
    
      PR_�������_Ԥ����(STR_��������       => STR_��������,
                  STR_Ψһ����       => STR_�շ����,
                  STR_��Ա���ͱ���   => '-1',
                  DEC_�Ż�ֵ         => 0,
                  NUM_�����ܶ�       => 0,
                  STR_��ִ�п��ҷ�Ʊ => STR_��ִ�п��ҷ�Ʊ,
                  STR_���뷽ʽ       => STR_���뷽ʽ,
                  INT_����λ��       => INT_С��λ��,
                  CUR_Ԥ����Ϣ       => CUR_Ԥ����Ϣ,
                  INT_����ֵ         => INT_����ֵ,
                  STR_������Ϣ       => STR_������Ϣ);
    
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '����Ԥ�����¼ʧ��!';
        GOTO �˳�;
      END IF;
    
      LOOP
        FETCH CUR_Ԥ����Ϣ
          INTO STR_ִ�п��ұ���,
               NUM_�����ܶ�,
               NUM_�����ܶ�,
               NUM_�Ը��ܶ�,
               NUM_�Ż��ܶ�,
               NUM_Ӧ���ܶ�,
               NUM_�����ܶ�,
               NUM_ʵ���ܶ�,
               NUM_������֧���ܶ�;
        EXIT WHEN CUR_Ԥ����Ϣ%NOTFOUND;
      
        STR_Ԥ������ϸ := STR_Ԥ������ϸ || STR_��Ʊ�� || '~' || STR_ִ�п��ұ��� || '~' ||
                      NUM_�����ܶ� || '~' || NUM_�����ܶ� || '~' || NUM_�Ը��ܶ� || '~' ||
                      NUM_�Ż��ܶ� || '~' || NUM_Ӧ���ܶ� || '~' || NUM_�����ܶ� || '~' ||
                      NUM_ʵ���ܶ� || '~' || STR_��Ʊ��� || '~' || 0 || '~' ||
                      NUM_Ӧ���ܶ� || '~' || 0 || '~' || NUM_Ӧ���ܶ� || '~' ||
                      NUM_������֧���ܶ� || '~~|';
      END LOOP;
    
      CLOSE CUR_Ԥ����Ϣ;
    
      IF STR_Ԥ������ϸ IS NULL THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '����Ԥ�����¼ʧ��!';
        GOTO �˳�;
      END IF;
    
      STR_Ԥ������ϸ := '��Ʊ��,ִ�п��ұ���,�����ܶ�,�����ܶ�,�Ը��ܶ�,�Ż��ܶ�,Ӧ���ܶ�,�����ܶ�,ʵ���ܶ�,��Ʊ���,ԭ��Ʊҽ��֧ͨ�����,�����˷��ܶ�,���ο��˷��ܶ�,�����ֽ��˷��ܶ�,������֧���ܶ�##' ||
                    STR_Ԥ������ϸ;
    
      DBMS_OUTPUT.PUT_LINE(STR_Ԥ������ϸ);
    
      PR_�������_�����շ�(STR_��������       => STR_��������,
                   STR_����ID         => STR_����ID,
                   STR_���ﲡ����     => STR_���ﲡ����,
                   STR_�Һ����       => STR_�Һ����,
                   STR_�������ͱ���   => STR_�������ͱ���,
                   STR_������������   => STR_������������,
                   STR_�շ����       => STR_�շ����,
                   STR_Ԥ������ϸ   => STR_Ԥ������ϸ,
                   STR_���ʽ       => STR_֧����ʽ || '|' || NUM_Ӧ���ܶ� || '@',
                   STR_ԭ��Ʊ���     => '0',
                   INT_���۱���       => 0,
                   STR_���۷�ʽ       => '-1',
                   STR_����Ա����     => STR_ƽ̨��ʶ,
                   STR_����Ա����     => STR_ƽ̨��ʶ,
                   STR_�շ�ֱ�ӿۿ�� => STR_�շ�ֱ�ӿۿ��,
                   INT_С��λ��       => INT_С��λ��,
                   STR_���뷽ʽ       => STR_���뷽ʽ,
                   STR_������Ϣ       => '',
                   DAT_ϵͳʱ��       => DAT_ϵͳʱ��,
                   INT_����ֵ         => INT_����ֵ,
                   STR_������Ϣ       => STR_������Ϣ,
                   STR_һ��ͨ���׺�   => '',
                   STR_���㷽ʽ       => '');
    
      IF INT_����ֵ <> 1 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '����ɷѼ�¼ʧ��!';
        GOTO �˳�;
      END IF;
    
      -- ���¶���״̬
      UPDATE ������ͨ_����
         SET ����״̬         = '��֧��',
             ƽ̨������       = STR_ƽ̨������,
             ʵ�����         = TO_NUMBER(STR_�����Ը����) / NUM_�������,
             ҽ��ͳ��֧����� = 0,
             ҽԺ֧����       = STR_��Ʊ��,
             ƽ̨������ˮ��   = STR_��ˮ��,
             ֧��ʱ��         = TO_DATE(STR_�������� || ' ' || STR_����ʱ��,
                                    'yyyy-MM-dd hh24:mi:ss'),
             ֧������         = STR_֧������ID,
             ������           = STR_ƽ̨��ʶ,
             ����ʱ��         = DAT_ϵͳʱ��
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ����ID = STR_����ID
         AND ҽԺ������ = STR_ҽԺ������
         AND �������� = STR_��������
         AND ����״̬ = '��֧��';
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '���¶���ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      STR_SQL := 'SELECT ''' || STR_��Ʊ�� ||
                 ''' AS HOSP_ORDER_ID,
    '''' AS RECEIPT_ID,'''' AS HOSP_MEDICAL_NUM ,'''' AS  HOSP_REMARK FROM DUAL';
    
      LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', '');
    
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    
      COMMIT;
    
    EXCEPTION
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '��Ӧ���󱨴�' || SQLERRM;
        GOTO �˳�;
    END;
  
  END;
  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_��������,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => DAT_ϵͳʱ��,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);
  ROLLBACK;
  RETURN;
END PR_������ͨ_�ɷѵ�֧��;
/

prompt
prompt Creating procedure PR_������ͨ_�ɷѶ�����ѯ
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_�ɷѶ�����ѯ(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2,
                                           STR_���ܱ��� IN VARCHAR2,
                                           LOB_��Ӧ���� OUT CLOB,
                                           INT_����ֵ   OUT INTEGER,
                                           STR_������Ϣ OUT VARCHAR2) IS

  STR_ҽԺID             VARCHAR2(50);
  STR_ƽ̨�ɷѶ�����     VARCHAR2(2000);
  STR_Ժ�ڽɷѵ�ΨһID   VARCHAR2(2000);
  STR_��ѯ״̬����       VARCHAR2(50);
  STR_�ɷѵ�������ʼ���� VARCHAR2(50);
  STR_�ɷѵ������������� VARCHAR2(50);

  STR_��ѯ��� VARCHAR2(50);

  STR_��ѯ״̬���ʹ� VARCHAR2(50);

  I            INTEGER;
  STR_SQL      VARCHAR2(3000);
  STR_�������� VARCHAR2(50);

BEGIN
  BEGIN
  
    STR_ҽԺID             := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_��ѯ״̬����       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'QUERY_DATE/QUERY_TYPE');
    STR_�ɷѵ�������ʼ���� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'QUERY_DATE/BEGIN_DATE');
    STR_�ɷѵ������������� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'QUERY_DATE/END_DATE');
  
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
  
    STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    IF STR_��ѯ״̬���� IS NULL AND STR_�ɷѵ�������ʼ���� IS NULL AND
       STR_�ɷѵ������������� IS NULL THEN
      STR_��ѯ��� := 1;
    ELSE
      STR_��ѯ��� := 2;
      IF STR_��ѯ״̬���� = '1' THEN
        STR_��ѯ״̬���ʹ� := '''��֧��''';
      ELSIF STR_��ѯ״̬���� = '2' THEN
        STR_��ѯ״̬���ʹ� := '''��֧��'',''���˿�''';
      ELSE
        STR_��ѯ״̬���ʹ� := '''��֧��'',''��֧��'',''���˿�''';
      END IF;
    END IF;
  
    I := 1;
    LOOP
      EXIT WHEN XMLTYPE(STR_�������).EXISTSNODE('/REQ/QUERY_ORDER[' || I || ']') = 0;
      IF I = 1 THEN
      
        STR_ƽ̨�ɷѶ�����   := '''' || XMLTYPE(STR_�������).EXTRACT('/REQ/QUERY_ORDER[
                        ' || I ||']/ORDER_NO/text()').GETSTRINGVAL() || '''';
        STR_Ժ�ڽɷѵ�ΨһID := '''' || XMLTYPE(STR_�������).EXTRACT('/REQ/QUERY_ORDER[
                        ' || I ||']/HOSP_SEQUENCE / text()').GETSTRINGVAL() || '''';
      ELSE
        STR_ƽ̨�ɷѶ�����   := STR_ƽ̨�ɷѶ����� || ',' || '''' || XMLTYPE(STR_�������).EXTRACT('/REQ/QUERY_ORDER[
                        ' || I ||']/ORDER_NO/text()').GETSTRINGVAL() || '''';
        STR_Ժ�ڽɷѵ�ΨһID := STR_ƽ̨�ɷѶ����� || ',' || '''' || XMLTYPE(STR_�������).EXTRACT('/REQ/QUERY_ORDER [
                        ' || I ||']/HOSP_SEQUENCE/text()').GETSTRINGVAL() || '''';
      END IF;
    
      I := I + 1;
    END LOOP;
  
    STR_SQL := 'SELECT A.���� AS USER_NAME,
                   A.����ID AS HOSP_PATIENT_ID,
                   A.֤������ AS ID_TYPE,
                   A.֤������ AS ID_NO,
                   A.�û������� AS CARD_TYPE,
                   A.�û����� AS CARD_NO,
                   B.���ﲡ���� AS HOSP_MEDICAL_NO,
                   ''�Է�'' AS MEDICARE_TYPE,
                   B.ƽ̨������ AS ORDER_NO,
                   B.ҽԺ������ AS HOSP_SEQUENCE,
                   B.����ʱ�� AS ORDER_TIME,
                   (SELECT T.��������
                      FROM ������Ŀ_�������� T
                     WHERE T.�������� = C.��������
                       AND T.���ұ��� = C.�Һſ��ұ���) AS DEPT_NAME,
                   (SELECT T.��Ա����
                      FROM ������Ŀ_��Ա���� T
                     WHERE T.�������� = C.��������
                       AND T.��Ա���� = C.�Һ�ҽ������) AS DOCTOR_NAME,
                   TO_CHAR(C.�Һ�ʱ��, ''yyyy-MM-dd'') AS REG_DATE,
                   B.�ܽ�� AS TOTAL_FEE,
                   B.Ӧ����� AS PAYABLE_FEE,
                   B.ҽ��ͳ��֧����� AS MEDICARE_FEE,
                   ''0'' AS ACTIVITY_FEE,
                   B.ʵ����� AS REAL_FEE,
                   ''��������'' AS PAY_TYPE,
                   B.֧������ AS PAY_CHANNEL_ID,
                   B.֧��ʱ�� AS PAY_TIME,
                   B.�˿�ʱ�� AS REFUND_TIME,
                   DECODE(B.֧������, ''6'', ''2'', ''1'') AS PAY_FLAG,
                   B.�˿��־ AS RETURN_FLAG,
                   DECODE(B.����״̬, ''��֧��'', ''0'', ''��֧��'', ''1'', ''���˿�'', ''2'') AS ORDER_STATUS,
                   '''' AS RECEIPT_ID,
                   '''' AS REMARK
              FROM ������ͨ_�û���Ϣ A, ������ͨ_���� B, �������_�ҺŵǼ� C
             WHERE A.ƽ̨��ʶ = B.ƽ̨��ʶ
               AND A.ҽԺ���� = B.ҽԺ����
               AND B.ҽԺ���� = C.��������
               AND B.���ﲡ���� = C.���ﲡ����
               AND A.����ID = B.����ID
               AND B.����ID = C.����ID
               AND B.�������� = ''����ɷ�''
               AND A.ҽԺ����=''' || STR_�������� || '''
               AND ((''' || STR_��ѯ��� ||
               '''=''2'' AND B.����ʱ�� BETWEEN TO_DATE(''' || STR_�ɷѵ�������ʼ���� ||
               ''',''yyyy-MM-dd'') AND TO_DATE(''' || STR_�ɷѵ������������� ||
               ''',''yyyy-MM-dd'') AND B.����״̬ IN(' || STR_��ѯ״̬���ʹ� ||
               ')) OR (''' || STR_��ѯ��� || '''=''1'' AND B.ҽԺ������ IN (''' ||
               STR_Ժ�ڽɷѵ�ΨһID || ''') AND B.ƽ̨������ IN(''' || STR_ƽ̨�ɷѶ����� ||
               ''')))';
  
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', 'ORDER_LIST');
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
    
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    ELSE
      INT_����ֵ   := 300401;
      STR_������Ϣ := 'δ�鵽�ɷѶ���';
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
               STR_ҽԺ���� => STR_��������,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  RETURN;

END PR_������ͨ_�ɷѶ�����ѯ;
/

prompt
prompt Creating procedure PR_������ͨ_�ɷѼ�¼��ѯ
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_�ɷѼ�¼��ѯ(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2, --3001
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

  STR_SQL VARCHAR2(3500);

  NUM_������� NUMBER(10, 3);
  STR_�������� VARCHAR2(50);

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
  
    ---��������֤��
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
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
    IF STR_�û����� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���û�����';
      GOTO �˳�;
    END IF;
    STR_�û��Ա� := fu_������ͨ_��֤�Ա�(STR_�û��Ա�);
    IF STR_�û��Ա� = '-1' THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���û��Ա�';
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
  
    STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    BEGIN
      SELECT �������
        INTO NUM_�������
        FROM ������ͨ_ƽ̨����
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ��Ч״̬ = '1';
    EXCEPTION
      WHEN OTHERS THEN
        NUM_������� := 100;
    END;
  
    IF STR_�û�Ժ��ID IS NULL THEN
      BEGIN
        SELECT T.����ID
          INTO STR_�û�Ժ��ID
          FROM ������ͨ_�û���Ϣ T
         WHERE T.ƽ̨��ʶ = STR_ƽ̨��ʶ
           AND T.ҽԺ���� = STR_��������
           AND T.���� = STR_�û�����
           AND T.�Ա� = STR_�û��Ա�
           AND (T.֤������ = STR_�û�֤������ OR STR_�û�֤������ IS NULL)
           AND (T.�û����� = STR_�û����� OR STR_�û����� IS NULL)
           AND ROWNUM = 1;
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          INT_����ֵ   := '300102';
          STR_������Ϣ := '�û�������';
          GOTO �˳�;
        WHEN OTHERS THEN
          INT_����ֵ   := 99;
          STR_������Ϣ := 'ƥ���û���Ϣʱ����';
          GOTO �˳�;
      END;   
    END IF;
  
    --�����û���Ϣ
    SELECT T.����, T.֤������, T.֤������, T.�û�������, T.�û�����
      INTO STR_�û�����,
           STR_�û�֤������,
           STR_�û�֤������,
           STR_�û�������,
           STR_�û�����
      FROM ������ͨ_�û���Ϣ T
     WHERE T.ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND T.ҽԺ���� = STR_��������
       AND T.����ID = STR_�û�Ժ��ID;
  
    --ҵ��SQL
    STR_SQL := 'SELECT T.ҽԺ������ AS HOSP_SEQUENCE,
                     CASE
                       WHEN T.�������� = ''ԤԼ�Һ�'' THEN
                        (SELECT A.�Һſ�������
                           FROM �������_ԤԼ�Һ� A
                          WHERE A.�������� = T.ҽԺ����
                            AND A.����ID = T.��������)
                       ELSE
                        (SELECT ��������
                           FROM ������Ŀ_��������
                          WHERE ���ұ��� = (SELECT ������ұ���
                                          FROM �������_�ҺŵǼ�
                                         WHERE �������� = T.ҽԺ����
                                           AND ����ID = T.����ID
                                           AND ���ﲡ���� = T.���ﲡ����))
                     END AS DEPT_NAME,
                     CASE
                       WHEN T.�������� = ''ԤԼ�Һ�'' THEN
                        (SELECT A.�Һ�ҽ������
                           FROM �������_ԤԼ�Һ� A
                          WHERE A.�������� = T.ҽԺ����
                            AND A.����ID = T.��������)
                       ELSE
                        (SELECT ��Ա����
                           FROM ������Ŀ_��Ա����
                          WHERE ��Ա���� = (SELECT ����ҽ������
                                          FROM �������_�ҺŵǼ�
                                         WHERE �������� = T.ҽԺ����
                                           AND ����ID = T.����ID
                                           AND ���ﲡ���� = T.���ﲡ����))
                     END AS DOCTOR_NAME,
                     T.�ܽ��*' || NUM_������� ||
               ' AS PAY_AMOUT,
                     T.֧������ AS PAY_CHANNEL_ID,
                     DECODE(T.����״̬, ''��֧��'', ''0'', ''��֧��'', ''1'', ''���˿�'', ''2'') AS ORDER_STATUS,
                     '''' AS RECEIPT_ID,
                     '''' AS PAY_REMARK,
                     TO_CHAR(T.����ʱ��,''yyyy-MM-dd'') AS RECEIPT_DATE
                FROM ������ͨ_���� T
               WHERE T.ƽ̨��ʶ = ' || STR_ƽ̨��ʶ || '
                 AND T.ҽԺ���� =' || STR_�������� || '
                 AND T.����ID = ''' || STR_�û�Ժ��ID || '''
                 AND T.����ʱ�� BETWEEN TO_DATE(''' || STR_��ʼ���� ||
               ''', ''yyyy-MM-dd'') AND
                     TO_DATE(''' || STR_�������� ||
               ''', ''yyyy-MM-dd'')
                 AND T.����״̬ = DECODE(' || STR_��ѯ״̬���� || ',
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
               STR_ҽԺ���� => STR_��������,
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
                                           STR_ƽ̨��ʶ IN VARCHAR2, --3002
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

  NUM_������� NUMBER(10, 3);
  STR_�������� VARCHAR2(50);

BEGIN
  BEGIN
    STR_ҽԺID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_�û�Ժ��ID   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_PATIENT_ID');
    STR_�ɷѵ�ΨһID := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_SEQUENCE');
  
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
  
    IF STR_�ɷѵ�ΨһID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ɷѵ�ΨһID';
      GOTO �˳�;
    END IF;
  
    STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    BEGIN
      SELECT �������
        INTO NUM_�������
        FROM ������ͨ_ƽ̨����
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ��Ч״̬ = '1';
    EXCEPTION
      WHEN OTHERS THEN
        NUM_������� := 100;
    END;
  
    BEGIN
      SELECT T.����ID,
             T.�ܽ�� * NUM_�������,
             T.Ӧ����� * NUM_�������,
             T.ʵ����� * NUM_�������
        INTO STR_�û�Ժ��ID, STR_�ܽ��, STR_Ӧ�����, STR_ʵ�����
        FROM ������ͨ_���� T
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ҽԺ���� = STR_��������
         AND T.ҽԺ������ = STR_�ɷѵ�ΨһID;
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
         AND T.ҽԺ���� = STR_��������
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
  
    STR_SQL := ' SELECT TT.��Ŀ���� AS DETAIL_TYPE,
                     TT.��Ŀ���� AS DETAIL_NAME,
                     TT.��ˮ�� AS DETAIL_ID,
                     TT.��λ AS DETAIL_UNIT,
                     TT.���� AS DETAIL_COUNT,
                     TT.����*' || NUM_������� ||
               ' AS DETAIL_PRICE,
                     TT.��� AS DETAIL_SPEC,
                     TT.�ܽ��*' || NUM_������� ||
               ' AS DETAIL_AMOUT,
                     ''0'' AS DETAIL_MI
                FROM ������ͨ_���� T, ������ͨ_������ϸ TT
               WHERE T.ҽԺ������ = TT.������
                 AND TT.������ = ''' || STR_�ɷѵ�ΨһID || '''';
  
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
               STR_ҽԺ���� => STR_��������,
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
prompt Creating procedure PR_������ͨ_���Ҳ�ѯ
prompt ===============================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_���Ҳ�ѯ(STR_������� IN VARCHAR2,
                                         STR_ƽ̨��ʶ IN VARCHAR2,
                                         STR_���ܱ��� IN VARCHAR2, --2001
                                         LOB_��Ӧ���� OUT CLOB,
                                         INT_����ֵ   OUT INTEGER,
                                         STR_������Ϣ OUT VARCHAR2) IS

  --�����������
  STR_ҽԺID VARCHAR2(50);
  STR_����ID VARCHAR2(50);

  STR_SQL          VARCHAR2(2000);
  STR_����ԤԼ���� varchar2(1000);

  STR_�������� VARCHAR2(50);

BEGIN
  BEGIN
    --���������������  
    STR_ҽԺID := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_����ID := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DEPT_ID');
  
    --��������֤��
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
  
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫�����ID';
      GOTO �˳�;
    END IF;
  
    STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    BEGIN
      SELECT ����ԤԼ����
        into STR_����ԤԼ����
        FROM ������ͨ_ƽ̨����
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ;
    EXCEPTION
      WHEN OTHERS THEN
        STR_����ԤԼ���� := '';
    END;
  
    --��ҵ����ӦSQL��
    STR_SQL := 'SELECT ���ұ��� AS DEPT_ID,
                                      �������� AS DEPT_NAME,
                                      ''-1'' AS PARENT_ID,
                                      '''' AS SORT_ID,
                                      ��ע AS "DESC",
                                      �������� AS EXPERTISE,
                                      '''' AS "LEVEL",
                                      '''' AS ADDRESS,
                                      DECODE(��Ч״̬, ''��Ч'', ''1'', ''2'') AS STATUS
                                FROM ������Ŀ_��������
                                WHERE ��������=''' || STR_�������� ||
               ''' AND ���ұ��� IN (SELECT T1.���ұ��� FROM �������_����һ���Ű�� T1 WHERE T1.ҽ������ IS NOT NULL
                                  UNION
                                  SELECT T2.���ұ��� FROM �������_�����Ű��¼ T2 WHERE T2.ҽ������ IS NOT NULL AND T2.�Ű�����>SYSDATE-1)
                   AND ���ұ��� not in (SELECT ����ԤԼ����
                                              FROM (SELECT REGEXP_SUBSTR(''' ||
               STR_����ԤԼ���� || ''',
                                                                         ''[^,]+'',
                                                                         1,
                                                                         LEVEL) ����ԤԼ����
                                                      FROM DUAL                                                   
                                                    CONNECT BY REGEXP_SUBSTR(''' ||
               STR_����ԤԼ���� || ''',
                                                                             ''[^,]+'',
                                                                             1,
                                                                             LEVEL) IS NOT NULL)
                                             WHERE ����ԤԼ���� IS NOT NULL)
                 AND (���ұ��� =''' || STR_����ID || ''' or ' ||
               STR_����ID || '=''-1'' or ' || STR_����ID || '=''0'')';
  
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
      STR_������Ϣ := '��Ӧ�������:' || SQLERRM;
      GOTO �˳�;
  END;

  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_��������,
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
                                           STR_ƽ̨��ʶ IN VARCHAR2, --2004
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

  STR_SQL      VARCHAR2(1500);
  STR_�������� VARCHAR2(50);

BEGIN
  BEGIN
  
    --���������������
    STR_ҽԺID   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_����ID   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DEPT_ID');
    STR_ҽ��ID   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DOCTOR_ID');
    STR_�������� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'REG_DATE');
    STR_����ʱ�� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'TIME_FLAG');
  
    -- ������У�顿
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫�����ID';
      GOTO �˳�;
    END IF;
    IF STR_ҽ��ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    IF STR_�������� IS NULL OR FU_����ת����(STR_��������) IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫����Ч�ĳ�������';
      GOTO �˳�;
    END IF;
  
    STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    --����֤�����Ű���Ϣ��
    SELECT COUNT(1)
      INTO INT_����ֵ
      FROM �������_�����Ű��¼
     WHERE �������� = STR_��������
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
     WHERE �������� = STR_��������
       AND ҽ������ = STR_ҽ��ID
       AND �Ű����� = TO_DATE(STR_��������, 'yyyy-MM-dd');
  
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := 200402;
      STR_������Ϣ := 'ҽ��������';
      GOTO �˳�;
    END IF;
  
    STR_SQL := 'SELECT ''4'' AS TIME_FLAG,
                       TT.��ʼʱ�� AS BEGIN_TIME,
                       TT.����ʱ�� AS END_TIME,
                       DECODE(TT.�޺���, ''-1'', ''99'', TT.�޺���) AS TOTAL,
                       DECODE(TT.�޺���,
                              ''-1'',
                              ''99'',
                              TT.�޺��� - nvl(TT.�ѹҺ���,0) -
                              (SELECT COUNT(1)
                                 FROM �������_ԤԼ�Һ�
                                WHERE �հ�α�ʶ = TT.�հ�α�ʶ
                                  AND (ȥ���־ = ''ռ��'' OR ��ʱʱ�� > SYSDATE))) AS OVER_COUNT,
                       TT.�հ�α�ʶ AS REG_ID
                FROM �������_�����Ű��¼ T, �������_���Ű�ʱ�α� TT
                WHERE T.�������� = TT.��������
                      AND T.�Ű���� = TT.�Ű����
                      AND T.��¼ID = TT.��¼ID
                      AND T.��������=' || STR_�������� ||
               ' AND T.���ұ���=' || STR_����ID || ' AND T.ҽ������=' || STR_ҽ��ID ||
               ' AND T.�Ű�����=TO_DATE(''' || STR_�������� || ''',''yyyy-MM-dd'')';
  
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', 'TIME_REG_LIST');
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    ELSE
      INT_����ֵ   := 200403;
      STR_������Ϣ := '�Ű಻����';
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '��Ӧ���󱨴�' || SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_��������,
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

  --STR_SQL  VARCHAR2(2000);
  STR_SQL1 VARCHAR2(2000);

  --�����������
  STR_ҽԺID       VARCHAR2(50);
  STR_����ID       VARCHAR2(50);
  STR_ҽ��ID       VARCHAR2(50);
  STR_�Ű࿪ʼ���� VARCHAR2(50);
  STR_�Ű�������� VARCHAR2(50);

  NUM_������� NUMBER(10, 3);
  STR_�������� VARCHAR2(50);

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
       AND T.ҽ������ IS NOT NULL
       AND (T.ҽ������ = STR_ҽ��ID OR STR_ҽ��ID = '-1')
       AND T.�Ű����� BETWEEN TO_DATE(STR_�Ű࿪ʼ����, 'yyyy-MM-dd') AND
           TO_DATE(STR_�Ű��������, 'yyyy-MM-dd')
     ORDER BY T.���ұ���, T.ҽ������, T.�Ű�����;

  ROW_�Ű���Ϣ CUR_�Ű���Ϣ%ROWTYPE;

BEGIN

  BEGIN
    --�����������
    STR_ҽԺID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_����ID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DEPT_ID');
    STR_ҽ��ID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DOCTOR_ID');
    STR_�Ű࿪ʼ���� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'START_DATE');
    STR_�Ű�������� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'END_DATE');
  
    --��������֤��
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫�����ID';
      GOTO �˳�;
    END IF;
    IF STR_ҽ��ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽ��ID';
      GOTO �˳�;
    END IF;
    IF STR_�Ű࿪ʼ���� IS NULL AND FU_����ת����(STR_�Ű࿪ʼ����) IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���Ű࿪ʼ����';
      GOTO �˳�;
    END IF;
    IF STR_�Ű�������� IS NULL AND FU_����ת����(STR_�Ű��������) IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���Ű��������';
      GOTO �˳�;
    END IF;
    STR_��������:=FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ,STR_ҽԺID,'1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    --����֤�����Ű���Ϣ��
    SELECT COUNT(1)
      INTO INT_����ֵ
      FROM �������_�����Ű��¼
     WHERE �������� = STR_��������
       AND ���ұ��� = STR_����ID
       AND �Ű����� BETWEEN TO_DATE(STR_�Ű࿪ʼ����, 'yyyy-MM-dd') AND
           TO_DATE(STR_�Ű��������, 'yyyy-MM-dd');
  
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := 200301;
      STR_������Ϣ := '���Ҳ�����';
      GOTO �˳�;
    END IF;
  
    --����֤ҽ���Ű���Ϣ��
    IF STR_ҽ��ID <> '-1' THEN
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM �������_�����Ű��¼
       WHERE �������� = STR_��������
         AND ҽ������ = STR_ҽ��ID
         AND �Ű����� BETWEEN TO_DATE(STR_�Ű࿪ʼ����, 'yyyy-MM-dd') AND
             TO_DATE(STR_�Ű��������, 'yyyy-MM-dd');
    
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 200302;
        STR_������Ϣ := 'ҽ��������';
        GOTO �˳�;
      END IF;
    END IF;
  
    BEGIN
      SELECT �������
        INTO NUM_�������
        FROM ������ͨ_ƽ̨����
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ��Ч״̬ = '1';
    EXCEPTION
      WHEN OTHERS THEN
        NUM_������� := 100;
    END;
  
    FOR ROW_�Ű���Ϣ IN CUR_�Ű���Ϣ LOOP
      EXIT WHEN CUR_�Ű���Ϣ%NOTFOUND;
    
      /*STR_SQL := 'SELECT T.�հ�α�ʶ AS REG_ID,
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
        AND T.��������=' || STR_�������� ||
      ' AND T.��¼ID=''' || ROW_�Ű���Ϣ.��¼ID || ''' AND T.�Ű����=' ||
      ROW_�Ű���Ϣ.�Ű����;*/
    
      STR_SQL1 := 'SELECT T1.��¼ID AS REG_ID,
                         ''4'' AS TIME_FLAG,
                         T1.����״̬ AS REG_STATUS,      
                         (SELECT SUM(�޺���)
                            FROM �������_���Ű�ʱ�α�
                           WHERE �������� = T1.��������
                             AND ��¼ID = T1.��¼ID
                             AND �޺��� >= 0) TOTAL,
                         (SELECT SUM(�޺���) - SUM(�ѹҺ���)
                            FROM �������_���Ű�ʱ�α�
                           WHERE �������� = T1.��������
                             AND ��¼ID = T1.��¼ID
                             AND �޺��� >= 0) OVER_COUNT,
                         1 AS REG_LEVEL,
                         T2.�Һŷ� * ' || NUM_������� ||
                  ' AS REG_FEE,
                         T2.���� * ' || NUM_������� ||
                  ' AS TREAT_FEE,
                         1 AS ISTIME
                    FROM �������_�����Ű��¼ T1, ������Ŀ_�Һ����� T2
                   WHERE T1.�������� = T2.��������
                     AND T1.�Һ����ͱ��� = T2.���ͱ���
                   AND T1.��������=' || STR_�������� ||
                  ' AND T1.��¼ID=''' || ROW_�Ű���Ϣ.��¼ID || ''' AND T1.�Ű����=''' ||
                  ROW_�Ű���Ϣ.�Ű���� || '''';
    
      IF STR_��ʱҽ��ID <> ROW_�Ű���Ϣ.ҽ������ THEN
        STR_��ʱҽ��ID   := ROW_�Ű���Ϣ.ҽ������;
        STR_��ʱ�������� := TO_DATE('1990-01-01', 'yyyy-MM-dd');
        IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
          LOB_��Ӧ���� := LOB_��Ӧ���� || '</REG_LIST>'; --�������ڽڵ����
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
        
        END IF;
      ELSE
        IF STR_��ʱ�������� <> ROW_�Ű���Ϣ.�Ű����� THEN
          IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
            LOB_��Ӧ���� := LOB_��Ӧ���� || '</REG_LIST>'; --�������ڽڵ����
          END IF;
          STR_��ʱ�������� := ROW_�Ű���Ϣ.�Ű�����;
          LOB_��Ӧ����     := LOB_��Ӧ���� || '<REG_LIST>'; --�������ڽڵ㿪ʼ
          LOB_��Ӧ����     := LOB_��Ӧ���� || '<REG_DATE>' ||
                          TO_CHAR(ROW_�Ű���Ϣ.�Ű�����, 'yyyy-MM-dd') ||
                          '</REG_DATE>'; --��������
          LOB_��Ӧ����     := LOB_��Ӧ���� || '<REG_WEEKDAY>' || ROW_�Ű���Ϣ.���� ||
                          '</REG_WEEKDAY>'; --�������ڶ�Ӧ����
        
          LOB_��Ӧ���� := LOB_��Ӧ���� ||
                      FU_������ͨ_�õ���Ӧ����(STR_SQL1, 'REG_TIME_LIST', '');
        
        ELSE
        
          LOB_��Ӧ���� := LOB_��Ӧ���� ||
                      FU_������ͨ_�õ���Ӧ����(STR_SQL1, 'REG_TIME_LIST', '');
        END IF;
      END IF;
    
    END LOOP;
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
      LOB_��Ӧ���� := LOB_��Ӧ���� || '</REG_LIST>'; --�������ڽڵ����
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
      STR_������Ϣ := '��Ӧ���󱨴�:' || SQLERRM;
      GOTO �˳�;
  END;
  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_��������,
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
prompt Creating procedure PR_������ͨ_����ҽ����ϸ
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_����ҽ����ϸ(STR_�������� IN VARCHAR2,
                                           STR_�Һ���� IN VARCHAR2,
                                           STR_ҽ����   IN VARCHAR2,
                                           STR_�շ���� OUT VARCHAR2,
                                           INT_����ֵ   OUT INTEGER,
                                           STR_������Ϣ OUT VARCHAR2) IS
  STR_���뷽ʽ       VARCHAR(50);
  INT_С��λ��       NUMBER;
  STR_ҩƷȡ�۷�ʽ   VARCHAR(50);
  STR_ʹ���ѱ��浥�� VARCHAR(50);
  STR_��������       VARCHAR(50);
  I_����             NUMBER;

  CUR_������Ϣ SYS_REFCURSOR;
  CUR_��Ŀ��Ϣ SYS_REFCURSOR;
  ROW_������Ϣ ��ʱ��_����ҽ����ϸ%ROWTYPE;
BEGIN

  -- ��ȡϵͳ����

  STR_�������� := '�������';

  SELECT SYS_GUID() INTO STR_�շ���� FROM DUAL;

  BEGIN
    SELECT ֵ
      INTO STR_���뷽ʽ
      FROM ������Ŀ_���������б�
     WHERE �������� = '257'
       AND �������� = STR_��������;
  EXCEPTION
    WHEN OTHERS THEN
      STR_���뷽ʽ := '1';
  END;

  BEGIN
    SELECT TO_NUMBER(ֵ)
      INTO INT_С��λ��
      FROM ������Ŀ_���������б�
     WHERE �������� = '256'
       AND �������� = STR_��������;
  EXCEPTION
    WHEN OTHERS THEN
      INT_С��λ�� := '2';
  END;

  BEGIN
    SELECT ֵ
      INTO STR_ҩƷȡ�۷�ʽ
      FROM ������Ŀ_���������б�
     WHERE �������� = '117'
       AND �������� = STR_��������;
  EXCEPTION
    WHEN OTHERS THEN
      STR_ҩƷȡ�۷�ʽ := '3';
  END;

  BEGIN
    SELECT ֵ
      INTO STR_ʹ���ѱ��浥��
      FROM ������Ŀ_���������б�
     WHERE �������� = '187'
       AND �������� = STR_��������;
  EXCEPTION
    WHEN OTHERS THEN
      STR_ʹ���ѱ��浥�� := '��';
  END;

  BEGIN
    SELECT TO_NUMBER(ֵ)
      INTO I_����
      FROM ������Ŀ_���������б�
     WHERE �������� = '48'
       AND �������� = STR_��������;
  EXCEPTION
    WHEN OTHERS THEN
      I_���� := 15;
  END;

  BEGIN
  
    DELETE FROM �������_����ҽ����ϸ
     WHERE �������� = STR_��������
       AND �Һ���� = STR_�Һ����
       AND ҽ���� = STR_ҽ����
       AND �շ���� = STR_�շ����;
  
    -- ������շѴ���
    PR_�������_���շѴ���(STR_��������       => STR_��������,
                  STR_�Һ����       => STR_�Һ����,
                  I_����             => I_����,
                  STR_ҩƷȡ�۷�ʽ   => STR_ҩƷȡ�۷�ʽ, --�����,סԺ��,������,���ۼ�
                  STR_ʹ���ѱ��浥�� => STR_ʹ���ѱ��浥��, --��ҽ�Ƿ��޸ĵ���
                  STR_���뷽ʽ       => STR_���뷽ʽ, --��������,��ǰ��λ��
                  INT_С��λ��       => INT_С��λ��, --������λС��
                  STR_����ҽ����     => STR_ҽ����, --��λ
                  STR_��������       => STR_��������, --�������;ҩ������,���ҽ��
                  CUR_������Ϣ       => CUR_������Ϣ,
                  CUR_��Ŀ��Ϣ       => CUR_��Ŀ��Ϣ, --���ڷ��ظôλ��۵�ҽ����Ŀ��Ϣ
                  INT_����ֵ         => INT_����ֵ,
                  STR_������Ϣ       => STR_������Ϣ);
  
    IF INT_����ֵ <> 1 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�޷������Ч�Ĵ��ɷ�����!';
      GOTO �˳�;
    END IF;
  
    -- ����ҽ����ϸ
    LOOP
      FETCH CUR_������Ϣ
        INTO ROW_������Ϣ;
      EXIT WHEN CUR_������Ϣ%NOTFOUND;
      INSERT INTO �������_����ҽ����ϸ
        (��������,
         ��ˮ��,
         ����ID,
         ���ﲡ����,
         �Һ����,
         ҽ����,
         ���,
         �������ұ���,
         ������ұ���,
         ִ�п��ұ���,
         ���˿��ұ���,
         ����ҽ������,
         ����Ա����,
         ����Ա����,
         ����ʱ��,
         ����ʱ��,
         �������,
         С�����,
         ��Ŀ����,
         ��Ŀ����,
         ���,
         ���κ�,
         ����,
         ��������,
         ��Ч��,
         ����,
         ��ҩ׼��,
         ��׼�ĺ�,
         ��λ����,
         ��λ����,
         �������,
         ���ͱ���,
         ��������,
         �������,
         ��������,
         ҩ�����,
         ҩ������,
         ũ�����,
         ҽ�����,
         �Ǿ����,
         ���۱���,
         ����,
         ����,
         ����,
         �ܽ��,
         �������,
         ���,
         ����,
         ��������,
         ��������,
         �÷�����,
         �÷�����,
         Ƶ�ʱ���,
         Ƶ������,
         ҽ������,
         ���ͱ�־,
         �ݴ��־,
         ͨ����,
         ������,
         ��ҩ����,
         ���۷�ʽ,
         ԭ��ˮ��,
         ռ�����޸ı�־,
         ����,
         �����ǼǺ�,
         �ײͱ���,
         ���뵥ID,
         ��λ����,
         С��λ����,
         �Ƽ�ID,
         �շ����,
         ��ҩ����,
         ԭʼҽ����,
         ԭʼ���,
         �Żݽ��,
         ������,
         ���ۼ�,
         �����,
         סԺ��)
      VALUES
        (ROW_������Ϣ.��������,
         ROW_������Ϣ.��ˮ��,
         ROW_������Ϣ.����ID,
         ROW_������Ϣ.���ﲡ����,
         ROW_������Ϣ.�Һ����,
         ROW_������Ϣ.ҽ����,
         ROW_������Ϣ.���,
         ROW_������Ϣ.�������ұ���,
         ROW_������Ϣ.������ұ���,
         ROW_������Ϣ.ִ�п��ұ���,
         ROW_������Ϣ.���˿��ұ���,
         ROW_������Ϣ.����ҽ������,
         ROW_������Ϣ.����Ա����,
         ROW_������Ϣ.����Ա����,
         ROW_������Ϣ.����ʱ��,
         ROW_������Ϣ.����ʱ��,
         ROW_������Ϣ.�������,
         ROW_������Ϣ.С�����,
         ROW_������Ϣ.��Ŀ����,
         ROW_������Ϣ.��Ŀ����,
         ROW_������Ϣ.���,
         ROW_������Ϣ.���κ�,
         ROW_������Ϣ.����,
         ROW_������Ϣ.��������,
         ROW_������Ϣ.��Ч��,
         ROW_������Ϣ.����,
         ROW_������Ϣ.��ҩ׼��,
         ROW_������Ϣ.��׼�ĺ�,
         ROW_������Ϣ.��λ����,
         ROW_������Ϣ.��λ����,
         ROW_������Ϣ.�������,
         ROW_������Ϣ.���ͱ���,
         ROW_������Ϣ.��������,
         ROW_������Ϣ.�������,
         ROW_������Ϣ.��������,
         ROW_������Ϣ.ҩ�����,
         ROW_������Ϣ.ҩ������,
         ROW_������Ϣ.ũ�����,
         ROW_������Ϣ.ҽ�����,
         ROW_������Ϣ.�Ǿ����,
         ROW_������Ϣ.���۱���,
         ROW_������Ϣ.����,
         ROW_������Ϣ.����,
         ROW_������Ϣ.����,
         ROW_������Ϣ.�ܽ��,
         ROW_������Ϣ.�������,
         ROW_������Ϣ.���,
         ROW_������Ϣ.����,
         ROW_������Ϣ.��������,
         ROW_������Ϣ.��������,
         ROW_������Ϣ.�÷�����,
         ROW_������Ϣ.�÷�����,
         ROW_������Ϣ.Ƶ�ʱ���,
         ROW_������Ϣ.Ƶ������,
         ROW_������Ϣ.ҽ������,
         ROW_������Ϣ.���ͱ�־,
         ROW_������Ϣ.�ݴ��־,
         ROW_������Ϣ.ͨ����,
         ROW_������Ϣ.������,
         ROW_������Ϣ.��ҩ����,
         ROW_������Ϣ.���۷�ʽ,
         ROW_������Ϣ.ԭ��ˮ��,
         ROW_������Ϣ.ռ�����޸ı�־,
         ROW_������Ϣ.����,
         ROW_������Ϣ.�����ǼǺ�,
         ROW_������Ϣ.�ײͱ���,
         ROW_������Ϣ.���뵥ID,
         ROW_������Ϣ.��λ����,
         ROW_������Ϣ.С��λ����,
         ROW_������Ϣ.�Ƽ�ID,
         STR_�շ����,
         ROW_������Ϣ.��ҩ����,
         ROW_������Ϣ.ԭʼҽ����,
         ROW_������Ϣ.ԭʼ���,
         ROW_������Ϣ.�Żݽ��,
         ROW_������Ϣ.������,
         ROW_������Ϣ.���ۼ�,
         ROW_������Ϣ.�����,
         ROW_������Ϣ.סԺ��);
    
      IF SQL%ROWCOUNT <= 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '���ɽɷѶ���ʧ��!';
        GOTO �˳�;
      END IF;
    END LOOP;
  
    CLOSE CUR_������Ϣ;
  
    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�';
  
    COMMIT;
    RETURN;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  <<�˳�>>

  IF CUR_������Ϣ%ISOPEN THEN
    CLOSE CUR_������Ϣ;
  END IF;
  STR_�շ���� := '';
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  ROLLBACK;

END PR_������ͨ_����ҽ����ϸ;
/

prompt
prompt Creating procedure PR_������ͨ_����������ɷ��嵥
prompt ====================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_����������ɷ��嵥(STR_��������   IN VARCHAR2,
                                              STR_����ID     IN VARCHAR2,
                                              STR_���ﲡ���� IN VARCHAR2,
                                              INT_����ֵ     OUT INTEGER,
                                              STR_������Ϣ   OUT VARCHAR2) IS

  -- �������
  STR_�Ƿ��������շ� VARCHAR2(50);
  I_����               INTEGER;
  STR_�շ����         VARCHAR2(50);
  STR_ҽ����           VARCHAR2(50);
  STR_�Һ����         VARCHAR2(50);
  NUM_Ӧ�����         NUMBER(18, 3);
  STR_������           VARCHAR2(50);
  DAT_ϵͳʱ��         DATE;
  STR_ƽ̨��ʶ         VARCHAR2(50);

  TYPE REF_CURSOR_TYPE IS REF CURSOR;
  CUR_���ɷ���ϸ REF_CURSOR_TYPE;
  ROW_���ɷ���ϸ �������_����ҽ����ϸ%ROWTYPE;
  STR_SQL        VARCHAR2(1000);
  NUM_�������   NUMBER(10, 3);
BEGIN

  -- �����ݳ�ʼ����
  SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  NUM_Ӧ����� := 0;
  STR_ƽ̨��ʶ := '12320';

  -- ��ȡϵͳ����
  BEGIN
    SELECT TO_NUMBER(ֵ)
      INTO I_����
      FROM ������Ŀ_���������б�
     WHERE �������� = '48'
       AND �������� = STR_��������;
  EXCEPTION
    WHEN OTHERS THEN
      I_���� := 15;
  END;

  BEGIN
    SELECT ֵ
      INTO STR_�Ƿ��������շ�
      FROM ������Ŀ_���������б�
     WHERE �������� = '311'
       AND �������� = STR_��������;
  EXCEPTION
    WHEN OTHERS THEN
      STR_�Ƿ��������շ� := '��';
  END;

  --���������ݡ�
  BEGIN
    SELECT �������
      INTO NUM_�������
      FROM ������ͨ_ƽ̨����
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND ��Ч״̬ = '1';
  EXCEPTION
    WHEN OTHERS THEN
      NUM_������� := 100;
  END;

  BEGIN
    SELECT DISTINCT A.ҽ����, A.�Һ����
      INTO STR_ҽ����, STR_�Һ����
      FROM �������_����ҽ�� A, �������_�ҺŵǼ� C
     WHERE A.�������� = C.��������
       AND A.����ID = C.����ID
       AND A.�Һ���� = C.�Һ����
       AND C.����״̬ = (CASE
             WHEN STR_�Ƿ��������շ� = '��' THEN
              '��ɽ���'
             ELSE
              C.����״̬
           END)
       AND (C.����״̬ <> '��ɽ���' OR EXISTS
            (SELECT 1
               FROM �������_����ҽ�� P
              WHERE P.�������� = A.��������
                AND P.�Һ���� = A.�Һ����
                AND P.����ID = A.����ID
                AND P.�շ�״̬ = '����δ�շ�'))
       AND C.�˺ű�־ = '��'
       AND (A.������� = '1' OR A.������� = '2' OR A.������� = '6')
       AND A.�������� = STR_��������
       AND A.���ﲡ���� = STR_���ﲡ����
       AND A.����ID = STR_����ID
       AND C.�Һ�ʱ�� > TRUNC(SYSDATE) - I_���� + 1
       AND A.¼��ʱ�� > TRUNC(SYSDATE) - I_���� + 1
       AND A.�շ�״̬ = '����δ�շ�'
       AND A.���۷�ʽ <> '�˷��Զ�����'
       AND C.�������ͱ��� = '1';
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'δ�ҵ���Ч�Ĵ��ɷ���Ϣ��';
      GOTO �˳�;
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  -- ����ҽ����ϸ
  BEGIN
  
    PR_������ͨ_����ҽ����ϸ(STR_�������� => STR_��������,
                   STR_�Һ���� => STR_�Һ����,
                   STR_ҽ����   => STR_ҽ����,
                   STR_�շ���� => STR_�շ����,
                   INT_����ֵ   => INT_����ֵ,
                   STR_������Ϣ => STR_������Ϣ);
  
    IF INT_����ֵ = -1 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '���ɴ��ɷ���Ϣʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  BEGIN
  
    BEGIN
      SELECT ҽԺ������
        INTO STR_������
        FROM ������ͨ_����
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ҽԺ���� = STR_��������
         AND ����ID = STR_����ID
         AND ���ﲡ���� = STR_���ﲡ����
         AND ����״̬ = '��֧��'
         AND �������� = '����ɷ�'
         AND ROWNUM = 1;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        STR_������ := '';
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '���Ҷ�����Ϣʧ��!';
        GOTO �˳�;
    END;
  
    IF STR_������ IS NULL THEN
      -- 1)���ɶ�����
      PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '6002',
                  PRM_��������   => STR_��������,
                  PRM_��������   => '1',
                  PRM_����Ψһ�� => STR_������,
                  PRM_ִ�н��   => INT_����ֵ,
                  PRM_������Ϣ   => STR_������Ϣ);
      IF INT_����ֵ <> 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '����������ʧ��!';
        GOTO �˳�;
      END IF;
    ELSE
      --ɾ��δ�ɷѵĶ�����ϸ��¼
      DELETE FROM ������ͨ_������ϸ WHERE ������ = STR_������;
      --ɾ��δ�ɷѵĶ�����¼
      DELETE FROM ������ͨ_���� WHERE ҽԺ������ = STR_������;
    END IF;
  
    STR_SQL := 'SELECT *
      FROM �������_����ҽ����ϸ
     WHERE �������� = ''' || STR_�������� || '''
       AND ����ID = ''' || STR_����ID || '''
       AND ���ﲡ���� = ''' || STR_���ﲡ���� || '''
       AND �շ���� = ''' || STR_�շ���� || '''';
    OPEN CUR_���ɷ���ϸ FOR STR_SQL;
    LOOP
      FETCH CUR_���ɷ���ϸ
        INTO ROW_���ɷ���ϸ;
      EXIT WHEN CUR_���ɷ���ϸ%NOTFOUND;
      INSERT INTO ������ͨ_������ϸ
        (��ˮ��,
         ������,
         Ψһ����,
         �������,
         С�����,
         ��Ŀ����,
         ��Ŀ����,
         ���,
         ���κ�,
         ����,
         ��λ,
         ����,
         �ܽ��,
         �������)
      VALUES
        (SEQ_������ͨ_������ϸ_��ˮ��.NEXTVAL,
         STR_������,
         ROW_���ɷ���ϸ.��ˮ��,
         ROW_���ɷ���ϸ.�������,
         ROW_���ɷ���ϸ.С�����,
         ROW_���ɷ���ϸ.��Ŀ����,
         ROW_���ɷ���ϸ.��Ŀ����,
         ROW_���ɷ���ϸ.���,
         ROW_���ɷ���ϸ.���κ�,
         ROW_���ɷ���ϸ.����,
         ROW_���ɷ���ϸ.��λ����,
         ROW_���ɷ���ϸ.����,
         ROW_���ɷ���ϸ.�ܽ��,
         ROW_���ɷ���ϸ.�������);
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '���涩����ϸʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      NUM_Ӧ����� := NUM_Ӧ����� + ROW_���ɷ���ϸ.�ܽ��;
    
    END LOOP;
  
    INSERT INTO ������ͨ_����
      (��ˮ��,
       ƽ̨��ʶ,
       ҽԺ����,
       ����ID,
       ���ﲡ����,
       ��������,
       ��������,
       ����ʱ��,
       ҽԺ������,
       �ܽ��,
       Ӧ�����,
       ʵ�����,
       ����״̬,
       ������,
       ����ʱ��,
       ������,
       ����ʱ��)
    VALUES
      (SEQ_������ͨ_����_��ˮ��.NEXTVAL,
       STR_ƽ̨��ʶ,
       STR_��������,
       STR_����ID,
       STR_���ﲡ����,
       STR_�շ����,
       '����ɷ�',
       DAT_ϵͳʱ��,
       STR_������,
       NUM_Ӧ�����,
       NUM_Ӧ�����,
       0,
       '��֧��',
       STR_ƽ̨��ʶ,
       DAT_ϵͳʱ��,
       STR_ƽ̨��ʶ,
       DAT_ϵͳʱ��);
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '���涩��ʧ�ܣ�';
      GOTO �˳�;
    END IF;
    CLOSE CUR_���ɷ���ϸ;
  
    INT_����ֵ := '0';
  
    COMMIT;
    RETURN;
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  <<�˳�>>
  IF CUR_���ɷ���ϸ%ISOPEN THEN
    CLOSE CUR_���ɷ���ϸ;
  END IF;

  INT_����ֵ := '-1';

  ROLLBACK;
  RETURN;
END PR_������ͨ_����������ɷ��嵥;
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

  STR_SQL1     VARCHAR2(1000);
  STR_SQL2     VARCHAR2(1000);
  STR_SQL      VARCHAR2(3000);
  INT_�ܼ�¼�� INTEGER;
  STR_�������� VARCHAR2(50);

BEGIN
  BEGIN
    --���������������
    STR_ҽԺID       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_��������     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'BILL_DATE');
    STR_�˵�����     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'BILL_TYPE'); --1���� 2֧��  3�˿�
    STR_ҳ����       := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAGE_CURRENT');
    STR_ÿҳ��¼���� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAGE_SIZE');
  
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    IF STR_�������� IS NULL OR FU_����ת����(STR_��������) IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫����ȷ�ļ�������';
      GOTO �˳�;
    END IF;
    STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    --֧������
    STR_SQL1 := 'SELECT ''1'' AS BUSS_TYPE,
                       T.ҽԺ֧���� AS TRADE_ORDER_NO,
                       T.ƽ̨������ AS ORDER_NO_12320,
                       T.ƽ̨������ˮ�� AS TRANSACTION_ID,
                       T.֧������ AS TRADE_CHANNEL_ID,
                       T.ʵ����� AS TRADE_AMOUNT,
                       TO_CHAR(T.֧��ʱ��,''yyyy-MM-dd'') AS TRADE_TIME,
                       T.�������� AS FEE_TYPE
                  FROM ������ͨ_���� T
                 WHERE T.ƽ̨��ʶ=''' || STR_ƽ̨��ʶ ||
                ''' AND T.ҽԺ����=''' || STR_�������� ||
                ''' AND T.����״̬=''��֧��''  AND TRUNC(T.֧��ʱ��)=TO_DATE(''' ||
                STR_�������� || ''',''yyyy-MM-dd'')';
    --�˿��
    STR_SQL2 := 'SELECT ''2'' AS BUSS_TYPE,
                       T.ҽԺ�˿�� AS TRADE_ORDER_NO,
                       T.ƽ̨������ AS ORDER_NO_12320,
                       T.ƽ̨�˿���ˮ�� AS TRANSACTION_ID,
                       T.֧������ AS TRADE_CHANNEL_ID,
                       T.�˿��� AS TRADE_AMOUNT,
                       TO_CHAR(T.�˿�ʱ��,''yyyy-MM-dd'') AS TRADE_TIME,
                       T.�������� AS FEE_TYPE
                  FROM ������ͨ_���� T
                 WHERE  T.ƽ̨��ʶ=''' || STR_ƽ̨��ʶ ||
                ''' AND T.ҽԺ����=''' || STR_�������� ||
                ''' AND T.����״̬ = ''���˿�'' AND TRUNC(T.�˿�ʱ��)=TO_DATE(''' ||
                STR_�������� || ''',''yyyy-MM-dd'')';
    IF STR_�˵����� = '2' THEN
      STR_SQL := STR_SQL1;
    ELSIF STR_�˵����� = '3' THEN
      STR_SQL := STR_SQL2;
    ELSE
      STR_SQL := STR_SQL1 || ' UNION ALL ' || STR_SQL2;
    END IF;
  
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', 'ORDER_LIST');
  
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
      INT_�ܼ�¼�� := 1;
      --�����ܼ�¼��
      LOOP
        EXIT WHEN XMLTYPE(LOB_��Ӧ����).EXISTSNODE('/RES/ORDER_LIST[' ||
                                               INT_�ܼ�¼�� || ']') = 0;
        INT_�ܼ�¼�� := INT_�ܼ�¼�� + 1;
      END LOOP;
    
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'ORDER_LIST',
                                'HOS_ID',
                                STR_ҽԺID);
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����,
                                'ORDER_LIST',
                                'COUNT',
                                INT_�ܼ�¼�� - 1);
    
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    ELSE
      INT_����ֵ   := 900301;
      STR_������Ϣ := 'δ��ѯ������';
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
               STR_ҽԺ���� => STR_��������,
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
                                         STR_ƽ̨��ʶ IN VARCHAR2, --2002
                                         STR_���ܱ��� IN VARCHAR2,
                                         LOB_��Ӧ���� OUT CLOB,
                                         INT_����ֵ   OUT INTEGER,
                                         STR_������Ϣ OUT VARCHAR2) IS

  --�����������
  STR_ҽԺID VARCHAR2(50);
  STR_����ID VARCHAR2(50);
  STR_ҽ��ID VARCHAR2(50);

  STR_SQL      VARCHAR2(2000);
  NUM_������� NUMBER(10, 3);
  STR_�������� VARCHAR2(50);
BEGIN
  BEGIN
    --���������������  
    STR_ҽԺID := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_����ID := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DEPT_ID'); --  -1��ѯ���п���
    STR_ҽ��ID := FU_������ͨ_�ڵ�ֵ(STR_�������, 'DOCTOR_ID'); --  -1��ѯ����������ҽ��
  
    --������������
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫�����ID';
      GOTO �˳�;
    END IF;
    IF STR_ҽ��ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽ��ID';
      GOTO �˳�;
    END IF;
    STR_��������:=FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ,STR_ҽԺID,'1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    IF STR_����ID <> '-1' THEN
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM (SELECT T1.��������, T1.���ұ���
                FROM �������_����һ���Ű�� T1
               WHERE T1.ҽ������ IS NOT NULL
              UNION
              SELECT T2.��������, T2.���ұ���
                FROM �������_�����Ű��¼ T2
               WHERE T2.ҽ������ IS NOT NULL
                 AND T2.�Ű����� >= trunc(SYSDATE)) T
       WHERE T.�������� = STR_��������
         AND T.���ұ��� = STR_����ID;
    
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 200201;
        STR_������Ϣ := '���Ҳ�����';
        GOTO �˳�;
      END IF;
    END IF;
  
    BEGIN
      SELECT �������
        INTO NUM_�������
        FROM ������ͨ_ƽ̨����
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ��Ч״̬ = '1';
    EXCEPTION
      WHEN OTHERS THEN
        NUM_������� := 100;
    END;
  
    --��ҵ����ӦSQL��
    STR_SQL := 'SELECT A.���ұ��� AS DEPT_ID,
                       A.ҽ������ AS DOCTOR_ID,
                       B.��Ա���� AS "NAME",
                       B.���֤�� AS IDCARD,
                       B.���˼�� AS "DESC",
                       B.ר���س�1 AS SPECIAL,
                       B.ְ�� AS JOB_TITLE,
                       (SELECT ���� * ' || NUM_������� || '
                          FROM ������Ŀ_�Һ�����
                         WHERE �������� = A.��������
                           AND ���ͱ��� = A.�Һ����ͱ���
                           AND ɾ����־ = ''0'') AS REG_FEE,
                       DECODE(B.��Ч״̬, ''��Ч'', ''1'', ''2'') AS STATUS,
                       DECODE(B.�Ա����,''1'',''1'',''2'',''0'',''3'') AS SEX,
                       TO_CHAR(B.��������,''yyyy-MM-dd'') AS BIRTHDAY,
                       B.�ֻ����� AS MOBILE,
                       B.�칫�ҵ绰���� AS TEL

                  FROM (SELECT T1.��������, T1.���ұ���, T1.ҽ������, T1.�Һ����ͱ���
                          FROM �������_����һ���Ű�� T1
                         WHERE T1.ҽ������ IS NOT NULL
                        UNION
                        SELECT T2.��������, T2.���ұ���, T2.ҽ������, T2.�Һ����ͱ���
                          FROM �������_�����Ű��¼ T2
                         WHERE T2.ҽ������ IS NOT NULL
                           AND T2.�Ű����� >= TRUNC(SYSDATE)) A,
                       ������Ŀ_��Ա���� B
                 WHERE A.�������� = B.��������
                   AND A.ҽ������ = B.��Ա����
                   AND  A.��������=''' || STR_�������� ||
               ''' AND (A.���ұ��� =''' || STR_����ID || ''' OR ''-1''=' ||
               STR_����ID || ') 
                   AND (A.ҽ������ =''' || STR_ҽ��ID ||
               ''' OR ''-1''=' || STR_ҽ��ID || ')';
  
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
               STR_ҽԺ���� => STR_��������,
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
                                             STR_ƽ̨��ʶ IN VARCHAR2, --2020
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
  STR_�������� VARCHAR2(50);

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
  
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫�����ID';
      GOTO �˳�;
    END IF;
    IF STR_ҽ��ID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽ��ID';
      GOTO �˳�;
    END IF;
    IF STR_����ͳ������ IS NULL OR FU_����ת����(STR_����ͳ������) IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫����Ч��ͳ������';
      GOTO �˳�;
    END IF;
    STR_��������:=FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ,STR_ҽԺID,'1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    STR_SQL := 'SELECT T.��Ա���� AS DOCTOR_ID,                  
                  (SELECT COUNT(1)
                      FROM �������_ԤԼ�Һ� A
                     WHERE A.�Һſ��ұ��� = T.���ұ���
                       AND A.�Һ�ҽ������ = T.��Ա����
                       AND A.��������=' || STR_�������� || '
                       AND A.֧����־ = ''��''
                       AND A.ȥ���־ = ''ԤԼ''
                       AND A.ԤԼʱ�� = TO_DATE(''' || STR_����ͳ������ ||
               ''', ''yyyy-MM-dd'') + 1) AS NEXT_COUNT,
                  (SELECT COUNT(1)
                      FROM �������_ԤԼ�Һ� A
                     WHERE A.�Һſ��ұ��� = T.���ұ���
                       AND A.�Һ�ҽ������ = T.��Ա����
                       AND A.��������=' || STR_�������� || '
                       AND A.ԤԼʱ�� = TO_DATE(''' || STR_����ͳ������ ||
               ''', ''yyyy-MM-dd'')) AS BOOK_COUNT,
                  (SELECT COUNT(1)
                      FROM �������_�ҺŵǼ� A
                     WHERE A.�Һſ��ұ��� = T.���ұ���
                       AND A.�Һ�ҽ������ = T.��Ա����
                       AND A.��������=' || STR_�������� || '
                       AND A.�Һ�ʱ�� = TO_DATE(''' || STR_����ͳ������ ||
               ''', ''yyyy-MM-dd'')) AS REG_COUNT,
                  (SELECT COUNT(1)
                      FROM �������_ԤԼ�Һ� A
                     WHERE A.�Һſ��ұ��� = T.���ұ���
                       AND A.�Һ�ҽ������ = T.��Ա����
                       AND A.��������=' || STR_�������� || '
                       AND A.֧����־ = ''��''
                       AND A.ȥ���־ = ''����''
                       AND A.ԤԼʱ�� = TO_DATE(''' || STR_����ͳ������ ||
               ''', ''yyyy-MM-dd'')) AS RECEIVE_BOOK,
                   (SELECT COUNT(1)
                      FROM �������_�ҺŵǼ� A
                     WHERE A.������ұ��� = T.���ұ���
                       AND A.����ҽ������ = T.��Ա����
                       AND A.��������=' || STR_�������� || '
                       AND A.����״̬ IN (''���ڽ���'', ''��ɽ���'')
                       AND A.�Һ�ʱ�� = TO_DATE(''' || STR_����ͳ������ ||
               ''', ''yyyy-MM-dd'')) AS RECEIVE_REG
              FROM ������Ŀ_��Ա�����б� T
             WHERE  T.��������=''' || STR_�������� ||
               ''' AND T.ɾ����־ = ''0''
             AND T.���ұ���=''' || STR_����ID || ''' AND (T.��Ա����=
             ''' || STR_ҽ��ID || ''' OR ''-1''=''' || STR_ҽ��ID ||
               ''')';
  
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', 'DOCTOR_INFO');
    IF DBMS_LOB.GETLENGTH(LOB_��Ӧ����) > 0 THEN
    
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM ������Ŀ_��Ա�����б� T
       WHERE T.�������� = STR_��������
         AND T.ɾ����־ = '0'
         AND T.���ұ��� = STR_����ID
         AND (T.��Ա���� = STR_ҽ��ID OR STR_ҽ��ID = '-1');
    
      LOB_��Ӧ���� := FU_������ͨ_����½ڵ�(LOB_��Ӧ����,
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
      INT_����ֵ   := 99;
      STR_������Ϣ := '��Ӧ���󱨴�:' || SQLERRM;
      GOTO �˳�;
  END;

  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_��������,
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
                                           STR_ƽ̨��ʶ IN VARCHAR2, --1004
                                           STR_���ܱ��� IN VARCHAR2,
                                           LOB_��Ӧ���� OUT CLOB,
                                           INT_����ֵ   OUT INTEGER,
                                           STR_������Ϣ OUT VARCHAR2) IS

  STR_SQL          VARCHAR2(1000);
  STR_ҽԺID       VARCHAR2(50);
  INT_���ԤԼ���� INTEGER;
  STR_��������     VARCHAR2(50);

BEGIN
  BEGIN
    dbms_output.put_line(STR_�������);
    STR_ҽԺID := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
  
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    --����ȡ���ԤԼ������
    BEGIN
      SELECT ֵ
        INTO INT_���ԤԼ����
        FROM ������Ŀ_���������б�
       WHERE �������룽 '910540'
         AND �������� = STR_��������
         AND ɾ����־ = '0';
    EXCEPTION
      WHEN OTHERS THEN
        INT_���ԤԼ���� := 15;
    END;
  
    --��ҵ����ӦSQL��
    STR_SQL := 'SELECT ''' || STR_ҽԺID || ''' AS HOS_ID,
                      �������� AS "NAME",
                      ''Ӫ�ڶ�Ժ'' AS SHORT_NAME, 
                      ������ַ AS ADDRESS,
                      ��ϵ�绰 AS TEL,
                      '''' AS WEBSITE,
                      '''' AS WEIBO,
                      ''2'' AS "LEVEL",
                      '''' AS "DESC",
                      '''' AS SPECIAL,
                      '''' AS LONGITUDE,
                      '''' AS LATITUDE,
                      ' || INT_���ԤԼ���� ||
               ' AS MAX_REG_DAYS,
                      '''' AS START_REG_TIME,
                      '''' AS END_REG_TIME,
                      '''' AS STOP_BOOK_TIMEA,
                      '''' AS STOP_BOOK_TIMEP                      
               FROM ������Ŀ_�������� WHERE ��������=' || STR_��������;
  
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
               STR_ҽԺ���� => STR_��������,
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
prompt Creating procedure PR_������ͨ_�û���Ϣ��ѯ
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_�û���Ϣ��ѯ(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2,
                                           STR_���ܱ��� IN VARCHAR2, --1003
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
  STR_��������     VARCHAR2(50);

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
  
    --����Ϣ��֤��
    --ҽԺID
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    --����
    IF STR_�û����� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫������';
      GOTO �˳�;
    END IF;
    --�Ա�
    STR_�û��Ա� := FU_������ͨ_��֤�Ա�(STR_�û��Ա�);
    IF STR_�û��Ա� = '-1' THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���Ա�';
      GOTO �˳�;
    END IF;
    --��������
    IF STR_�û��������� IS NULL OR FU_����ת����(STR_�û���������) IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫����Ч�ĳ�������';
      GOTO �˳�;
    END IF;
    --���֤
    IF STR_�û�֤������ IS NULL AND STR_�໤��֤������ IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���û���໤��֤����Ϣ';
      GOTO �˳�;
    END IF;
    STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    IF STR_�û�ID IS NULL THEN
      BEGIN
        SELECT T.����ID
          INTO STR_�û�ID
          FROM ������ͨ_�û���Ϣ T
         WHERE T.ƽ̨��ʶ = STR_ƽ̨��ʶ
           AND T.ҽԺ���� = STR_��������
           AND T.���� = STR_�û�����
           AND T.�Ա� = STR_�û��Ա�
           AND T.�������� = TO_DATE(STR_�û���������, 'yyyy-MM-dd')
           AND (T.֤������ = STR_�û�֤������ OR STR_�û�֤������ IS NULL)
           AND (T.�໤��֤������ = STR_�໤��֤������ OR STR_�໤��֤������ IS NULL)
           AND ROWNUM = 1;
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          INT_����ֵ   := '100301';
          STR_������Ϣ := '�û�δ��ҽԺע��';
          GOTO �˳�;
        WHEN OTHERS THEN
          INT_����ֵ   := 99;
          STR_������Ϣ := 'ƥ���û���Ϣʱ����';
          GOTO �˳�;
      END;
    
    END IF;
  
    BEGIN
      SELECT TO_CHAR(B.����ʱ��, 'yyyy-MM-dd hh24:mi:ss')
        INTO STR_�û�ע��ʱ��
        FROM ������ͨ_�û���Ϣ B
       WHERE B.ҽԺ���� = STR_��������
         AND B.ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND B.����ID = STR_�û�ID
         AND ROWNUM = 1;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 100301;
        STR_������Ϣ := '�û�δ��ҽԺע��';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := 'ƥ���û���Ϣʱ����';
        GOTO �˳�;
    END;
  
    STR_SQL := 'SELECT ����ID AS HOSP_PATIENT_ID,
                  '''' AS HOSP_MEDICAL_NUM,
                  ''99'' AS CARD_TYPE,
                  ����ID AS CARD_NO,
                  ''0'' AS CARD_STATUS,
                  '''' AS CARD_TIME,
                  '''' AS LAST_TIME,
                  �ֻ����� AS MOBILE,
                  ��ϵ��ַ AS ADDRESS                 
   FROM ������ͨ_�û���Ϣ WHERE ҽԺ����=''' || STR_�������� ||
               ''' AND ƽ̨��ʶ=''' || STR_ƽ̨��ʶ || ''' AND ����ID =''' ||
               STR_�û�ID || '''';
  
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
               STR_ҽԺ���� => STR_��������,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
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
  STR_����     VARCHAR2(50);
  DAT_�������� DATE;
  STR_�������� varchar2(50);
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
    --ҽԺID
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    --����
    IF STR_���� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫������';
      GOTO �˳�;
    END IF;
    --�Ա�
    STR_�Ա� := FU_������ͨ_��֤�Ա�(STR_�Ա�);
    IF STR_�Ա� = '-1' THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���Ա�';
      GOTO �˳�;
    END IF;
    --�ֻ�����
    IF STR_�ֻ����� IS NULL OR FU_������ͨ_��֤�ֻ���(STR_�ֻ�����) <> 0 THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫����Ч���ֻ�����';
      GOTO �˳�;
    END IF;
    --��������
    IF STR_�������� IS NULL OR FU_����ת����(STR_��������) IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫����Ч�ĳ�������';
      GOTO �˳�;
    END IF;
    --֤����Ϣ
    IF (STR_֤������ IS NULL OR STR_֤������ IS NULL) AND
       (STR_�໤��֤������ IS NULL OR STR_�໤��֤������ IS NULL OR STR_�໤������ IS NULL) THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���û�֤����Ϣ��໤��֤����Ϣ';
      GOTO �˳�;
    END IF;
    IF STR_֤������ = '1' AND STR_֤������ IS NOT NULL AND
       FU_������ͨ_��֤���֤(STR_֤������) <> 0 THEN
      STR_������Ϣ := '��Ч���û�֤������';
      INT_����ֵ   := 1;
      GOTO �˳�;
    END IF;
    IF STR_�໤��֤������ = '1' AND STR_�໤��֤������ IS NOT NULL AND
       FU_������ͨ_��֤���֤(STR_�໤��֤������) <> 0 THEN
      STR_������Ϣ := '��Ч�ļ໤��֤������';
      INT_����ֵ   := 1;
      GOTO �˳�;
    END IF;
    --��֤���֤����Ч��
    IF STR_֤������ = '1' AND STR_֤������ IS NOT NULL THEN
      INT_����ֵ := FU_������ͨ_�⹹���֤(STR_���֤�� => STR_֤������,
                               DAT_�������� => DAT_��������,
                               STR_����     => STR_����,
                               STR_�Ա�     => STR_�Ա�,
                               STR_������Ϣ => STR_������Ϣ);
      IF INT_����ֵ <> 0 THEN
        GOTO �˳�;
      END IF;
    ELSE
      DAT_�������� := TO_DATE(STR_��������, 'yyyy-mm-dd');
      STR_����     := FU_�õ�_����(DAT_��������);
    END IF;
  
    STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    --����֤�Ƿ���ڸ��û���
    SELECT COUNT(1)
      INTO INT_����ֵ
      FROM ������ͨ_�û���Ϣ B
     WHERE B.ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND B.ҽԺ���� = STR_��������
       AND B.���� = STR_����
       AND NVL(B.֤������, '��ֵ') = (CASE
                     WHEN STR_֤������ IS NULL THEN                   
                      '��ֵ'
                     ELSE
                      STR_֤������
                   END)
       AND B.�໤��֤������ = STR_�໤��֤������;
  
    IF INT_����ֵ > 0 THEN
      INT_����ֵ   := 100202;
      STR_������Ϣ := 'Ժ�ڶ���û�������������ϵҽԺ����';
      GOTO �˳�;
    END IF;
  
    SELECT COUNT(1)
      INTO INT_����ֵ
      FROM ������Ŀ_������Ϣ B
      LEFT JOIN ������Ŀ_������Ϣ_���� C
        ON B.�������� = C.��������
       AND B.����ID = C.����ID
     WHERE B.�������� = STR_��������
       AND B.���� = STR_����
       AND NVL(B.���֤��, '��ֵ') = (CASE
                     WHEN STR_֤������ IS NULL THEN                   
                      '��ֵ'
                     ELSE
                      STR_֤������
                   END)
       AND C.�໤�����֤�� = STR_�໤��֤������;
  
    IF INT_����ֵ = 0 THEN
      -- �����ɲ���ID��
      PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '30',
                  PRM_��������   => STR_��������,
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
         ����,
         ����,
         �Ա�,
         ��������,
         ����,
         ��ͥ��ַ,
         �ֻ�����,
         �Ǽ�ʱ��,
         ƴ����,
         �����,
         ���֤��,
         ¼���˱���,
         ��������)
      VALUES
        (STR_��������,
         STR_����ID,
         STR_����,
         STR_����,
         STR_�Ա�,
         DAT_��������,
         STR_����,
         STR_��ַ,
         STR_�ֻ�����,
         SYSDATE,
         FU_ͨ��_����_ת��_��ƴ(STR_����),
         FU_ͨ��_����_ת��_���(STR_����),
         STR_֤������,
         STR_ƽ̨��ʶ,
         DECODE(STR_������, '1', STR_����, NULL));
    
      -- �����벡�˸�����Ϣ��
      INSERT INTO ������Ŀ_������Ϣ_����
        (��������,
         ����ID,
         �໤������,
         �໤�����֤��,
         �໤���ֻ�����,
         �໤����ϵ��ַ,
         ������Դ)
      VALUES
        (STR_��������,
         STR_����ID,
         STR_�໤������,
         STR_�໤��֤������,
         NULL,
         NULL,
         '1');
    
    ELSE
      SELECT B.����ID
        INTO STR_����ID
        FROM ������Ŀ_������Ϣ B
        LEFT JOIN ������Ŀ_������Ϣ_���� C
          ON B.�������� = C.��������
         AND B.����ID = C.����ID
       WHERE B.�������� = STR_��������
         AND B.���� = STR_����
         AND NVL(B.���֤��, '��ֵ') = (CASE
                     WHEN STR_֤������ IS NULL THEN                   
                      '��ֵ'
                     ELSE
                      STR_֤������
                   END)
         AND C.�໤�����֤�� = STR_�໤��֤������
         AND ROWNUM = 1;
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
       STR_��������,
       STR_����ID,
       NULL,
       STR_����,
       STR_�Ա�,
       DAT_��������,
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
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '��Ӧ���󱨴�:' || SQLERRM;
      GOTO �˳�;
  END;

  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_��������,
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
prompt Creating procedure PR_������ͨ_ԤԼ�ҺŵǼ�
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_ԤԼ�ҺŵǼ�(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2, --2007
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

  NUM_�Һŷ�   NUMBER;
  NUM_����   NUMBER;
  STR_������� VARCHAR2(50);
  INT_�޺���   INTEGER;
  INT_�ѹҺ��� INTEGER;

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

  STR_��ʱ�û�����       VARCHAR2(50);
  STR_��ʱ�û��Ա�       VARCHAR2(50);
  STR_��ʱ�û����֤��   VARCHAR2(50);
  STR_��ʱ�໤�����֤�� VARCHAR2(50);
  DAT_���߳�������       DATE;
  STR_����               VARCHAR2(50);
  STR_����״��           VARCHAR2(50);
  STR_������λ           VARCHAR2(50);

  STR_ȡ��ʱ��� VARCHAR2(50);
  NUM_�������   NUMBER(10, 3);

  INT_�û�ע������ INTEGER; --1ֻע��HIS  1ע��HIS+ƽ̨
  STR_��������     VARCHAR2(50);

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
  
    -- ����ȡDAT_ϵͳʱ�䡿
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    --��������֤��
    IF STR_ƽ̨������ IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ƽ̨������';
      GOTO �˳�;
    END IF;
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    IF STR_��α�ʶ IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���Ű�ID';
      GOTO �˳�;
    END IF;
    STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    BEGIN
      SELECT A.�޺���, A.�ѹҺ���
        INTO INT_�޺���, INT_�ѹҺ���
        FROM �������_���Ű�ʱ�α� A, �������_�����Ű��¼ B
       WHERE A.�������� = B.��������
         AND A.��¼ID = B.��¼ID
         AND A.�������� = STR_��������
         AND B.�Ű����� = TO_DATE(STR_��������, 'yyyy-MM-dd')
         AND A.�հ�α�ʶ = STR_��α�ʶ;
      IF INT_�޺��� >= 0 THEN
        IF INT_�ѹҺ��� >= INT_�޺��� THEN
          INT_����ֵ   := 200709;
          STR_������Ϣ := '���Ű�Һ���������ʣ���Դ��';
          GOTO �˳�;
        ELSE
          SELECT COUNT(1)
            INTO INT_����ֵ
            FROM �������_ԤԼ�Һ�
           WHERE �������� = STR_��������
             AND �հ�α�ʶ = STR_��α�ʶ
             AND ȥ���־ = 'ռ��'
             AND ��ʱʱ�� > SYSDATE;
          IF INT_����ֵ >= INT_�޺��� - INT_�ѹҺ��� THEN
            INT_����ֵ   := 200710;
            STR_������Ϣ := '���Ű��µĵ�ǰ��Դ�ѱ�ռ��';
            GOTO �˳�;
          END IF;
        END IF;
      END IF;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 200705;
        STR_������Ϣ := '��Ч���Ű�';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '��֤�Ű౨��';
        GOTO �˳�;
    END;
  
    --����֤�����Ű���Ϣ��
    IF STR_����ID IS NOT NULL THEN
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM �������_�����Ű��¼ T, �������_���Ű�ʱ�α� TT
       WHERE T.�������� = TT.��������
         AND T.��¼ID = TT.��¼ID
         AND T.�������� = STR_��������
         AND T.���ұ��� = STR_����ID
         AND TT.�հ�α�ʶ = STR_��α�ʶ
         AND T.�Ű����� = TO_DATE(STR_��������, 'yyyy-MM-dd');
    
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 200705;
        STR_������Ϣ := '��Ч���Ű�';
        GOTO �˳�;
      END IF;
    END IF;
  
    --����֤ҽ���Ű���Ϣ��
    IF STR_ҽ��ID IS NOT NULL THEN
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM �������_�����Ű��¼ T, �������_���Ű�ʱ�α� TT
       WHERE T.�������� = TT.��������
         AND T.��¼ID = TT.��¼ID
         AND T.�������� = STR_��������
         AND T.ҽ������ = STR_ҽ��ID
         AND T.����״̬ = '1'
         AND TT.�հ�α�ʶ = STR_��α�ʶ
         AND T.�Ű����� = TO_DATE(STR_��������, 'yyyy-MM-dd');
    
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 200705;
        STR_������Ϣ := '��Ч���Ű�';
        GOTO �˳�;
      END IF;
    END IF;
  
    --����֤ƽ̨�����š�
  
    --���ŹҺ�
    IF STR_�Ƿ�ΪԤԼ�Һ� = '3' THEN
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM ������ͨ_����
       WHERE ҽԺ���� = STR_��������
         AND ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ƽ̨������ = STR_ƽ̨������
         AND ����״̬ = '������';
    
      IF INT_����ֵ <= 0 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := 'ƽ̨��������Ч';
        GOTO �˳�;
      END IF;
    
      SELECT ��������
        INTO STR_ԤԼ����
        FROM ������ͨ_����
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ҽԺ���� = STR_��������
         AND ƽ̨������ = STR_ƽ̨������
         AND ����״̬ = '������';
    
      -- ��֤ԤԼ��
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM �������_ԤԼ�Һ�
       WHERE �������� = STR_��������
         AND ����ID = STR_ԤԼ����
         AND ȥ���־ = 'ռ��'
         AND ��ʱʱ�� < SYSDATE;
    
      IF INT_����ֵ > 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '������Դ��ʧЧ';
        GOTO �˳�;
      END IF;
    
    ELSE
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM ������ͨ_����
       WHERE ҽԺ���� = STR_��������
         AND ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ƽ̨������ = STR_ƽ̨������;
    
      IF INT_����ֵ > 0 THEN
        INT_����ֵ   := 200708;
        STR_������Ϣ := 'ƽ̨�������Ѵ���';
        GOTO �˳�;
      END IF;
    END IF;
  
    BEGIN
      SELECT �������
        INTO NUM_�������
        FROM ������ͨ_ƽ̨����
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ��Ч״̬ = '1';
    EXCEPTION
      WHEN OTHERS THEN
        NUM_������� := 100;
    END;
  
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
         AND D.�������� = STR_��������
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
  
    IF SYSDATE >
       TO_DATE(TO_CHAR(DAT_�Ű�����, 'yyyy-mm-dd') || ' ' || STR_ԤԼʱ�ν���,
               'yyyy-mm-dd hh24:mi:ss') THEN
      INT_����ֵ   := 200705;
      STR_������Ϣ := '��Ч���Ű�';
      GOTO �˳�;
    END IF;
  
    --����ȡ�Һ�������Ϣ��
    BEGIN
      SELECT ���ͱ���, ��������, �Һŷ�, ����, �������
        INTO STR_�Һ����ͱ���,
             STR_�Һ���������,
             NUM_�Һŷ�,
             NUM_����,
             STR_�������
        FROM ������Ŀ_�Һ�����
       WHERE �������� = STR_��������
         AND ���ͱ��� = STR_�Һ����ͱ���
         AND ��Ч״̬ = '��Ч'
         AND ɾ����־ = '0';
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 1;
        STR_������Ϣ := 'δ�ҵ���Ч�ĹҺ�����';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
  
    IF STR_�Һŷ��� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��Һŷ���';
      GOTO �˳�;
    END IF;
    IF STR_���Ʒ��� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫�����Ʒ���';
      GOTO �˳�;
    END IF;
  
    IF TO_NUMBER(STR_�Һŷ���) <> NUM_�Һŷ� * NUM_������� OR
       TO_NUMBER(STR_���Ʒ���) <> NUM_���� * NUM_������� THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�ҷ�������Ű�������ò���';
      GOTO �˳�;
    END IF;
  
    --��ע����ȡ������Ϣ��ʼ��
    BEGIN
      --����Ϣ��֤��
      --����
      IF STR_�������� IS NULL THEN
        INT_����ֵ   := 1;
        STR_������Ϣ := '�봫�뻼������';
        GOTO �˳�;
      END IF;
      --�Ա�
      STR_�����Ա� := FU_������ͨ_��֤�Ա�(STR_�����Ա�);
      IF STR_�����Ա� = '-1' THEN
        INT_����ֵ   := 1;
        STR_������Ϣ := '�봫�뻼���Ա�';
        GOTO �˳�;
      END IF;
    
      --��������
      IF STR_���߳������� IS NULL OR FU_����ת����(STR_���߳�������) IS NULL THEN
        INT_����ֵ   := 1;
        STR_������Ϣ := '�봫����Ч�ĳ�������';
        GOTO �˳�;
      END IF;
      --֤����Ϣ
      IF STR_�Һ����� = '2' THEN
        --��Ů
        IF STR_�Һ������֤���� IS NULL OR FU_������ͨ_��֤���֤(STR_�Һ������֤����) <> 0 THEN
          INT_����ֵ   := 1;
          STR_������Ϣ := '�봫����ȷ�ĹҺ���֤����Ϣ';
          GOTO �˳�;
        END IF;
        IF STR_����֤������ IS NOT NULL AND FU_������ͨ_��֤���֤(STR_����֤������) <> 0 THEN
          INT_����ֵ   := 1;
          STR_������Ϣ := '�봫����ȷ�Ļ���֤����Ϣ';
          GOTO �˳�;
        END IF;
      
      ELSE
        --���˻�����
        IF STR_����֤������ IS NULL OR FU_������ͨ_��֤���֤(STR_����֤������) <> 0 THEN
          INT_����ֵ   := 1;
          STR_������Ϣ := '�봫����ȷ�Ļ���֤����Ϣ';
          GOTO �˳�;
        END IF;
        IF STR_�Һ������֤���� IS NOT NULL AND FU_������ͨ_��֤���֤(STR_�Һ������֤����) <> 0 THEN
          INT_����ֵ   := 1;
          STR_������Ϣ := '�봫����ȷ�ĹҺ���֤����Ϣ';
          GOTO �˳�;
        END IF;
      END IF;
    
      --�ֻ�����
      IF STR_�Һ����ֻ����� IS NULL OR FU_������ͨ_��֤�ֻ���(STR_�Һ����ֻ�����) <> 0 THEN
        INT_����ֵ   := 1;
        STR_������Ϣ := '�봫����Ч�ĹҺ����ֻ�����';
        GOTO �˳�;
      END IF;
      --�Һ�������
      IF STR_�Һ������� IS NULL THEN
        INT_����ֵ   := 1;
        STR_������Ϣ := '�봫��Һ�������';
        GOTO �˳�;
      END IF;
      --�µ�ʱ��
      IF STR_�µ�ʱ�� IS NULL THEN
        INT_����ֵ   := 1;
        STR_������Ϣ := '�봫���µ�ʱ��';
        GOTO �˳�;
      END IF;
    
      --���û�ID��Ϊ��ʱ��
      IF STR_����ID IS NOT NULL THEN
        BEGIN
          --��֤ƽ̨�û����Ƿ���ڸò���ID
          SELECT ����, �Ա�, ��������, ֤������, �໤��֤������
            INTO STR_��ʱ�û�����,
                 STR_��ʱ�û��Ա�,
                 DAT_���߳�������,
                 STR_��ʱ�û����֤��,
                 STR_��ʱ�໤�����֤��
            FROM ������ͨ_�û���Ϣ
           WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
             AND ҽԺ���� = STR_��������
             AND ����ID = STR_����ID
             AND ROWNUM = 1;
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            INT_����ֵ   := 200711;
            STR_������Ϣ := '�����������Ϣ��ҽԺ������ƥ��';
            GOTO �˳�;
          WHEN OTHERS THEN
            INT_����ֵ   := 99;
            STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
            GOTO �˳�;
        END;
      
        --ƽ̨����ڸò���ID����֤��Ϣ�Ƿ�ƥ��
        IF STR_�������� <> STR_��ʱ�û����� OR STR_�����Ա� <> STR_��ʱ�û��Ա� OR
           DAT_���߳������� <> TO_DATE(STR_���߳�������, 'yyyy-MM-dd') OR
           (STR_��ʱ�໤�����֤�� <> STR_�Һ������֤���� AND STR_�Һ����� = '2') OR
           (STR_��ʱ�û����֤�� <> STR_����֤������ AND STR_�Һ����� <> '2') THEN
          INT_����ֵ   := 200711;
          STR_������Ϣ := '�����������Ϣ��ҽԺ������ƥ��';
          GOTO �˳�;
        END IF;
      
        BEGIN
          --��֤HIS�û����Ƿ���ڸò���ID
          SELECT B.����, B.�Ա�, B.��������, B.���֤��, C.�໤�����֤��
            INTO STR_��ʱ�û�����,
                 STR_��ʱ�û��Ա�,
                 DAT_���߳�������,
                 STR_��ʱ�û����֤��,
                 STR_��ʱ�໤�����֤��
            FROM ������Ŀ_������Ϣ B
            LEFT JOIN ������Ŀ_������Ϣ_���� C
              ON B.�������� = C.��������
             AND B.����ID = C.����ID
           WHERE B.�������� = STR_��������
             AND B.����ID = STR_����ID
             AND ROWNUM = 1;
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
            INT_����ֵ   := 200711;
            STR_������Ϣ := '�����������Ϣ��ҽԺ������ƥ��';
            GOTO �˳�;
          WHEN OTHERS THEN
            INT_����ֵ   := 99;
            STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
            GOTO �˳�;
        END;
      
        --HIS����ڸò���ID����֤��Ϣ�Ƿ�ƥ��
        IF STR_�������� <> STR_��ʱ�û����� OR STR_�����Ա� <> STR_��ʱ�û��Ա� OR
           DAT_���߳������� <> TO_DATE(STR_���߳�������, 'yyyy-MM-dd') OR
           (STR_��ʱ�໤�����֤�� <> STR_�Һ������֤���� AND STR_�Һ����� = '2') OR
           (STR_��ʱ�û����֤�� <> STR_����֤������ AND STR_�Һ����� <> '2') THEN
          INT_����ֵ   := 200711;
          STR_������Ϣ := '�����������Ϣ��ҽԺ������ƥ��';
          GOTO �˳�;
        END IF;
      
      ELSE
        --�����û�IDΪ��ʱ��
        --��Ů
        IF STR_�Һ����� = '2' THEN
          --����֤�Ƿ���ڸ��û���
          BEGIN
            SELECT B.����ID
              INTO STR_����ID
              FROM ������ͨ_�û���Ϣ B
             WHERE B.ƽ̨��ʶ = STR_ƽ̨��ʶ
               AND B.ҽԺ���� = STR_��������
               AND B.���� = STR_��������
               AND NVL(B.֤������, '��ֵ') = (CASE
                     WHEN STR_����֤������ IS NULL THEN
                      '��ֵ'
                     ELSE
                      STR_����֤������
                   END)
               AND B.�໤��֤������ = STR_�Һ������֤����
               AND ROWNUM = 1;
          EXCEPTION
            WHEN NO_DATA_FOUND THEN
              STR_����ID := NULL;
            WHEN OTHERS THEN
              INT_����ֵ   := 99;
              STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
              GOTO �˳�;
          END;
        ELSE
          --����֤�Ƿ���ڸ��û���
          BEGIN
            SELECT B.����ID
              INTO STR_����ID
              FROM ������ͨ_�û���Ϣ B
             WHERE B.ƽ̨��ʶ = STR_ƽ̨��ʶ
               AND B.ҽԺ���� = STR_��������
               AND B.���� = STR_��������
               AND B.֤������ = STR_����֤������
               AND ROWNUM = 1;
          EXCEPTION
            WHEN NO_DATA_FOUND THEN
              STR_����ID := NULL;
            WHEN OTHERS THEN
              INT_����ֵ   := 99;
              STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
              GOTO �˳�;
          END;
        END IF;
      
        IF STR_����ID IS NULL THEN
          --ƽ̨���в����ڸ��û� ��ע��
          INT_�û�ע������ := 2;
        ELSE
          --ƽ̨���д��ڸ��û� �ж�ҽԺ�����Ƿ����  
          INT_�û�ע������ := 1;
        END IF;
      
        --��֤ҽԺ�����Ƿ���ڸ��û�
        IF STR_�Һ����� = '2' THEN
          -- ��Ů   
          IF STR_����֤������ IS NULL THEN
            DAT_���߳������� := TO_DATE(STR_���߳�������, 'yyyy-MM-dd');
            STR_����         := FU_�õ�_����(DAT_���߳�������);
          ELSE
            INT_����ֵ := FU_������ͨ_�⹹���֤(STR_���֤�� => STR_����֤������,
                                     DAT_�������� => DAT_���߳�������,
                                     STR_����     => STR_����,
                                     STR_�Ա�     => STR_�����Ա�,
                                     STR_������Ϣ => STR_������Ϣ);
            IF INT_����ֵ <> 0 THEN
              GOTO �˳�;
            END IF;
          END IF;
        
          SELECT COUNT(1)
            INTO INT_����ֵ
            FROM ������Ŀ_������Ϣ B
            LEFT JOIN ������Ŀ_������Ϣ_���� C
              ON B.�������� = C.��������
             AND B.����ID = C.����ID
           WHERE B.�������� = STR_��������
             AND B.���� = STR_��������
             AND NVL(B.���֤��, '��ֵ') = (CASE
                   WHEN STR_����֤������ IS NULL THEN
                    '��ֵ'
                   ELSE
                    STR_����֤������
                 END)
             AND C.�໤�����֤�� = STR_�Һ������֤����;
        ELSE
          -- ���˻�����
          INT_����ֵ := FU_������ͨ_�⹹���֤(STR_���֤�� => STR_����֤������,
                                   DAT_�������� => DAT_���߳�������,
                                   STR_����     => STR_����,
                                   STR_�Ա�     => STR_�����Ա�,
                                   STR_������Ϣ => STR_������Ϣ);
          IF INT_����ֵ <> 0 THEN
            GOTO �˳�;
          END IF;
        
          SELECT COUNT(1)
            INTO INT_����ֵ
            FROM ������Ŀ_������Ϣ B
           WHERE B.�������� = STR_��������
             AND B.���� = STR_��������
             AND B.���֤�� = STR_����֤������;
        
        END IF;
      
        --ҽԺ���в����ڸ��û� ��ע��
        IF INT_����ֵ = 0 THEN
          PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '30',
                      PRM_��������   => STR_��������,
                      PRM_��������   => '1',
                      PRM_����Ψһ�� => STR_����ID,
                      PRM_ִ�н��   => INT_����ֵ,
                      PRM_������Ϣ   => STR_������Ϣ);
        
          IF INT_����ֵ <> 0 THEN
            INT_����ֵ   := 1;
            STR_������Ϣ := '��ȡ����IDʧ��';
            GOTO �˳�;
          END IF;
        
          -- ���벡�˻�����Ϣ
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
            (STR_��������,
             STR_����ID,
             STR_���߿���,
             STR_��������,
             STR_�����Ա�,
             DAT_���߳�������,
             STR_����,
             STR_�������ڵ�,
             STR_�����ֻ�����,
             FU_ͨ��_����_ת��_��ƴ(STR_��������),
             FU_ͨ��_����_ת��_���(STR_��������),
             SYSDATE,
             STR_ƽ̨��ʶ,
             STR_����֤������);
        
          INT_����ֵ := SQL%ROWCOUNT;
          IF INT_����ֵ = 0 THEN
            INT_����ֵ   := 200703;
            STR_������Ϣ := '�û�����ʧ��';
            GOTO �˳�;
          END IF;
        
          --���벡�˸�����Ϣ
          INSERT INTO ������Ŀ_������Ϣ_����
            (��������,
             ����ID,
             �໤������,
             �໤�����֤��,
             �໤���ֻ�����,
             �໤����ϵ��ַ,
             ������Դ)
          VALUES
            (STR_��������,
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
        ELSE
          --�����û���Ϣ
          SELECT B.����ID, B.����״��, B.������λ
            INTO STR_����ID, STR_����״��, STR_������λ
            FROM ������Ŀ_������Ϣ B
            LEFT JOIN ������Ŀ_������Ϣ_���� C
              ON B.�������� = C.��������
             AND B.����ID = C.����ID
           WHERE B.�������� = STR_��������
             AND B.���� = STR_��������
             AND NVL(B.���֤��, '��ֵ') = (CASE
                   WHEN STR_����֤������ IS NULL THEN
                    '��ֵ'
                   ELSE
                    STR_����֤������
                 END)
             AND C.�໤�����֤�� = STR_�Һ������֤����
             AND ROWNUM = 1;
        END IF;
      
        IF INT_�û�ע������ = '2' THEN
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
             STR_��������,
             STR_����ID,
             STR_�Һ�����,
             STR_��������,
             STR_�����Ա�,
             DAT_���߳�������,
             STR_����֤������,
             STR_����֤������,
             NULL,
             NULL,
             STR_�Һ����ֻ�����,
             STR_�������ڵ�,
             DECODE(STR_�Һ�����, '2', STR_�Һ�������, NULL),
             DECODE(STR_�Һ�����, '2', STR_�Һ���֤������, NULL),
             DECODE(STR_�Һ�����, '2', STR_�Һ������֤����, NULL),
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
    PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '6002',
                PRM_��������   => STR_��������,
                PRM_��������   => '1',
                PRM_����Ψһ�� => STR_������,
                PRM_ִ�н��   => INT_����ֵ,
                PRM_������Ϣ   => STR_������Ϣ);
    IF INT_����ֵ <> 0 THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '����������ʧ��!';
      GOTO �˳�;
    END IF;
  
    -- �����ɹ���ʱ�䡿
    IF STR_�Ƿ�ΪԤԼ�Һ� = '1' THEN
      --HIS�б�ƽ̨����ʱ���Գ�Щ
      SELECT SYSDATE + (1 / (24 * 60)) * 10
        INTO DAT_��������ʱ��
        FROM DUAL;
      STR_������ע := '����5���������֧���������Զ�ȡ��';
    ELSE
      SELECT SYSDATE + (1 / (24 * 60)) * 35
        INTO DAT_��������ʱ��
        FROM DUAL;
      STR_������ע := '����30���������֧���������Զ�ȡ��';
    END IF;
  
    BEGIN
      --���ŹҺ�
      IF STR_�Ƿ�ΪԤԼ�Һ� = '3' THEN
        UPDATE �������_ԤԼ�Һ�
           SET ����ID       = STR_����ID,
               ����         = STR_��������,
               �Ա�         = STR_�����Ա�,
               ��������     = DAT_���߳�������,
               ����״��     = STR_����״��,
               ��ϵ�绰     = STR_�����ֻ�����,
               ��ͥ��ַ     = STR_�������ڵ�,
               ������λ     = STR_������λ,
               ���֤��     = STR_�Һ������֤����,
               ƴ����       = FU_ͨ��_����_ת��_��ƴ(STR_��������),
               �����       = FU_ͨ��_����_ת��_���(STR_��������),
               �Һſ��ұ��� = STR_�Һſ��ұ���,
               �Һſ������� = STR_�Һſ�������,
               �Һſ���λ�� = STR_�Һſ���λ��,
               �Һ�ҽ������ = STR_�Һ�ҽ������,
               �Һ�ҽ������ = STR_�Һ�ҽ������,
               �Һ����ͱ��� = STR_�Һ����ͱ���,
               �Һ��������� = STR_�Һ���������,
               �Һŷ�       = NUM_�Һŷ�,
               ����       = NUM_����,
               �������     = STR_�������,
               ԤԼʱ��     = DAT_�Ű�����,
               �Ű�ID       = STR_�Ű��¼ID,
               ԤԼʱ�α��� = STR_ԤԼʱ�α���,
               ԤԼʱ�ο�ʼ = TO_DATE(TO_CHAR(DAT_�Ű�����, 'yyyy-mm-dd') || ' ' ||
                                STR_ԤԼʱ�ο�ʼ,
                                'yyyy-mm-dd hh24:mi:ss'),
               ԤԼʱ�ν��� = TO_DATE(TO_CHAR(DAT_�Ű�����, 'yyyy-mm-dd') || ' ' ||
                                STR_ԤԼʱ�ν���,
                                'yyyy-mm-dd hh24:mi:ss'),
               ��ʱʱ��     = DAT_��������ʱ��
         WHERE �������� = STR_��������
           AND ����ID = STR_ԤԼ����;
        INT_����ֵ := SQL%ROWCOUNT;
        IF INT_����ֵ = 0 THEN
          INT_����ֵ   := 99;
          STR_������Ϣ := '����ԤԼ��¼ʧ�ܣ�';
          GOTO �˳�;
        END IF;
      
        UPDATE ������ͨ_����
           SET ����ID       = STR_����ID,
               ����ʱ��     = TO_DATE(STR_�µ�ʱ��, 'yyyy-MM-dd hh24:mi:ss'),
               ҽԺ������   = STR_������,
               �Һ�����     = STR_�Һ�����,
               ԤԼ�Һ����� = STR_�Ƿ�ΪԤԼ�Һ�,
               �Һŷ���     = NUM_�Һŷ�,
               ���Ʒ���     = NUM_����,
               ����״̬     = '��֧��',
               ����ʱ��     = DAT_��������ʱ��
         WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
           AND ҽԺ���� = STR_��������
           AND ƽ̨������ = STR_ƽ̨������;
        IF INT_����ֵ = 0 THEN
          INT_����ֵ   := 99;
          STR_������Ϣ := '���¶�����¼ʧ�ܣ�';
          GOTO �˳�;
        END IF;
      ELSE
        --������ԤԼ���š�
        SELECT SEQ_�������_ԤԼ�Һ�_ΨһID.NEXTVAL
          INTO STR_ԤԼ����
          FROM DUAL;
      
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
          (STR_��������,
           STR_ԤԼ����,
           STR_��������,
           STR_�����Ա�,
           DAT_���߳�������,
           STR_����״��,
           STR_�����ֻ�����,
           STR_�������ڵ�,
           STR_������λ,
           STR_�Һ������֤����,
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
           STR_ƽ̨��ʶ,
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
           ����ʱ��,
           ҽԺ������,
           �Һŷ���,
           ���Ʒ���,
           ����ʱ��,
           ����״̬,
           �Һ�����,
           ԤԼ�Һ�����,
           �Һ�����,
           ������,
           ����ʱ��,
           ������,
           ����ʱ��)
        VALUES
          (SEQ_������ͨ_����_��ˮ��.NEXTVAL,
           STR_ƽ̨��ʶ,
           STR_��������,
           STR_����ID,
           STR_ԤԼ����,
           STR_ƽ̨������,
           'ԤԼ�Һ�',
           TO_DATE(STR_�µ�ʱ��, 'yyyy-MM-dd hh24:mi:ss'),
           STR_������,
           NUM_�Һŷ�,
           NUM_����,
           DAT_��������ʱ��,
           '��֧��',
           STR_�Һ�����,
           STR_�Ƿ�ΪԤԼ�Һ�,
           STR_�Һ�����ID,
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
    STR_ȡ��ʱ��� := '0800-' || REPLACE(STR_ԤԼʱ�ν���, ':', '');
    STR_SQL        := 'SELECT ''' || STR_������ || ''' AS HOSP_ORDER_ID,''' ||
                      STR_����ID || ''' AS HOSP_PATIENT_ID,
               '''' AS HOSP_SERIAL_NUM,
               '''' AS HOSP_MEDICAL_NUM,
               ''' || STR_ȡ��ʱ��� || ''' AS HOSP_GETREG_DATE,
               '''' AS HOSP_SEE_DOCT_ADDR,
               '''' AS HOSP_CARD_NO,
               ''' || STR_������ע ||
                      ''' AS HOSP_REMARK,
               ''0'' AS IS_CONCESSIONS
               FROM DUAL';
    LOB_��Ӧ����   := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', '');
  
    INT_����ֵ   := 0;
    STR_������Ϣ := '���׳ɹ�';
  
    COMMIT;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
    
  END;

  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_��������,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => DAT_ϵͳʱ��,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  ROLLBACK;
  RETURN;

END PR_������ͨ_ԤԼ�ҺŵǼ�;
/

prompt
prompt Creating procedure PR_������ͨ_ԤԼ�Һż�¼��ѯ
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_ԤԼ�Һż�¼��ѯ(STR_������� IN VARCHAR2,
                                             STR_ƽ̨��ʶ IN VARCHAR2, --2012
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

  STR_SQL      VARCHAR2(2000);
  STR_�������� VARCHAR2(50);

BEGIN

  BEGIN
    --���������������
    STR_ҽԺID     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    STR_ƽ̨������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ORDER_ID');
    STR_ҽԺ������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_ORDER_ID');
    STR_��ʼ����   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'BEGIN_DATE');
    STR_��������   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'END_DATE');
    STR_��ǰҳ��   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAGE_CURRENT');
    STR_ÿҳ����   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'PAGE_SIZE');
  
    --��������֤��
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
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
    STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    STR_SQL := 'SELECT T.ƽ̨������ AS ORDER_ID,
                   CASE
                     WHEN T.����״̬ = ''��֧��'' THEN
                      ''2''
                     WHEN T.����״̬ = ''��ȡ��'' THEN
                      ''4''
                     WHEN T.����״̬ = ''���˿�'' THEN
                      ''5''
                     WHEN T.����״̬ = ''��֧��'' AND TT.ȥ���־ = ''ԤԼ'' THEN
                      ''6''
                     WHEN T.����״̬ = ''��֧��'' AND TT.ȥ���־ = ''����'' THEN
                      ''3''
                     ELSE
                      ''1''
                   END AS ORDER_STATUS
                   T.ҽԺ����� AS HOSP_SERIAL_NUM,
                   TO_CHAR(TT.ȡ��ʱ��,''yyyy-MM-dd hh24:mi:ss'') AS GET_REGNO_DATE,
                   T.ҽԺ֧���� AS HOSP_PAY_ID,
                   T.���ﲡ���� AS HOSP_MEDICAL_NUM,
                   TO_CHAR(TT.ԤԼʱ�ο�ʼ, ''hh24:mi'') || ''-'' ||
                   TO_CHAR(TT.ԤԼʱ�ν���, ''hh24:mi'') AS HOSP_GETREG_DATE,
                   T.ҽԺ�˿�� AS HOSP_REFUND_ID,
                   T.�˿��־ AS REFUND_FLAG,
                   T.ȡ��ʱ�� AS CANCEL_DATE
              FROM ������ͨ_���� T, �������_ԤԼ�Һ� TT
             WHERE T.ҽԺ���� = TT.��������
               AND T.�������� = TT.����ID
               AND T.ƽ̨��ʶ=''' || STR_ƽ̨��ʶ || '''
               AND T.ҽԺ����=''' || STR_�������� || '''
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
         AND T.ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND T.ҽԺ���� = STR_��������
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

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_��������,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  RETURN;
END PR_������ͨ_ԤԼ�Һż�¼��ѯ;
/

prompt
prompt Creating procedure PR_������ͨ_ԤԼ�Һ�ȡ��
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_ԤԼ�Һ�ȡ��(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2,
                                           STR_���ܱ��� IN VARCHAR2, --2011
                                           STR_���ñ�ʶ IN VARCHAR2, --0ƽ̨  1ҽԺ
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

  STR_ҽԺ����� VARCHAR2(50);
  STR_��������   VARCHAR2(50);
  STR_����Ա���� VARCHAR2(50);
  STR_����Ա���� VARCHAR2(50);

BEGIN
  BEGIN
  
    --���������������
    STR_ҽԺID := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
    IF STR_���ñ�ʶ = '1' THEN
      --ҽԺ
      STR_ҽԺID     := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '2');
      STR_����Ա���� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'OPERATOR_ID');
      STR_����Ա���� := FU_������ͨ_�ڵ�ֵ(STR_�������, 'OPERATOR_NAME');
      BEGIN
        SELECT ֧����ʽ
          INTO STR_���ʽ
          FROM ������ͨ_ƽ̨����
         WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
           AND ROWNUM = 1;
      EXCEPTION
        WHEN OTHERS THEN
          STR_���ʽ := '����֧��';
      END;
    ELSE
      STR_����Ա���� := STR_ƽ̨��ʶ;
      BEGIN
        SELECT ƽ̨����, ֧����ʽ
          INTO STR_����Ա����, STR_���ʽ
          FROM ������ͨ_ƽ̨����
         WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
           AND ROWNUM = 1;
      EXCEPTION
        WHEN OTHERS THEN
          STR_���ʽ   := '����֧��';
          STR_����Ա���� := STR_ƽ̨��ʶ;
      END;
    END IF;
  
    STR_ƽ̨������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ORDER_ID');
  
    STR_�������ͱ��� := '1';
    STR_������������ := '�ֽ�';
    STR_����״̬     := '�ȴ�����';
    STR_�Һ���Դ     := 'ԤԼ';
  
    -- ��������ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    IF STR_ƽ̨������ IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ƽ̨������';
      GOTO �˳�;
    END IF;
    STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    -- ����ȡԤԼ���š�
    BEGIN
      SELECT ��������
        INTO STR_ԤԼ����
        FROM ������ͨ_����
       WHERE ҽԺ���� = STR_��������
         AND ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ƽ̨������ = STR_ƽ̨������
         AND ����״̬ = '��֧��';
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 201101;
        STR_������Ϣ := '�ҺŶ���������';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
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
       WHERE G.�������� = STR_��������
         AND G.����ID = STR_ԤԼ����
         AND G.ȥ���־ = 'ԤԼ'
         AND G.֧����־ = '��'
         AND TO_CHAR(G.ԤԼʱ��, 'yyyymmdd') = TO_CHAR(SYSDATE, 'yyyymmdd');
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 201101;
        STR_������Ϣ := '�ҺŶ���������';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
  
    -- ���������ﲡ���š�
    PR_����_ȡ��ҵ������(STR_��������   => STR_��������,
                  STR_���������� => '���ﲡ����',
                  STR_���ز����� => STR_���ﲡ����,
                  INT_����ֵ     => INT_����ֵ,
                  STR_������Ϣ   => STR_������Ϣ);
    IF INT_����ֵ <> 1 THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '�������ﲡ����ʧ��,ԭ��:' + STR_������Ϣ;
      GOTO �˳�;
    END IF;
  
    -- �������Һ���š�
    PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '26',
                PRM_��������   => STR_��������,
                PRM_��������   => '1',
                PRM_����Ψһ�� => STR_�Һ����,
                PRM_ִ�н��   => INT_����ֵ,
                PRM_������Ϣ   => STR_������Ϣ);
    IF INT_����ֵ <> 0 THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '�����Һ����ʧ��!';
      GOTO �˳�;
    END IF;
  
    -- �������Һŵ��š�
    SELECT FU_����_��ȡ��ǰƱ�ݺ�(STR_��������, STR_����Ա����, '4')
      INTO STR_�Һŵ���
      FROM DUAL;
  
    IF STR_�Һŵ��� = '�뵽����������Ʊ��' THEN
      INT_����ֵ   := 99;
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
        (STR_��������,
         STR_����ID,
         STR_���ﲡ����,
         STR_�Һ����,
         STR_�Һŵ���,
         STR_�Һſ��ұ���,
         STR_�Һſ���λ��,
         STR_�Һ�ҽ������,
         STR_�Һ����ͱ���,
         STR_����Ա����,
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
        INT_����ֵ   := 99;
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
        (STR_��������,
         STR_�Һŵ���,
         NUM_�ܷ���,
         STR_���ʽ,
         '�Һ�',
         STR_����Ա����,
         STR_����Ա����,
         SYSDATE,
         STR_�Һ����,
         STR_�Һ����,
         '�Һ�',
         STR_�������ͱ���,
         STR_������������);
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '������֧������ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      -- ����ԤԼ״̬
      UPDATE �������_ԤԼ�Һ�
         SET ȥ���־ = '����',
             �Һ���� = STR_�Һ����,
             ȡ��ʱ�� = DAT_ϵͳʱ��
       WHERE �������� = STR_��������
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
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ҽԺ���� = STR_��������
         AND ƽ̨������ = STR_ƽ̨������
         AND �������� = STR_ԤԼ����
         AND �������� = 'ԤԼ�Һ�';
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '���¶���״̬ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      IF STR_���ñ�ʶ = '0' THEN
        STR_SQL      := 'SELECT ''' || STR_ҽԺ����� ||
                        ''' AS HOSP_SERIAL_NUM,      
                         '''' AS REMARK FROM DUAL';
        LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', '');
      ELSE
        LOB_��Ӧ���� := STR_�Һŵ���;
      END IF;
    
      INT_����ֵ   := 0;
      STR_������Ϣ := '���׳ɹ�';
    
      COMMIT;
    
    EXCEPTION
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '��Ӧ���󱨴�:' || SQLERRM;
        GOTO �˳�;
    END;
  
  END;
  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_��������,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => DAT_ϵͳʱ��,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);
  ROLLBACK;
  RETURN;

END PR_������ͨ_ԤԼ�Һ�ȡ��;
/

prompt
prompt Creating procedure PR_������ͨ_ԤԼ�Һ�ȡ��
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_ԤԼ�Һ�ȡ��(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2, --2009
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
  STR_ԤԼ���� VARCHAR2(50);
  STR_����״̬ VARCHAR2(50);
  STR_�������� VARCHAR2(50);

BEGIN

  --�����������
  STR_ҽԺID     := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOS_ID');
  STR_ƽ̨������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'ORDER_ID');
  STR_ҽԺ������ := FU_������ͨ_�ڵ�ֵ(STR_�������, 'HOSP_ORDER_ID');
  STR_ȡ��ʱ��   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'CANCEL_DATE');
  STR_ȡ��ԭ��   := FU_������ͨ_�ڵ�ֵ(STR_�������, 'CANCEL_REMARK');
  --����֤���ݡ�

  IF STR_ҽԺID IS NULL THEN
    INT_����ֵ   := 1;
    STR_������Ϣ := '�봫��ҽԺID';
    GOTO �˳�;
  END IF;
  IF STR_ƽ̨������ IS NULL THEN
    INT_����ֵ   := 1;
    STR_������Ϣ := '�봫��ƽ̨������';
    GOTO �˳�;
  END IF;

  IF STR_ȡ��ʱ�� IS NULL OR FU_����ת����(STR_ȡ��ʱ��) IS NULL THEN
    INT_����ֵ   := 1;
    STR_������Ϣ := '�봫����ȷ��ȡ��ʱ��';
    GOTO �˳�;
  END IF;
  STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
  IF STR_�������� IS NULL THEN
    INT_����ֵ   := 1;
    STR_������Ϣ := 'ҽԺID��Ч';
    GOTO �˳�;
  END IF;

  --����֤����״̬��
  BEGIN
    SELECT ��������, ����״̬
      INTO STR_ԤԼ����, STR_����״̬
      FROM ������ͨ_����
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND ҽԺ���� = STR_��������
       AND ƽ̨������ = STR_ƽ̨������;
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      INT_����ֵ   := 200901;
      STR_������Ϣ := '�ҺŶ���������';
      GOTO �˳�;
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  IF STR_����״̬ = '��֧��' THEN
    INT_����ֵ   := 200902;
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

  SELECT COUNT(1)
    INTO INT_����ֵ
    FROM �������_ԤԼ�Һ�
   WHERE �������� = STR_��������
     AND ����ID = STR_ԤԼ����
     AND ȥ���־ = 'ռ��'
     AND ��ʱʱ�� < SYSDATE;

  IF INT_����ֵ > 0 THEN
    INT_����ֵ   := 200903;
    STR_������Ϣ := '�ҺŶ����ѹر�';
    GOTO �˳�;
  END IF;

  -- �����ܴ���
  BEGIN
  
    -- ����ԤԼ��
    UPDATE �������_ԤԼ�Һ�
       SET ȥ���־ = '����'
     WHERE �������� = STR_��������
       AND ����ID = STR_ԤԼ����
       AND ȥ���־ = 'ռ��';
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '����ԤԼ��ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
    -- ���¶���״̬
    UPDATE ������ͨ_����
       SET ����״̬ = '��ȡ��',
           ȡ��ʱ�� = TO_DATE(STR_ȡ��ʱ��, 'yyyy-MM-dd hh24:mi:ss'),
           ȡ��ԭ�� = STR_ȡ��ԭ��,
           ������   = STR_ƽ̨��ʶ,
           ����ʱ�� = SYSDATE
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND ƽ̨������ = STR_ƽ̨������
       AND �������� = STR_ԤԼ����
       AND ����״̬ = '��֧��';
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '���¶���ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
    LOB_��Ӧ���� := '<RES></RES>';
  
    INT_����ֵ   := 0;
    STR_������Ϣ := '���׳ɹ�';
  
    COMMIT;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '��Ӧ���󱨴�:' || SQLERRM;
      GOTO �˳�;
  END;

  --���쳣�˳���
  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_��������,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  ROLLBACK;
  RETURN;

END PR_������ͨ_ԤԼ�Һ�ȡ��;
/

prompt
prompt Creating procedure PR_������ͨ_ԤԼ�Һ��˿�
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_ԤԼ�Һ��˿�(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2, --2010
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

  STR_ԤԼ����         VARCHAR2(50);
  STR_����״̬         VARCHAR2(50);
  STR_SQL              VARCHAR2(1000);
  STR_ҽԺ�˿��     VARCHAR2(50);
  NUM_ʵ�����         NUMBER;
  STR_���Ű��ʶ       VARCHAR2(50);
  str_֧������         varchar2(50);
  NUM_�������         NUMBER(10, 3);
  STR_��������         VARCHAR2(50);

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

  --����֤������
  IF STR_ҽԺID IS NULL THEN
    INT_����ֵ   := 1;
    STR_������Ϣ := '�봫��ҽԺID';
    GOTO �˳�;
  END IF;
  IF STR_ƽ̨������ IS NULL THEN
    INT_����ֵ   := 1;
    STR_������Ϣ := '�봫��ƽ̨������';
    GOTO �˳�;
  END IF;
  IF STR_ҽԺ������ IS NULL THEN
    INT_����ֵ   := 1;
    STR_������Ϣ := '�봫��ҽԺ������';
    GOTO �˳�;
  END IF;
  IF STR_ƽ̨�˿�� IS NULL THEN
    INT_����ֵ   := 1;
    STR_������Ϣ := '�봫��ƽ̨�˿��';
    GOTO �˳�;
  END IF;
  IF STR_�ܽ�� IS NULL THEN
    INT_����ֵ   := 1;
    STR_������Ϣ := '�봫���ܽ��';
    GOTO �˳�;
  END IF;
  IF STR_�˿��� IS NULL THEN
    INT_����ֵ   := 1;
    STR_������Ϣ := '�봫���˿���';
    GOTO �˳�;
  END IF;
  STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
  IF STR_�������� IS NULL THEN
    INT_����ֵ   := 1;
    STR_������Ϣ := 'ҽԺID��Ч';
    GOTO �˳�;
  END IF;

  BEGIN
    SELECT �������
      INTO NUM_�������
      FROM ������ͨ_ƽ̨����
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND ��Ч״̬ = '1';
  EXCEPTION
    WHEN OTHERS THEN
      NUM_������� := 100;
  END;

  --����֤����״̬��
  BEGIN
    SELECT ��������, ����״̬, ʵ�����, ҽԺ������, ֧������
      INTO STR_ԤԼ����,
           STR_����״̬,
           NUM_ʵ�����,
           STR_ҽԺ������,
           str_֧������
      FROM ������ͨ_����
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND ҽԺ���� = STR_��������
       AND ƽ̨������ = STR_ƽ̨������;
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      INT_����ֵ   := 201001;
      STR_������Ϣ := '�ҺŶ���������';
      GOTO �˳�;
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
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

  IF NUM_ʵ����� * NUM_������� <> TO_NUMBER(STR_�˿���) THEN
    INT_����ֵ   := 201003;
    STR_������Ϣ := '�˿����ȷ';
    GOTO �˳�;
  END IF;

  -- ��֤ԤԼ��
  BEGIN
    SELECT �հ�α�ʶ
      INTO STR_���Ű��ʶ
      FROM �������_ԤԼ�Һ�
     WHERE �������� = STR_��������
       AND ����ID = STR_ԤԼ����
       AND ȥ���־ = 'ԤԼ';
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      INT_����ֵ   := 201001;
      STR_������Ϣ := '�ҺŶ���������';
      GOTO �˳�;
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
    
  END;

  -- �����ܴ���
  BEGIN
  
    -- ������
    UPDATE �������_���Ű�ʱ�α�
       SET �ѹҺ��� = �ѹҺ��� - 1
     WHERE �������� = STR_��������
       AND �հ�α�ʶ = STR_���Ű��ʶ;
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '������Դʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
    -- ����ԤԼ��
    UPDATE �������_ԤԼ�Һ�
       SET ȥ���־ = '����'
     WHERE �������� = STR_��������
       AND ����ID = STR_ԤԼ����
       AND ȥ���־ = 'ԤԼ';
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '����ԤԼ��ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
    --�����ɶ����š�
    PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '6002',
                PRM_��������   => STR_��������,
                PRM_��������   => '1',
                PRM_����Ψһ�� => STR_ҽԺ�˿��,
                PRM_ִ�н��   => INT_����ֵ,
                PRM_������Ϣ   => STR_������Ϣ);
    IF INT_����ֵ <> 0 THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '����ҽԺ�˿��ʧ��!';
      GOTO �˳�;
    END IF;
  
    -- ���¶���״̬
    UPDATE ������ͨ_����
       SET ����״̬       = '���˿�',
           ƽ̨�˿��     = STR_ƽ̨�˿��,
           ƽ̨�˿���ˮ�� = STR_�˿���ˮ��,
           �˿�ʱ��       = DECODE(STR_�˿�����,
                               NULL,
                               SYSDATE,
                               TO_DATE(STR_�˿����� || ' ' || STR_�˿�ʱ��,
                                       'yyyy-MM-dd hh24:mi:ss')),
           ҽԺ�˿��     = STR_ҽԺ�˿��,
           �˿���       = NUM_ʵ�����,
           �˿��־       = '1', --�ɹ� ƽ̨�˿�
           ������         = STR_ƽ̨��ʶ,
           ����ʱ��       = SYSDATE
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND ҽԺ���� = STR_��������
       AND ƽ̨������ = STR_ƽ̨������
       AND �������� = STR_ԤԼ����;
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '���¶���ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
    STR_SQL := 'SELECT ''' || STR_ҽԺ�˿�� ||
               ''' AS HOSP_REFUND_ID, 
                   ''1'' AS REFUND_FLAG FROM DUAL';
  
    LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', '');
  
    INT_����ֵ   := 0;
    STR_������Ϣ := '���׳ɹ�';
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '��Ӧ���󱨴�:' || SQLERRM;
      GOTO �˳�;
  END;

  COMMIT;

  --���쳣�˳���
  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_��������,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => SYSDATE,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  ROLLBACK;
  RETURN;

END PR_������ͨ_ԤԼ�Һ��˿�;
/

prompt
prompt Creating procedure PR_������ͨ_ԤԼ�Һ�֧��
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_ԤԼ�Һ�֧��(STR_������� IN VARCHAR2,
                                           STR_ƽ̨��ʶ IN VARCHAR2, --2008
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
  NUM_�Һŷ���     NUMBER;
  NUM_���Ʒ���     NUMBER;
  STR_��α�ʶ     VARCHAR2(50);
  DAT_ԤԼ����ʱ�� DATE;
  STR_ȡ��ʱ���   VARCHAR2(50);
  NUM_�������     NUMBER(10, 3);
  STR_��������     VARCHAR2(50);

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
  
    --����֤���ݡ�
    IF STR_ҽԺID IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ҽԺID';
      GOTO �˳�;
    END IF;
    IF STR_ƽ̨������ IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ƽ̨������';
      GOTO �˳�;
    END IF;
    IF STR_��ˮ�� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫����ˮ��';
      GOTO �˳�;
    END IF;
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫�뽻������';
      GOTO �˳�;
    END IF;
    IF STR_����ʱ�� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫�뽻��ʱ��';
      GOTO �˳�;
    END IF;
    IF STR_�ܽ�� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫���ܽ��';
      GOTO �˳�;
    END IF;
    IF STR_Ӧ����� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��Ӧ�����';
      GOTO �˳�;
    END IF;
    IF STR_ʵ����� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := '�봫��ʵ�����';
      GOTO �˳�;
    END IF;
  
    STR_�������� := FU_������ͨ_ҽԺIDת��(STR_ƽ̨��ʶ, STR_ҽԺID, '1');
    IF STR_�������� IS NULL THEN
      INT_����ֵ   := 1;
      STR_������Ϣ := 'ҽԺID��Ч';
      GOTO �˳�;
    END IF;
  
    BEGIN
      SELECT �������
        INTO NUM_�������
        FROM ������ͨ_ƽ̨����
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ��Ч״̬ = '1';
    EXCEPTION
      WHEN OTHERS THEN
        NUM_������� := 100;
    END;
  
    --����֤����״̬��
    BEGIN
      SELECT ��������, ����״̬, �Һŷ���, ���Ʒ���
        INTO STR_ԤԼ����, STR_����״̬, NUM_�Һŷ���, NUM_���Ʒ���
        FROM ������ͨ_����
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ҽԺ���� = STR_��������
         AND ƽ̨������ = STR_ƽ̨������;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 200801;
        STR_������Ϣ := '�ҺŶ���������';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
  
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
     WHERE �������� = STR_��������
       AND ����ID = STR_ԤԼ����
       AND ȥ���־ = 'ռ��'
       AND ��ʱʱ�� < SYSDATE;
  
    IF INT_����ֵ > 0 THEN
      INT_����ֵ   := 200803;
      STR_������Ϣ := '�ҺŶ����ѹر�';
      GOTO �˳�;
    END IF;
  
    --��֤����
    IF (NUM_�Һŷ��� + NUM_���Ʒ���) * NUM_������� <> TO_NUMBER(STR_ʵ�����) THEN
      INT_����ֵ   := 200804;
      STR_������Ϣ := '�ҺŽ���ȷ';
      GOTO �˳�;
    END IF;
  
    -- ��ȡ�հ�α�ʶ
    BEGIN
      SELECT �հ�α�ʶ, ԤԼʱ�ν���
        INTO STR_��α�ʶ, DAT_ԤԼ����ʱ��
        FROM �������_ԤԼ�Һ�
       WHERE �������� = STR_��������
         AND ����ID = STR_ԤԼ����;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := 200803;
        STR_������Ϣ := '�ҺŶ����ѹر�';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
    IF SYSDATE > DAT_ԤԼ����ʱ�� THEN
      INT_����ֵ   := 200803;
      STR_������Ϣ := '�ҺŶ����ѹر�';
      GOTO �˳�;
    END IF;
  
    -- �����ܴ���
    BEGIN
    
      -- ����ԤԼ��
      UPDATE �������_ԤԼ�Һ� G
         SET ֧����־ = '��', ȥ���־ = 'ԤԼ'
       WHERE �������� = STR_��������
         AND ����ID = STR_ԤԼ����;
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '����ԤԼ��ʧ��';
        GOTO �˳�;
      END IF;
    
      -- ����
      UPDATE �������_���Ű�ʱ�α�
         SET �ѹҺ��� = �ѹҺ��� + 1
       WHERE �������� = STR_��������
         AND �հ�α�ʶ = STR_��α�ʶ;
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '������Դʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      --������ҽԺ֧�����š�
      PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '6002',
                  PRM_��������   => STR_��������,
                  PRM_��������   => '1',
                  PRM_����Ψһ�� => STR_ҽԺ֧������,
                  PRM_ִ�н��   => INT_����ֵ,
                  PRM_������Ϣ   => STR_������Ϣ);
      IF INT_����ֵ <> 0 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '����ҽԺ֧������ʧ��!';
        GOTO �˳�;
      END IF;
    
      -- ���¶���״̬
      UPDATE ������ͨ_����
         SET ����״̬       = '��֧��',
             �ܽ��         = TO_NUMBER(STR_�ܽ��) / NUM_�������,
             Ӧ�����       = TO_NUMBER(STR_Ӧ�����) / NUM_�������,
             ʵ�����       = TO_NUMBER(STR_ʵ�����) / NUM_�������,
             ƽ̨������ˮ�� = STR_��ˮ��,
             ֧��ʱ��       = TO_DATE(STR_�������� || ' ' || STR_����ʱ��,
                                  'yyyy-MM-dd hh24:mi:ss'),
             ҽԺ֧����     = STR_ҽԺ֧������,
             ֧������       = STR_֧������ID,
             ƽ̨�˿��     = NULL,
             �˿�ʱ��       = NULL,
             ������         = STR_ƽ̨��ʶ,
             ����ʱ��       = DAT_ϵͳʱ��
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND ҽԺ���� = STR_��������
         AND ƽ̨������ = STR_ƽ̨������
         AND ����״̬ = '��֧��';
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := 99;
        STR_������Ϣ := '���¶���ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
    END;
    STR_ȡ��ʱ��� := '0800-' || TO_CHAR(DAT_ԤԼ����ʱ��, 'hh24mi');
    STR_SQL        := 'SELECT ''' || STR_ҽԺ֧������ ||
                      ''' AS HOSP_PAY_ID,
                            '''' AS RECEIPT_ID,
                            '''' AS HOSP_SERIAL_NUM,
                            '''' AS HOSP_MEDICAL_NUM,
                            ''' || STR_ȡ��ʱ��� ||
                      ''' AS HOSP_GETREG_DATE,
                            '''' AS HOSP_SEE_DOCT_ADDR,
                            '''' AS HOSP_REMARK
                            FROM DUAL';
    LOB_��Ӧ����   := FU_������ͨ_�õ���Ӧ����(STR_SQL, 'RES', '');
  
    INT_����ֵ   := 0;
    STR_������Ϣ := '���׳ɹ�';
  
    COMMIT;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := 99;
      STR_������Ϣ := '��Ӧ���󱨴�' || SQLERRM;
      GOTO �˳�;
  END;

  --���쳣�˳���
  <<�˳�>>

  -- ��������־��
  PR_������ͨ_������־(STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
               STR_ҽԺ���� => STR_��������,
               STR_���ܱ��� => STR_���ܱ���,
               STR_������� => STR_�������,
               DAT_����ʱ�� => DAT_ϵͳʱ��,
               INT_����ֵ   => INT_����ֵ,
               STR_������Ϣ => STR_������Ϣ,
               DAT_ִ��ʱ�� => SYSDATE);

  ROLLBACK;
  RETURN;

END PR_������ͨ_ԤԼ�Һ�֧��;
/

prompt
prompt Creating procedure PR_������ͨ_���ߵ���
prompt ===============================
prompt
CREATE OR REPLACE PROCEDURE CLOUDHIS.PR_������ͨ_���ߵ���(STR_���ܺ�   IN VARCHAR2,
                                         STR_������� IN VARCHAR2,
                                         LOB_��Ӧ���� OUT CLOB,
                                         RES_CODE     OUT VARCHAR2,
                                         RES_MSG      OUT VARCHAR2) IS
  STR_ƽ̨��ʶ VARCHAR2(10) := '12320';
BEGIN
  BEGIN
    IF STR_���ܺ� = '1001' THEN
      LOB_��Ӧ���� := FU_������ͨ_�õ���Ӧ����('SELECT TO_CHAR(SYSDATE,''yyyy-MM-dd hh24:mi:ss'') AS "SYSDATE" FROM DUAL',
                                 'RES',
                                 '');
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
      RES_CODE := 200502;
      RES_MSG  := '�û�����Ϣ��ƥ��';    
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
      PR_������ͨ_��Դ����(STR_������� => STR_�������,
                   STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                   STR_���ܱ��� => STR_���ܺ�,
                   LOB_��Ӧ���� => LOB_��Ӧ����,
                   INT_����ֵ   => RES_CODE,
                   STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2006' THEN
      PR_������ͨ_��Դ����(STR_������� => STR_�������,
                   STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                   STR_���ܱ��� => STR_���ܺ�,
                   LOB_��Ӧ���� => LOB_��Ӧ����,
                   INT_����ֵ   => RES_CODE,
                   STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2007' THEN
      PR_������ͨ_ԤԼ�ҺŵǼ�(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2008' THEN
      PR_������ͨ_ԤԼ�Һ�֧��(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2009' THEN
      PR_������ͨ_ԤԼ�Һ�ȡ��(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2010' THEN
      PR_������ͨ_ԤԼ�Һ��˿�(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2011' THEN
      PR_������ͨ_ԤԼ�Һ�ȡ��(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     STR_���ñ�ʶ => '0',
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '2012' THEN
      PR_������ͨ_ԤԼ�Һż�¼��ѯ(STR_������� => STR_�������,
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
      PR_������ͨ_�ɷѵ�֧��(STR_������� => STR_�������,
                    STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                    STR_���ܱ��� => STR_���ܺ�,
                    LOB_��Ӧ���� => LOB_��Ӧ����,
                    INT_����ֵ   => RES_CODE,
                    STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '3004' THEN
      PR_������ͨ_�ɷѶ�����ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '4001' THEN
      RES_CODE := 400101;
      RES_MSG  := '�ŶӼ�¼�����ڣ�δ��ѯ���ŶӼ�¼';
    ELSIF STR_���ܺ� = '8001' THEN
      PR_������ͨ_�������б��ѯ(STR_������� => STR_�������,
                       STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                       STR_���ܱ��� => STR_���ܺ�,
                       LOB_��Ӧ���� => LOB_��Ӧ����,
                       INT_����ֵ   => RES_CODE,
                       STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '8002' THEN
      PR_������ͨ_���鱨���ѯ(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSIF STR_���ܺ� = '8003' THEN
      PR_������ͨ_���鱨���ѯ(STR_������� => STR_�������,
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
    
    ELSIF STR_���ܺ� = '5004' THEN
      PR_������ͨ_ԤԼ�Һ�ȡ��(STR_������� => STR_�������,
                     STR_ƽ̨��ʶ => STR_ƽ̨��ʶ,
                     STR_���ܱ��� => STR_���ܺ�,
                     STR_���ñ�ʶ => '1',
                     LOB_��Ӧ���� => LOB_��Ӧ����,
                     INT_����ֵ   => RES_CODE,
                     STR_������Ϣ => RES_MSG);
    ELSE
      RES_CODE := '-1';
      RES_MSG  := '���ܺŴ���';
    END IF;
    RETURN;
  END;

END PR_������ͨ_���ߵ���;
/


prompt Done
spool off
set define on
