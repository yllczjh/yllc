prompt PL/SQL Developer Export User Objects for user USERS@47.104.4.221:9900/YKEY
prompt Created by syyyhl on 2021-02-03
set define off
spool ������ͨ�洢����.log

prompt
prompt Creating table ƽ̨�ӿ�_������־
prompt ========================
prompt
create table ƽ̨�ӿ�_������־
(
  ��ˮ��   NUMBER not null,
  ƽ̨��ʶ  VARCHAR2(50),
  �ͻ��˱�ʶ VARCHAR2(50),
  ҽԺ����  VARCHAR2(50),
  ���ܱ���  VARCHAR2(50),
  �������  VARCHAR2(4000),
  ����ʱ��  DATE,
  ������  VARCHAR2(4000),
  ��������  VARCHAR2(50),
  ִ����   VARCHAR2(50),
  ִ��ʱ��  DATE,
  ִ��״̬  VARCHAR2(50) default 0 not null
)
tablespace USERS
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
comment on table ƽ̨�ӿ�_������־
  is 'ƽ̨�ӿ�_������־';
comment on column ƽ̨�ӿ�_������־.��ˮ��
  is '��ˮ��';
comment on column ƽ̨�ӿ�_������־.ƽ̨��ʶ
  is 'ƽ̨��ʶ';
comment on column ƽ̨�ӿ�_������־.�ͻ��˱�ʶ
  is '�ͻ��˱�ʶ';
comment on column ƽ̨�ӿ�_������־.ҽԺ����
  is 'ҽԺ����';
comment on column ƽ̨�ӿ�_������־.���ܱ���
  is '���ܱ���';
comment on column ƽ̨�ӿ�_������־.�������
  is '�������';
comment on column ƽ̨�ӿ�_������־.����ʱ��
  is '����ʱ��';
comment on column ƽ̨�ӿ�_������־.������
  is '������';
comment on column ƽ̨�ӿ�_������־.��������
  is '��������ҵ�����ˮ��';
comment on column ƽ̨�ӿ�_������־.ִ����
  is 'ִ����';
comment on column ƽ̨�ӿ�_������־.ִ��ʱ��
  is 'ִ��ʱ��';
comment on column ƽ̨�ӿ�_������־.ִ��״̬
  is '0���ɹ���-1��ʧ��';
alter table ƽ̨�ӿ�_������־
  add primary key (��ˮ��)
  using index 
  tablespace USERS
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
prompt Creating table ƽ̨�ӿ�_����
prompt ======================
prompt
create table ƽ̨�ӿ�_����
(
  ��ˮ��    NUMBER not null,
  ƽ̨��ʶ   VARCHAR2(50) not null,
  �ͻ��˱�ʶ  VARCHAR2(50) not null,
  ҽԺ����   VARCHAR2(50),
  ����id   VARCHAR2(50) not null,
  ���ﲡ����  VARCHAR2(50),
  ��������   VARCHAR2(50),
  ������    VARCHAR2(50) not null,
  ��������   VARCHAR2(50) not null,
  ����ʱ��   DATE not null,
  Ӧ�����   NUMBER(10,4) not null,
  �Żݽ��   NUMBER(10,4),
  ʵ�ս��   NUMBER(10,4) not null,
  ����ʱ��   DATE,
  ����״̬   VARCHAR2(50) not null,
  ҽԺ������  VARCHAR2(50),
  ҽԺ���׺�  VARCHAR2(50),
  ҽԺ�˿��  VARCHAR2(50),
  ƽ̨������  VARCHAR2(50),
  ƽ̨����ʱ�� DATE,
  ƽ̨���׺�  VARCHAR2(50),
  ƽ̨����ʱ�� DATE,
  ƽ̨�˿��  VARCHAR2(50),
  ƽ̨�˿�ʱ�� DATE,
  ������    VARCHAR2(50) not null,
  ����ʱ��   DATE not null,
  ������    VARCHAR2(50),
  ����ʱ��   DATE,
  �����    INTEGER default 0,
  ��ע     VARCHAR2(4000),
  ״̬     VARCHAR2(50) default 0 not null
)
tablespace USERS
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
comment on table ƽ̨�ӿ�_����
  is 'ƽ̨�ӿ�_����';
comment on column ƽ̨�ӿ�_����.��ˮ��
  is '��ˮ��';
comment on column ƽ̨�ӿ�_����.ƽ̨��ʶ
  is 'ƽ̨��ʶ';
comment on column ƽ̨�ӿ�_����.�ͻ��˱�ʶ
  is '�ͻ��˱�ʶ';
comment on column ƽ̨�ӿ�_����.ҽԺ����
  is 'ҽԺ����';
comment on column ƽ̨�ӿ�_����.����id
  is '����ID';
comment on column ƽ̨�ӿ�_����.���ﲡ����
  is 'סԺ�����ţ����ﲡ����';
comment on column ƽ̨�ӿ�_����.��������
  is '��������';
comment on column ƽ̨�ӿ�_����.������
  is '������';
comment on column ƽ̨�ӿ�_����.��������
  is 'ԤԼ�Һţ�ԤԼ�˺ţ������շѣ�Ԥ��Ѻ�𣻳�Ժ����';
comment on column ƽ̨�ӿ�_����.����ʱ��
  is '����ʱ��';
comment on column ƽ̨�ӿ�_����.Ӧ�����
  is 'Ӧ�����';
comment on column ƽ̨�ӿ�_����.�Żݽ��
  is '�Żݽ��';
comment on column ƽ̨�ӿ�_����.ʵ�ս��
  is 'ʵ�ս��';
comment on column ƽ̨�ӿ�_����.����ʱ��
  is 'Ĭ��Ϊ��������ʱ��+15����';
comment on column ƽ̨�ӿ�_����.����״̬
  is '��֧������֧�������˿��ȡ������ɾ����';
comment on column ƽ̨�ӿ�_����.ҽԺ������
  is 'ҽԺ������';
comment on column ƽ̨�ӿ�_����.ҽԺ���׺�
  is 'ҽԺ���׺�';
comment on column ƽ̨�ӿ�_����.ҽԺ�˿��
  is 'ҽԺ���׺�';
comment on column ƽ̨�ӿ�_����.ƽ̨������
  is 'ƽ̨������';
comment on column ƽ̨�ӿ�_����.ƽ̨����ʱ��
  is 'ƽ̨����ʱ��';
comment on column ƽ̨�ӿ�_����.ƽ̨���׺�
  is 'ƽ̨���׺�';
comment on column ƽ̨�ӿ�_����.ƽ̨����ʱ��
  is 'ƽ̨����ʱ��';
comment on column ƽ̨�ӿ�_����.ƽ̨�˿��
  is 'ƽ̨�˿��';
comment on column ƽ̨�ӿ�_����.ƽ̨�˿�ʱ��
  is 'ƽ̨�˿�ʱ��';
comment on column ƽ̨�ӿ�_����.������
  is '������';
comment on column ƽ̨�ӿ�_����.����ʱ��
  is '����ʱ��';
comment on column ƽ̨�ӿ�_����.������
  is '������';
comment on column ƽ̨�ӿ�_����.����ʱ��
  is '����ʱ��';
comment on column ƽ̨�ӿ�_����.�����
  is '�����';
comment on column ƽ̨�ӿ�_����.��ע
  is '��ע';
comment on column ƽ̨�ӿ�_����.״̬
  is '״̬';
alter table ƽ̨�ӿ�_����
  add primary key (��ˮ��)
  using index 
  tablespace USERS
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
prompt Creating table ƽ̨�ӿ�_������ϸ
prompt ========================
prompt
create table ƽ̨�ӿ�_������ϸ
(
  ��ˮ��  NUMBER not null,
  ������  VARCHAR2(50) not null,
  ������� VARCHAR2(50),
  С����� VARCHAR2(50),
  ��Ŀ���� VARCHAR2(50),
  ��Ŀ���� VARCHAR2(200),
  ���   VARCHAR2(200),
  ���κ�  VARCHAR2(50),
  ����   NUMBER(10,4) not null,
  ��λ   VARCHAR2(50),
  ����   NUMBER(10,4) not null,
  �ܽ��  NUMBER(10,4) not null,
  ������� VARCHAR2(50),
  Ψһ���� VARCHAR2(50)
)
tablespace USERS
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
comment on table ƽ̨�ӿ�_������ϸ
  is 'ƽ̨�ӿ�_������ϸ';
comment on column ƽ̨�ӿ�_������ϸ.��ˮ��
  is '��ˮ��';
comment on column ƽ̨�ӿ�_������ϸ.������
  is '������';
comment on column ƽ̨�ӿ�_������ϸ.�������
  is '�������';
comment on column ƽ̨�ӿ�_������ϸ.С�����
  is 'С�����';
comment on column ƽ̨�ӿ�_������ϸ.��Ŀ����
  is '��Ŀ����';
comment on column ƽ̨�ӿ�_������ϸ.��Ŀ����
  is '��Ŀ����';
comment on column ƽ̨�ӿ�_������ϸ.���
  is '���';
comment on column ƽ̨�ӿ�_������ϸ.���κ�
  is '���κ�';
comment on column ƽ̨�ӿ�_������ϸ.����
  is '����';
comment on column ƽ̨�ӿ�_������ϸ.��λ
  is '��λ';
comment on column ƽ̨�ӿ�_������ϸ.����
  is '����';
comment on column ƽ̨�ӿ�_������ϸ.�ܽ��
  is '�ܽ��';
comment on column ƽ̨�ӿ�_������ϸ.�������
  is '�������';
comment on column ƽ̨�ӿ�_������ϸ.Ψһ����
  is 'Ψһ����';
alter table ƽ̨�ӿ�_������ϸ
  add primary key (��ˮ��)
  using index 
  tablespace USERS
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
prompt Creating table ƽ̨�ӿ�_��������Ϣ
prompt =========================
prompt
create table ƽ̨�ӿ�_��������Ϣ
(
  ��ˮ��     NUMBER not null,
  ƽ̨��ʶ    VARCHAR2(50) not null,
  �ͻ��˱�ʶ   VARCHAR2(50) not null,
  ҽԺ����    VARCHAR2(50) not null,
  ����id    VARCHAR2(50) not null,
  ���������   VARCHAR2(50) not null,
  ����      VARCHAR2(200) not null,
  �Ա�      VARCHAR2(50),
  ��������    DATE,
  ���֤��    VARCHAR2(50),
  �ֻ�����    VARCHAR2(50),
  ��ϵ��ַ    VARCHAR2(50),
  �໤������   VARCHAR2(200),
  �໤�����֤�� VARCHAR2(50),
  �໤���ֻ����� VARCHAR2(50),
  �໤����ϵ��ַ VARCHAR2(50),
  ������     VARCHAR2(50) not null,
  ����ʱ��    DATE not null,
  ������     VARCHAR2(50),
  ����ʱ��    DATE,
  �����     INTEGER default 0 not null,
  ��ע      VARCHAR2(4000),
  ״̬      VARCHAR2(50) default 0 not null
)
tablespace USERS
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
comment on table ƽ̨�ӿ�_��������Ϣ
  is 'ƽ̨�ӿ�_��������Ϣ';
comment on column ƽ̨�ӿ�_��������Ϣ.��ˮ��
  is '��ˮ��';
comment on column ƽ̨�ӿ�_��������Ϣ.ƽ̨��ʶ
  is 'ƽ̨��ʶ';
comment on column ƽ̨�ӿ�_��������Ϣ.�ͻ��˱�ʶ
  is '�ͻ��˱�ʶ';
comment on column ƽ̨�ӿ�_��������Ϣ.ҽԺ����
  is 'ҽԺ����';
comment on column ƽ̨�ӿ�_��������Ϣ.����id
  is '����ID';
comment on column ƽ̨�ӿ�_��������Ϣ.���������
  is '���������';
comment on column ƽ̨�ӿ�_��������Ϣ.����
  is '����';
comment on column ƽ̨�ӿ�_��������Ϣ.�Ա�
  is '�Ա�';
comment on column ƽ̨�ӿ�_��������Ϣ.��������
  is '��������';
comment on column ƽ̨�ӿ�_��������Ϣ.���֤��
  is '���֤��';
comment on column ƽ̨�ӿ�_��������Ϣ.�ֻ�����
  is '�ֻ�����';
comment on column ƽ̨�ӿ�_��������Ϣ.��ϵ��ַ
  is '��ϵ��ַ';
comment on column ƽ̨�ӿ�_��������Ϣ.�໤������
  is '�໤������';
comment on column ƽ̨�ӿ�_��������Ϣ.�໤�����֤��
  is '�໤�����֤��';
comment on column ƽ̨�ӿ�_��������Ϣ.�໤���ֻ�����
  is '�໤���ֻ�����';
comment on column ƽ̨�ӿ�_��������Ϣ.�໤����ϵ��ַ
  is '�໤����ϵ��ַ';
comment on column ƽ̨�ӿ�_��������Ϣ.������
  is '������';
comment on column ƽ̨�ӿ�_��������Ϣ.����ʱ��
  is '����ʱ��';
comment on column ƽ̨�ӿ�_��������Ϣ.������
  is '������';
comment on column ƽ̨�ӿ�_��������Ϣ.����ʱ��
  is '����ʱ��';
comment on column ƽ̨�ӿ�_��������Ϣ.�����
  is '�����';
comment on column ƽ̨�ӿ�_��������Ϣ.��ע
  is '��ע';
comment on column ƽ̨�ӿ�_��������Ϣ.״̬
  is '0,�Ѱ󶨣�-1,�ѽ��1,��ɾ��';
alter table ƽ̨�ӿ�_��������Ϣ
  add primary key (��ˮ��)
  using index 
  tablespace USERS
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
prompt Creating table ƽ̨�ӿ�_ƽ̨��������
prompt ==========================
prompt
create table ƽ̨�ӿ�_ƽ̨��������
(
  ��ˮ��  NUMBER not null,
  ƽ̨��ʶ VARCHAR2(50) not null,
  ���ܱ��� VARCHAR2(50) not null,
  ҽԺ���� VARCHAR2(50) not null,
  ������  VARCHAR2(50) not null,
  ����ʱ�� DATE not null,
  ������  VARCHAR2(50),
  ����ʱ�� DATE,
  �����  INTEGER default 0 not null,
  ��ע   VARCHAR2(4000),
  ״̬   VARCHAR2(50) default 0 not null
)
tablespace USERS
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
comment on table ƽ̨�ӿ�_ƽ̨��������
  is 'ƽ̨�ӿ�_ƽ̨��������';
comment on column ƽ̨�ӿ�_ƽ̨��������.��ˮ��
  is '��ˮ��';
comment on column ƽ̨�ӿ�_ƽ̨��������.ƽ̨��ʶ
  is 'ƽ̨��ʶ';
comment on column ƽ̨�ӿ�_ƽ̨��������.���ܱ���
  is '���ܱ���';
comment on column ƽ̨�ӿ�_ƽ̨��������.ҽԺ����
  is 'ҽԺ����';
comment on column ƽ̨�ӿ�_ƽ̨��������.������
  is '������';
comment on column ƽ̨�ӿ�_ƽ̨��������.����ʱ��
  is '����ʱ��';
comment on column ƽ̨�ӿ�_ƽ̨��������.������
  is '������';
comment on column ƽ̨�ӿ�_ƽ̨��������.����ʱ��
  is '����ʱ��';
comment on column ƽ̨�ӿ�_ƽ̨��������.�����
  is '�����';
comment on column ƽ̨�ӿ�_ƽ̨��������.��ע
  is '��ע';
comment on column ƽ̨�ӿ�_ƽ̨��������.״̬
  is '0����Ч��-1��ͣ�ã�1��ɾ��';
alter table ƽ̨�ӿ�_ƽ̨��������
  add primary key (��ˮ��)
  using index 
  tablespace USERS
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
prompt Creating table ƽ̨�ӿ�_ƽ̨����
prompt ========================
prompt
create table ƽ̨�ӿ�_ƽ̨����
(
  ��ˮ��  NUMBER not null,
  ƽ̨��ʶ VARCHAR2(50) not null,
  ƽ̨���� VARCHAR2(200) not null,
  ��֤��Կ VARCHAR2(50) not null,
  ������  VARCHAR2(50) not null,
  ����ʱ�� DATE not null,
  ������  VARCHAR2(50),
  ����ʱ�� DATE,
  �����  INTEGER default 0 not null,
  ��ע   VARCHAR2(4000),
  ״̬   VARCHAR2(50) default 0 not null,
  ֧����ʽ VARCHAR2(50),
  �޺����� VARCHAR2(50)
)
tablespace USERS
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
comment on table ƽ̨�ӿ�_ƽ̨����
  is 'ƽ̨�ӿ�_ƽ̨����';
comment on column ƽ̨�ӿ�_ƽ̨����.��ˮ��
  is '��ˮ��';
comment on column ƽ̨�ӿ�_ƽ̨����.ƽ̨��ʶ
  is 'ƽ̨��ʶ';
comment on column ƽ̨�ӿ�_ƽ̨����.ƽ̨����
  is 'ƽ̨����';
comment on column ƽ̨�ӿ�_ƽ̨����.��֤��Կ
  is '��֤��Կ';
comment on column ƽ̨�ӿ�_ƽ̨����.������
  is '������';
comment on column ƽ̨�ӿ�_ƽ̨����.����ʱ��
  is '����ʱ��';
comment on column ƽ̨�ӿ�_ƽ̨����.������
  is '������';
comment on column ƽ̨�ӿ�_ƽ̨����.����ʱ��
  is '����ʱ��';
comment on column ƽ̨�ӿ�_ƽ̨����.�����
  is '�����';
comment on column ƽ̨�ӿ�_ƽ̨����.��ע
  is '��ע';
comment on column ƽ̨�ӿ�_ƽ̨����.״̬
  is '0����Ч��-1��ͣ�ã�1��ɾ��';
comment on column ƽ̨�ӿ�_ƽ̨����.֧����ʽ
  is '֧����ʽ';
comment on column ƽ̨�ӿ�_ƽ̨����.�޺�����
  is '�޺�����';
alter table ƽ̨�ӿ�_ƽ̨����
  add primary key (��ˮ��)
  using index 
  tablespace USERS
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
prompt Creating table ƽ̨�ӿ�_ϵͳ����
prompt ========================
prompt
create table ƽ̨�ӿ�_ϵͳ����
(
  ��ˮ��  NUMBER not null,
  ���ܱ��� VARCHAR2(50) not null,
  �������� VARCHAR2(200) not null,
  ����˵�� VARCHAR2(4000),
  ������  VARCHAR2(50) not null,
  ����ʱ�� DATE not null,
  ������  VARCHAR2(50),
  ����ʱ�� DATE,
  �����  INTEGER default 0 not null,
  ��ע   VARCHAR2(4000),
  ״̬   VARCHAR2(50) default 0 not null,
  �洢���� VARCHAR2(200)
)
tablespace USERS
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
comment on table ƽ̨�ӿ�_ϵͳ����
  is 'ƽ̨�ӿ�_ϵͳ����';
comment on column ƽ̨�ӿ�_ϵͳ����.��ˮ��
  is '��ˮ��';
comment on column ƽ̨�ӿ�_ϵͳ����.���ܱ���
  is '���ܱ���';
comment on column ƽ̨�ӿ�_ϵͳ����.��������
  is '��������';
comment on column ƽ̨�ӿ�_ϵͳ����.����˵��
  is '����˵��';
comment on column ƽ̨�ӿ�_ϵͳ����.������
  is '������';
comment on column ƽ̨�ӿ�_ϵͳ����.����ʱ��
  is '����ʱ��';
comment on column ƽ̨�ӿ�_ϵͳ����.������
  is '������';
comment on column ƽ̨�ӿ�_ϵͳ����.����ʱ��
  is '����ʱ��';
comment on column ƽ̨�ӿ�_ϵͳ����.�����
  is '�����';
comment on column ƽ̨�ӿ�_ϵͳ����.��ע
  is '��ע';
comment on column ƽ̨�ӿ�_ϵͳ����.״̬
  is '0����Ч��-1��ͣ�ã�1��ɾ��';
comment on column ƽ̨�ӿ�_ϵͳ����.�洢����
  is '�洢����';
create unique index U_ƽ̨�ӿ�_ϵͳ����_Ψһ�� on ƽ̨�ӿ�_ϵͳ���� (���ܱ���)
  tablespace USERS
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
alter table ƽ̨�ӿ�_ϵͳ����
  add primary key (��ˮ��)
  using index 
  tablespace USERS
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
prompt Creating sequence SEQ_ƽ̨�ӿ�_������־_��ˮ��
prompt ===================================
prompt
create sequence SEQ_ƽ̨�ӿ�_������־_��ˮ��
minvalue 1
maxvalue 9999999999
start with 8841
increment by 1
cache 10;

prompt
prompt Creating sequence SEQ_ƽ̨�ӿ�_����_������
prompt =================================
prompt
create sequence SEQ_ƽ̨�ӿ�_����_������
minvalue 1
maxvalue 9999999999
start with 1
increment by 1
cache 10;

prompt
prompt Creating sequence SEQ_ƽ̨�ӿ�_����_��ˮ��
prompt =================================
prompt
create sequence SEQ_ƽ̨�ӿ�_����_��ˮ��
minvalue 1
maxvalue 9999999999
start with 291
increment by 1
cache 10;

prompt
prompt Creating sequence SEQ_ƽ̨�ӿ�_������ϸ_��ˮ��
prompt ===================================
prompt
create sequence SEQ_ƽ̨�ӿ�_������ϸ_��ˮ��
minvalue 1
maxvalue 9999999999
start with 371
increment by 1
cache 10;

prompt
prompt Creating sequence SEQ_ƽ̨�ӿ�_��������Ϣ_��ˮ��
prompt ====================================
prompt
create sequence SEQ_ƽ̨�ӿ�_��������Ϣ_��ˮ��
minvalue 1
maxvalue 9999999999
start with 121
increment by 1
cache 10;

prompt
prompt Creating sequence SEQ_ƽ̨�ӿ�_ƽ̨����_��ˮ��
prompt ===================================
prompt
create sequence SEQ_ƽ̨�ӿ�_ƽ̨����_��ˮ��
minvalue 1
maxvalue 9999999999
start with 11
increment by 1
cache 10;

prompt
prompt Creating sequence SEQ_ƽ̨�ӿ�_ƽ̨����_��ˮ��
prompt ===================================
prompt
create sequence SEQ_ƽ̨�ӿ�_ƽ̨����_��ˮ��
minvalue 1
maxvalue 9999999999
start with 11
increment by 1
cache 10;

prompt
prompt Creating sequence SEQ_ƽ̨�ӿ�_ϵͳ����_��ˮ��
prompt ===================================
prompt
create sequence SEQ_ƽ̨�ӿ�_ϵͳ����_��ˮ��
minvalue 1
maxvalue 9999999999
start with 11
increment by 1
cache 10;

prompt
prompt Creating function FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ
prompt ================================
prompt
CREATE OR REPLACE FUNCTION FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(str_�ַ���   IN VARCHAR2,
                                        str_����ַ� IN VARCHAR2,
                                        int_λ��     IN NUMBER)
  RETURN VARCHAR2 IS
  RESULT   VARCHAR2(500);
  LL_START NUMBER(5);
  LL_END   NUMBER(5);
BEGIN

  IF int_λ�� < 1 THEN
    RESULT := '';
    RETURN(RESULT);
  END IF;

  IF int_λ�� = 1 THEN
    LL_START := 1;
    LL_END   := INSTR(str_�ַ���, str_����ַ�, 1, int_λ��);
    IF LL_END = 0 THEN
      RESULT := '';
    ELSE
      RESULT := SubStr(str_�ַ���, LL_START, LL_END - 1);
    END IF;
  ELSE
    LL_START := INSTR(str_�ַ���, str_����ַ�, 1, int_λ�� - 1);
    IF LL_START = 0 THEN
      RESULT := '';
    ELSE
      LL_END := INSTR(str_�ַ���, str_����ַ�, 1, int_λ��);
      IF LL_END = 0 THEN
        RESULT := SubStr(str_�ַ���, LL_START + LENGTH(str_����ַ�),
                         LENGTH(str_�ַ���) - LL_START);
      ELSE
        RESULT := SubStr(str_�ַ���, LL_START + LENGTH(str_����ַ�),
                         LL_END - (LL_START + LENGTH(str_����ַ�)));
      END IF;
    END IF;
  END IF;

  RETURN(RESULT);
END FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ;
/

prompt
prompt Creating function FU_ƽ̨�ӿ�_��֤��ֵ
prompt ==============================
prompt
CREATE OR REPLACE FUNCTION FU_ƽ̨�ӿ�_��֤��ֵ(p_string IN VARCHAR2)
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
    END FU_ƽ̨�ӿ�_��֤��ֵ;
/

prompt
prompt Creating function FU_ƽ̨�ӿ�_��֤����
prompt ==============================
prompt
CREATE OR REPLACE FUNCTION FU_ƽ̨�ӿ�_��֤����(p_date IN VARCHAR2) RETURN BOOLEAN IS
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
END FU_ƽ̨�ӿ�_��֤����;
/

prompt
prompt Creating function FU_ƽ̨�ӿ�_��֤���֤
prompt ===============================
prompt
CREATE OR REPLACE FUNCTION FU_ƽ̨�ӿ�_��֤���֤(P_IDCARD IN VARCHAR2)
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
    V_ISNUMBER := FU_ƽ̨�ӿ�_��֤��ֵ(P_IDCARD);
    IF NOT (V_ISNUMBER) THEN
      RETURN(-3);
    END IF;
  ELSIF V_LENGTH = 18 THEN
    V_ISNUMBER    := FU_ƽ̨�ӿ�_��֤��ֵ(P_IDCARD);
    V_ISNUMBER_17 := FU_ƽ̨�ӿ�_��֤��ֵ(SUBSTR(P_IDCARD, 1, 17));
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
  V_ISDATE := FU_ƽ̨�ӿ�_��֤����(V_DATE);
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
END FU_ƽ̨�ӿ�_��֤���֤;
/

prompt
prompt Creating function FU_ƽ̨�ӿ�_�⹹���֤
prompt ===============================
prompt
CREATE OR REPLACE FUNCTION FU_ƽ̨�ӿ�_�⹹���֤(STR_���֤�� IN VARCHAR2,

                                         DAT_�������� OUT DATE,
                                         STR_����     OUT VARCHAR2,
                                         STR_�Ա�     OUT VARCHAR2,
                                         STR_������Ϣ OUT VARCHAR2)
  RETURN INTEGER IS
BEGIN
  IF FU_ƽ̨�ӿ�_��֤���֤(STR_���֤��) <> 0 THEN
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
END FU_ƽ̨�ӿ�_�⹹���֤;
/

prompt
prompt Creating function FU_ƽ̨�ӿ�_ȡ��ƽ̨����
prompt ================================
prompt
CREATE OR REPLACE FUNCTION FU_ƽ̨�ӿ�_ȡ��ƽ̨����(STR_ƽ̨��ʶ IN VARCHAR2)
  RETURN VARCHAR2 IS
  STR_ƽ̨���� VARCHAR2(50);
BEGIN
  BEGIN
    SELECT NVL(ƽ̨����, '')
      INTO STR_ƽ̨����
      FROM ƽ̨�ӿ�_ƽ̨���� P
     WHERE P.ƽ̨��ʶ = STR_ƽ̨��ʶ;
    RETURN STR_ƽ̨����;
  EXCEPTION
    WHEN OTHERS THEN
      RETURN('');
  END;
END FU_ƽ̨�ӿ�_ȡ��ƽ̨����;
/

prompt
prompt Creating function FU_ƽ̨�ӿ�_��֤��������Ϣ
prompt =================================
prompt
CREATE OR REPLACE FUNCTION FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_������� IN VARCHAR2)
  RETURN INTEGER IS
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);
  STR_����ID     VARCHAR2(50);
  INT_��¼��     INTEGER;
BEGIN

  -- ��ʼֵ
  INT_��¼�� := 0;

  BEGIN

    IF STR_������� IS NULL THEN
      RETURN(-1);
    END IF;

    -- ��������⹹
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);
    STR_����ID     := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 6);

    -- ����ֵ�ж�
    IF STR_ƽ̨��ʶ IS NULL
       OR STR_��֤�ܳ� IS NULL
       OR STR_�ͻ��˱�ʶ IS NULL
       OR STR_���ܱ��� IS NULL
       OR STR_ҽԺ���� IS NULL
       OR STR_����ID IS NULL THEN
      RETURN(-1);
    END IF;

    -- ������Ч���ж�
    SELECT COUNT(1)
      INTO INT_��¼��
      FROM ƽ̨�ӿ�_��������Ϣ
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND �ͻ��˱�ʶ = STR_�ͻ��˱�ʶ
       AND ҽԺ���� = STR_ҽԺ����
       AND ����ID = STR_����ID
       AND ״̬ = 0;

    IF INT_��¼�� <= 0 THEN
      RETURN(-1);
    END IF;

    RETURN 0;

  EXCEPTION
    WHEN OTHERS THEN
      RETURN(-1);
  END;

END FU_ƽ̨�ӿ�_��֤��������Ϣ;
/

prompt
prompt Creating function FU_ƽ̨�ӿ�_��֤�ֻ���
prompt ===============================
prompt
CREATE OR REPLACE FUNCTION FU_ƽ̨�ӿ�_��֤�ֻ���(STR_�ֻ����� VARCHAR2) RETURN INTEGER IS
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

END FU_ƽ̨�ӿ�_��֤�ֻ���;
/

prompt
prompt Creating function FU_ƽ̨�ӿ�_��֤��������
prompt ================================
prompt
CREATE OR REPLACE FUNCTION FU_ƽ̨�ӿ�_��֤��������(STR_������� IN VARCHAR2)
  RETURN INTEGER IS
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);
  INT_��¼��     INTEGER;
BEGIN

  -- ��ʼֵ
  INT_��¼�� := 0;

  BEGIN

    IF STR_������� IS NULL THEN
      RETURN(-1);
    END IF;

    -- ��������⹹
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);

    -- ����ֵ�ж�
    IF STR_ƽ̨��ʶ IS NULL
       OR STR_��֤�ܳ� IS NULL
       OR STR_�ͻ��˱�ʶ IS NULL
       OR STR_���ܱ��� IS NULL
       OR STR_ҽԺ���� IS NULL THEN
      RETURN(-1);
    END IF;

    -- ������Ч���ж�
    SELECT COUNT(1)
      INTO INT_��¼��
      FROM ƽ̨�ӿ�_ƽ̨�������� G, ƽ̨�ӿ�_ƽ̨���� P
     WHERE G.ƽ̨��ʶ = P.ƽ̨��ʶ
       AND P.��֤��Կ = STR_��֤�ܳ�
       AND G.ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND G.���ܱ��� = STR_���ܱ���
       AND G.ҽԺ���� = STR_ҽԺ����
       AND G.״̬ = '0';

    IF INT_��¼�� <= 0 THEN
      RETURN(-1);
    END IF;

    RETURN 0;

  EXCEPTION
    WHEN OTHERS THEN
      RETURN(-1);
  END;

END FU_ƽ̨�ӿ�_��֤��������;
/

prompt
prompt Creating function FU_ƽ̨�ӿ�_��֤�Ա�
prompt ==============================
prompt
CREATE OR REPLACE FUNCTION FU_ƽ̨�ӿ�_��֤�Ա�(STR_�Ա� IN VARCHAR2) RETURN INTEGER IS
BEGIN
  IF STR_�Ա� IS NULL THEN
    RETURN(-1);
  END IF;
  IF STR_�Ա� = '1' --��
     OR STR_�Ա� = '2' THEN
    --Ů
    RETURN 0;
  ELSE
    RETURN(-1);
  END IF;
END FU_ƽ̨�ӿ�_��֤�Ա�;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_������־
prompt ===============================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_������־(Str_ƽ̨��ʶ   IN VARCHAR2,
                                         Str_�ͻ��˱�ʶ IN VARCHAR2,
                                         Str_ҽԺ����   IN VARCHAR2,
                                         Str_���ܱ���   IN VARCHAR2,
                                         Str_�������   IN VARCHAR2,
                                         Dat_����ʱ��   IN DATE,
                                         Str_������ IN VARCHAR2,
                                         Str_�������� IN VARCHAR2,
                                         Str_ִ����   IN VARCHAR2,
                                         Dat_ִ��ʱ�� IN DATE,
                                         Str_ִ��״̬ IN VARCHAR2) IS

  PRAGMA AUTONOMOUS_TRANSACTION; --�������ﲻӰ��������
BEGIN

  BEGIN
    INSERT INTO ƽ̨�ӿ�_������־
      (��ˮ��,
       ƽ̨��ʶ,
       �ͻ��˱�ʶ,
       ҽԺ����,
       ���ܱ���,
       �������,
       ����ʱ��,
       ������,
       ��������,
       ִ����,
       ִ��ʱ��,
       ִ��״̬)
    VALUES
      (seq_ƽ̨�ӿ�_������־_��ˮ��.nextval,
       Str_ƽ̨��ʶ,
       Str_�ͻ��˱�ʶ,
       Str_ҽԺ����,
       Str_���ܱ���,
       Str_�������,
       Dat_����ʱ��,
       Str_������,
       Str_��������,
       Str_ִ����,
       Dat_ִ��ʱ��,
       Str_ִ��״̬);

  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;
      RETURN;
  END;

  COMMIT;

  RETURN;

END PR_ƽ̨�ӿ�_������־;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_���˽ɷѼ�¼��ѯ
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_���˽ɷѼ�¼��ѯ(STR_�������   IN VARCHAR2,
                                             CUR_���ؽ���� OUT SYS_REFCURSOR,
                                             INT_����ֵ     OUT INTEGER,
                                             STR_������Ϣ   OUT VARCHAR2) IS
  -- �̶�����
  DAT_ϵͳʱ�� DATE;

  -- �������
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID   VARCHAR2(50);
  STR_������   VARCHAR2(50);
  STR_����״̬ VARCHAR2(50);

BEGIN
  BEGIN
  
    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;
  
    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ϣ��Ч��';
      GOTO �˳�;
    END IF;
  
    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    -- ���������������
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);
  
    -- �����ܲ���������
    STR_����ID   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_������   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 7);
    STR_����״̬ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 8);
  
    -- ������֤
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;
  
    IF STR_������ IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ķ����ţ�';
      GOTO �˳�;
    END IF;
  
    IF STR_����״̬ IS NULL
       OR STR_����״̬ NOT IN ('ȫ��', '��ȡ��', '��֧��', '��֧��') THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�ľ���״̬��';
      GOTO �˳�;
    END IF;
  
    /*
    ˵����
        1��ֻ��ʾ1���ڵĶ�����Ϣ
    */
    OPEN CUR_���ؽ���� FOR
      SELECT ����ID     AS ����ID,
             ���ﲡ���� AS ���ﲡ����,
             ��������   AS ��������,
             ������     AS ������,
             ʵ�ս��   AS �����ܶ�,
             ����״̬   AS ����״̬,
             ����ʱ��   AS ����ʱ��,
             ƽ̨����ʱ��  AS ֧��ʱ��
        FROM ƽ̨�ӿ�_����
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND �ͻ��˱�ʶ = STR_�ͻ��˱�ʶ
         AND ҽԺ���� = STR_ҽԺ����
         AND ����ID = STR_����ID
         AND ������ = DECODE(STR_������, '-1', ������, STR_������)
         AND ����״̬ = DECODE(STR_����״̬, 'ȫ��', ����״̬, STR_����״̬)
         AND ����ʱ�� > ADD_MONTHS(SYSDATE, -1);
  
    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�!';
  
    -- ��������־��
    PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
                 STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
                 STR_ҽԺ����   => STR_ҽԺ����,
                 STR_���ܱ���   => STR_���ܱ���,
                 STR_�������   => STR_�������,
                 DAT_����ʱ��   => DAT_ϵͳʱ��,
                 STR_������   => STR_������Ϣ,
                 STR_��������   => STR_������,
                 STR_ִ����     => STR_ƽ̨��ʶ,
                 DAT_ִ��ʱ��   => SYSDATE,
                 STR_ִ��״̬   => INT_����ֵ);
  
    RETURN;
  
  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  RETURN;

END PR_ƽ̨�ӿ�_���˽ɷѼ�¼��ѯ;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_���˽ɷ���ϸ��ѯ
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_���˽ɷ���ϸ��ѯ(STR_�������   IN VARCHAR2,
                                             CUR_���ؽ���� OUT SYS_REFCURSOR,
                                             INT_����ֵ     OUT INTEGER,
                                             STR_������Ϣ   OUT VARCHAR2) IS
  -- �̶�����
  DAT_ϵͳʱ�� DATE;

  -- �������
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID VARCHAR2(50);
  STR_������ VARCHAR2(50);

BEGIN
  BEGIN
  
    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;
  
    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ϣ��Ч��';
      GOTO �˳�;
    END IF;
  
    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    -- ���������������
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);
  
    -- �����ܲ���������
    STR_����ID := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_������ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 7);
  
    -- ������֤
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;
  
    IF STR_������ IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ķ����ţ�';
      GOTO �˳�;
    END IF;
  
    /*
    ˵����
        1��ֻ��ʾ1���ڵĶ�����Ϣ
    */
    OPEN CUR_���ؽ���� FOR
      SELECT D.����ID   AS ����ID,
             M.������   AS ������,
             M.��Ŀ���� AS ��Ŀ����,
             M.��Ŀ���� AS ��Ŀ����,
             M.���     AS ���,
             M.����     AS ����,
             M.��λ     AS ��λ,
             M.����     AS ����,
             M.�ܽ��   AS �ܽ��
        FROM ƽ̨�ӿ�_���� D, ƽ̨�ӿ�_������ϸ M
       WHERE D.������ = M.������
         AND D.ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND D.�ͻ��˱�ʶ = STR_�ͻ��˱�ʶ
         AND D.ҽԺ���� = STR_ҽԺ����
         AND D.����ID = STR_����ID
         AND D.������ = DECODE(STR_������, '-1', D.������, STR_������)
         AND D.����ʱ�� > ADD_MONTHS(SYSDATE, -1);
  
    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�!';
  
    -- ��������־��
    PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
                 STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
                 STR_ҽԺ����   => STR_ҽԺ����,
                 STR_���ܱ���   => STR_���ܱ���,
                 STR_�������   => STR_�������,
                 DAT_����ʱ��   => DAT_ϵͳʱ��,
                 STR_������   => STR_������Ϣ,
                 STR_��������   => STR_������,
                 STR_ִ����     => STR_ƽ̨��ʶ,
                 DAT_ִ��ʱ��   => SYSDATE,
                 STR_ִ��״̬   => INT_����ֵ);
  
    RETURN;
  
  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  RETURN;

END PR_ƽ̨�ӿ�_���˽ɷ���ϸ��ѯ;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_����_ҽ������
prompt ==================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_����_ҽ������(STR_�������   IN VARCHAR2,
                                            CUR_���ؽ���� OUT SYS_REFCURSOR,
                                            INT_����ֵ     OUT INTEGER,
                                            STR_������Ϣ   OUT VARCHAR2) IS
  ----4499|123456|wx000000000010|1004|522633020000001|0000000159|             �ҺŲ�����ҽ��
  ----4499|123456|wx000000000010|1004|522633020000001|0000000159|2021028657   ֻ����ҽ��
  STR_ƽ̨���� VARCHAR2(50);
  DAT_ϵͳʱ�� DATE;

  -- �̶�����
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID     VARCHAR2(50);
  STR_���ﲡ���� VARCHAR2(50);

  -- �������
  ----������Ϣ
  STR_����     VARCHAR2(50);
  STR_�Ա�     VARCHAR2(50);
  STR_�������� VARCHAR2(50);
  STR_����״�� VARCHAR2(50);
  STR_��ϵ�绰 VARCHAR2(50);
  STR_��ͥ��ַ VARCHAR2(200);
  STR_������λ VARCHAR2(200);
  STR_���֤�� VARCHAR2(50);

  ----�Һ����
  STR_�Һ����     VARCHAR2(50);
  STR_�Һŵ���     VARCHAR2(50);
  STR_�Һ����ͱ��� VARCHAR2(50);
  STR_�Һ��������� VARCHAR2(50);
  NUM_�Һŷ�       NUMBER(18, 3);
  NUM_����       NUMBER(18, 3);
  NUM_�ܷ���       NUMBER(18, 3);
  STR_�������     VARCHAR2(50);
  STR_�������ͱ��� VARCHAR2(50);
  STR_������������ VARCHAR2(50);
  STR_����״̬     VARCHAR2(50);
  STR_�Һ���Դ     VARCHAR2(50);
  STR_���ʽ     VARCHAR2(50);

  ----ҽ����Ŀ���
  STR_�������     VARCHAR2(50);
  STR_С�����     VARCHAR2(50);
  STR_��Ŀ����     VARCHAR2(50);
  STR_��Ŀ����     VARCHAR2(100);
  STR_��λ����     VARCHAR2(50);
  STR_��λ����     VARCHAR2(50);
  STR_ִ�п��ұ��� VARCHAR2(50);
  STR_ҽ����       VARCHAR2(50);
  STR_��ĿID       VARCHAR2(50);
  STR_���         VARCHAR2(50);
  STR_���뵥ID     VARCHAR2(50);

  TYPE REF_CURSOR_TYPE IS REF CURSOR;
  CUR_��ʱ���� REF_CURSOR_TYPE;
  STR_SQL      VARCHAR2(1000);

BEGIN

  -- ����������Ч����֤��
  IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '�Ƿ�����';
    GOTO �˳�;
  END IF;

  -- ����������Ч����֤��
  IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ϣ��Ч��';
    GOTO �˳�;
  END IF;

  -- �����ݳ�ʼ����
  SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;

  STR_�������ͱ��� := '1';
  STR_������������ := '�ֽ�';
  STR_����״̬     := '���ڽ���';
  STR_�Һ���Դ     := 'ԤԼ';

  -- ���̶�����������
  STR_ƽ̨��ʶ   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 1);
  STR_��֤�ܳ�   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 2);
  STR_�ͻ��˱�ʶ := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 3);
  STR_���ܱ���   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 4);
  STR_ҽԺ����   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 5);

  -- �����ܲ���������
  STR_����ID     := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 6);
  STR_���ﲡ���� := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 7);
  BEGIN
  
    BEGIN
      SELECT P.֧����ʽ, P.ƽ̨����
        INTO STR_���ʽ, STR_ƽ̨����
        FROM ƽ̨�ӿ�_ƽ̨���� P
       WHERE P.ƽ̨��ʶ = STR_ƽ̨��ʶ;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'δ�ҵ���Ч��ƽ̨��Ϣ��';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
  
    -- �������������֤��
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;
  
    --����ȡ�Һŷ��������Ϣ��
    BEGIN
      SELECT ���ͱ���, ��������, �Һŷ�, ����, �Һŷ� + ����, �������
        INTO STR_�Һ����ͱ���,
             STR_�Һ���������,
             NUM_�Һŷ�,
             NUM_����,
             NUM_�ܷ���,
             STR_�������
        FROM ������Ŀ_�Һ�����
       WHERE �������� = STR_ҽԺ����
         AND ���ͱ��� = '000002' --��Ѻ�
         AND ��Ч״̬ = '��Ч'
         AND ɾ����־ = '0';
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'δ�ҵ���Ч�ĹҺ����ͣ�';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
  
    --����ȡ������Ϣ��
    BEGIN
      SELECT ����,
             �Ա�,
             ��������,
             ����״��,
             �ֻ�����,
             ��ͥ��ַ,
             ������λ,
             ���֤��
        INTO STR_����,
             STR_�Ա�,
             STR_��������,
             STR_����״��,
             STR_��ϵ�绰,
             STR_��ͥ��ַ,
             STR_������λ,
             STR_���֤��
        FROM ������Ŀ_������Ϣ
       WHERE �������� = STR_ҽԺ����
         AND ����ID = STR_����ID;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'δ�ҵ���Ч�Ĳ�����Ϣ��';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
    if STR_���ﲡ���� is null then
      -- ���������ݡ�
      ---- ���������ﲡ���š�
      PR_����_ȡ��ҵ������(STR_��������   => STR_ҽԺ����,
                    STR_���������� => '���ﲡ����',
                    STR_���ز����� => STR_���ﲡ����,
                    INT_����ֵ     => INT_����ֵ,
                    STR_������Ϣ   => STR_������Ϣ);
      IF INT_����ֵ <> 1 THEN
        STR_������Ϣ := '�������ﲡ����ʧ��,ԭ��:' + STR_������Ϣ;
        GOTO �˳�;
      END IF;
    
      ---- �������Һ���š�
      PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '26',
                  PRM_��������   => STR_ҽԺ����,
                  PRM_��������   => '1',
                  PRM_����Ψһ�� => STR_�Һ����,
                  PRM_ִ�н��   => INT_����ֵ,
                  PRM_������Ϣ   => STR_������Ϣ);
      IF INT_����ֵ <> 0 THEN
        STR_������Ϣ := '�����Һ����ʧ��!';
        GOTO �˳�;
      END IF;
      ---- �������Һŵ��š�
      SELECT FU_����_��ȡ��ǰƱ�ݺ�(STR_ҽԺ����, STR_ƽ̨��ʶ, '4')
        INTO STR_�Һŵ���
        FROM DUAL;
    
      IF STR_�Һŵ��� = '�뵽����������Ʊ��' THEN
        STR_������Ϣ := '�ò���Ա�޹Һŵ���,��֪ͨ����������Ʊ��!';
        GOTO �˳�;
      END IF;
    
      -- �����ɹҺż�¼�� 
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
           ��֧�����)
        VALUES
          (STR_ҽԺ����,
           STR_����ID,
           STR_���ﲡ����,
           STR_�Һ����,
           STR_�Һŵ���,
           '', --�Һſ��ұ���
           NULL,
           STR_ƽ̨��ʶ,
           STR_�Һ����ͱ���,
           STR_ƽ̨��ʶ,
           DAT_ϵͳʱ��,
           '��',
           STR_�������,
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
           '', --�Һſ��ұ���
           STR_ƽ̨��ʶ,
           0,
           0,
           '-1',
           0);
      
        INT_����ֵ := SQL%ROWCOUNT;
        IF INT_����ֵ = 0 THEN
          INT_����ֵ   := -1;
          STR_������Ϣ := '����Һ�����ʧ�ܣ�';
          GOTO �˳�;
        END IF;
      
        -- ��������֧���¼��
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
          (STR_ҽԺ����,
           STR_�Һŵ���,
           NUM_�ܷ���,
           STR_���ʽ,
           '�Һ�',
           STR_ƽ̨��ʶ,
           STR_ƽ̨����,
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
      
      EXCEPTION
        WHEN OTHERS THEN
          INT_����ֵ   := -1;
          STR_������Ϣ := STR_������Ϣ || SQLERRM;
          GOTO �˳�;
      END;
    else
      begin
        select g.�Һ����
          into STR_�Һ����
          from �������_�ҺŵǼ� g
         where g.�������� = STR_ҽԺ����
           and g.���ﲡ���� = STR_���ﲡ����;
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
          INT_����ֵ   := -1;
          STR_������Ϣ := 'δ�ҵ���Ч�ĹҺ���Ϣ��';
          GOTO �˳�;
        WHEN OTHERS THEN
          INT_����ֵ   := -1;
          STR_������Ϣ := 'ϵͳ�쳣1��' || SQLERRM;
          GOTO �˳�;
      END;
    end if;
  
    update �������_����ҽ��
       set �շ�״̬ = '�������շ�'
     where �������� = STR_ҽԺ����
       and ����ID = STR_����ID
       and ���ﲡ���� = STR_���ﲡ����;
  
    ---- ������ҽ���š�
    PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '8',
                PRM_��������   => STR_ҽԺ����,
                PRM_��������   => '1',
                PRM_����Ψһ�� => STR_ҽ����,
                PRM_ִ�н��   => INT_����ֵ,
                PRM_������Ϣ   => STR_������Ϣ);
    IF INT_����ֵ <> 0 THEN
      STR_������Ϣ := '����ҽ����ʧ��!';
      GOTO �˳�;
    END IF;
  
    STR_SQL := 'SELECT �������,
             С�����,
             ��λ����,
             ��λ����,
             ��Ŀ����,
             ��Ŀ����,
             ����ִ�п��ұ���,
             �������,
             ����� FROM ������Ŀ_�����ֵ�
       WHERE �������� = ''' || STR_ҽԺ���� || '''
         AND ��Ŀ���� in ( ''260000002*'',''31070100105'')';
    OPEN CUR_��ʱ���� FOR STR_SQL;
  
    LOOP
      FETCH CUR_��ʱ����
        INTO STR_�������,
             STR_С�����,
             STR_��λ����,
             STR_��λ����,
             STR_��Ŀ����,
             STR_��Ŀ����,
             STR_ִ�п��ұ���,
             STR_�������,
             NUM_�ܷ���;
      EXIT WHEN CUR_��ʱ����%NOTFOUND;
    
      ---- ���������뵥ID��
      PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '33',
                  PRM_��������   => STR_ҽԺ����,
                  PRM_��������   => '1',
                  PRM_����Ψһ�� => STR_���뵥ID,
                  PRM_ִ�н��   => INT_����ֵ,
                  PRM_������Ϣ   => STR_������Ϣ);
      IF INT_����ֵ <> 0 THEN
        STR_������Ϣ := '�������뵥IDʧ��!';
        GOTO �˳�;
      END IF;
    
      ----��������ĿID��
      SELECT SYS_GUID() INTO STR_��ĿID FROM DUAL;
    
      ----��������š�
      SELECT SEQ_����ҽ��_���.NEXTVAL INTO STR_��� FROM DUAL;
    
      --����������ҽ����
      INSERT INTO �������_����ҽ��
        (��������,
         ���,
         ����ID,
         ���ﲡ����,
         ҽ����,
         �������ұ���,
         ������ұ���,
         ִ�п��ұ���,
         ���˿��ұ���,
         ����ҽ������,
         ����Ա����,
         ����Ա����,
         ¼��ʱ��,
         �������,
         С�����,
         ��Ŀ����,
         ��Ŀ����,
         ���,
         ����,
         ��������,
         ��������,
         ����,
         ����,
         ��λ����,
         ��λ����,
         �÷�����,
         �÷�����,
         Ƶ�ʱ���,
         Ƶ������,
         ��ʼʱ��,
         ����,
         ҽ��״̬,
         �����,
         �������,
         ��������ID,
         �Һ����,
         �������,
         ����,
         Ƥ�Ա�־,
         �շ�״̬,
         ���۷�ʽ,
         �ܽ��,
         ����,
         ��ĿID,
         ҽ������,
         ҽ������)
      VALUES
        (STR_ҽԺ����,
         STR_���,
         STR_����ID,
         STR_���ﲡ����,
         STR_ҽ����,
         '', --�������ұ���
         '', --������ұ���
         STR_ִ�п��ұ���,
         '', --���˿��ұ���
         STR_ƽ̨��ʶ,
         STR_ƽ̨��ʶ,
         STR_ƽ̨����,
         SYSDATE,
         STR_�������,
         STR_С�����,
         STR_��Ŀ����,
         STR_��Ŀ����,
         SEQ_����ҽ��_���.NEXTVAL,
         1,
         '37',
         '��',
         1,
         1,
         STR_��λ����,
         STR_��λ����,
         '0000000002',
         '����',
         '1003',
         'һ����',
         SYSDATE,
         '��',
         '��Ч',
         1,
         1,
         STR_���뵥ID,
         STR_�Һ����,
         SEQ_����ҽ��_�������.NEXTVAL,
         1,
         '-1',
         '����δ�շ�',
         'ҽ������',
         NUM_�ܷ���,
         NUM_�ܷ���,
         STR_��ĿID,
         STR_��Ŀ����,
         '0000003920');
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '��������ҽ��ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      --����������ҽ����Ŀ��
      INSERT INTO �������_����ҽ����Ŀ
        (��������,
         ����ID,
         ���ﲡ����,
         ҽ����,
         ��ĿID,
         �������,
         С�����,
         ��Ŀ����,
         ��Ŀ����,
         �������,
         ����,
         ��������,
         ��������,
         ����,
         ����,
         ����,
         �ܽ��,
         ��λ����,
         ��λ����,
         �÷�����,
         �÷�����,
         ִ�п��ұ���,
         ����ʱ��,
         ��λ����,
         С��λ����,
         �Ƽ�ID,
         ���۷�ʽ,
         ����Ա����,
         ����ҽ������,
         �������ұ���,
         ���,
         �������)
      VALUES
        (STR_ҽԺ����,
         STR_����ID,
         STR_���ﲡ����,
         STR_ҽ����,
         STR_��ĿID,
         STR_�������,
         STR_С�����,
         STR_��Ŀ����,
         STR_��Ŀ����,
         1,
         1,
         '37',
         '��',
         1,
         1,
         NUM_�ܷ���,
         NUM_�ܷ���,
         STR_��λ����,
         STR_��λ����,
         '0000000002',
         '����',
         STR_ִ�п��ұ���,
         SYSDATE,
         0,
         NUM_�ܷ���,
         SYS_GUID(),
         'ҽ������',
         STR_ƽ̨��ʶ,
         STR_ƽ̨��ʶ,
         '', --�������ұ���
         STR_���,
         STR_�������);
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '��������ҽ����Ŀʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      --�����ɼ�����_���롿
      INSERT INTO ������_����
        (��������,
         ���뵥ID,
         ��Ŀ����,
         ��Ŀ����,
         ִ�п��ұ���,
         ҽ������,
         ����ʱ��,
         ���״̬,
         ΨһID,
         ID����,
         ҽ����,
         ����ID,
         ������,
         �Һ����,
         �ײ�����,
         ����)
      VALUES
        (STR_ҽԺ����,
         STR_���뵥ID,
         STR_��Ŀ����,
         STR_��Ŀ����,
         STR_ִ�п��ұ���,
         STR_ƽ̨��ʶ,
         SYSDATE,
         'δ����',
         SYS_GUID(),
         '����',
         STR_ҽ����,
         STR_����ID,
         STR_���ﲡ����,
         STR_�Һ����,
         1,
         '����');
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '�������������ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
     
    
    END LOOP;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  INT_����ֵ   := 0;
  STR_������Ϣ := '����ɹ�!' || STR_���ﲡ����;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  COMMIT;
  RETURN;

  --���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  --��������־��
  /*PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
  STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
  STR_ҽԺ����   => STR_ҽԺ����,
  STR_���ܱ���   => STR_���ܱ���,
  STR_�������   => STR_�������,
  DAT_����ʱ��   => DAT_ϵͳʱ��,
  STR_������   => STR_������Ϣ,
  STR_��������   => NULL,
  STR_ִ����     => STR_ƽ̨��ʶ,
  DAT_ִ��ʱ��   => SYSDATE,
  STR_ִ��״̬   => INT_����ֵ);*/
  ROLLBACK;
  RETURN;

END PR_ƽ̨�ӿ�_����_ҽ������;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_��鱨����
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_��鱨����(STR_�������   IN VARCHAR2,

                                           CUR_���ؽ���� OUT SYS_REFCURSOR,
                                           INT_����ֵ     OUT INTEGER,
                                           STR_������Ϣ   OUT VARCHAR2) IS
  -- �̶�����
  DAT_ϵͳʱ�� DATE;

  -- �������
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID   VARCHAR2(50);
  STR_������   VARCHAR2(50);
  STR_���浥�� VARCHAR2(50);

BEGIN
  BEGIN

    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;

    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ϣ��Ч��';
      GOTO �˳�;
    END IF;

    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;

    -- ���������������
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);

    -- �����ܲ���������
    STR_����ID   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_������   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 7);
    STR_���浥�� := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 8);

    -- ������֤
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;

    IF STR_������ IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ����ţ�';
      GOTO �˳�;
    END IF;

    IF STR_���浥�� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�ı��浥�ţ�';
      GOTO �˳�;
    END IF;

    /*
    ˵����
        1��ֻ��ʾ6�����ڵļ�鱨��
    */
    OPEN CUR_���ؽ���� FOR
      SELECT S.����ID AS ����ID,
             S.������ AS ������,
             J.���浥�� AS ���浥��,
             J.���ֱ������� AS �������,
             '' AS ������,
             NVL((SELECT ��Ա����
                   FROM ������Ŀ_��Ա���� Z
                  WHERE Z.�������� = J.��������
                    AND Z.��Ա���� = J.����ҽ������),
                 '') AS ����ҽ��,
             J.����ʱ�� AS ����ʱ��
        FROM ������_���� S, ������_��� J
       WHERE S.�������� = J.��������
         AND S.���뵥ID = J.���뵥ID
         AND S.�������� = STR_ҽԺ����
         AND S.����ID = STR_����ID
         AND S.������ = STR_������
         AND J.���浥�� = STR_���浥��
         AND S.����ʱ�� > ADD_MONTHS(SYSDATE, -6);

    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�!';

    -- ��������־��
    PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
                 STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
                 STR_ҽԺ����   => STR_ҽԺ����,
                 STR_���ܱ���   => STR_���ܱ���,
                 STR_�������   => STR_�������,
                 DAT_����ʱ��   => DAT_ϵͳʱ��,
                 STR_������   => STR_������Ϣ,
                 STR_��������   => STR_���浥��,
                 STR_ִ����     => STR_ƽ̨��ʶ,
                 DAT_ִ��ʱ��   => SYSDATE,
                 STR_ִ��״̬   => INT_����ֵ);

    RETURN;

  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  RETURN;

END PR_ƽ̨�ӿ�_��鱨����;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_���鱨����
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_���鱨����(STR_�������   IN VARCHAR2,
                                           CUR_���ؽ���� OUT SYS_REFCURSOR,
                                           INT_����ֵ     OUT INTEGER,
                                           STR_������Ϣ   OUT VARCHAR2) IS
  -- �̶�����
  DAT_ϵͳʱ�� DATE;

  -- �������
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID   VARCHAR2(50);
  STR_������   VARCHAR2(50);
  STR_���浥�� VARCHAR2(50);

BEGIN
  BEGIN
  
    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;
  
    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ϣ��Ч��';
      GOTO �˳�;
    END IF;
  
    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    -- ���������������
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);
  
    -- �����ܲ���������
    STR_����ID   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_������   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 7);
    STR_���浥�� := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 8);
  
    -- ������֤
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;
  
    IF STR_������ IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ����ţ�';
      GOTO �˳�;
    END IF;
  
    IF STR_���浥�� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�ı��浥�ţ�';
      GOTO �˳�;
    END IF;
  
    /*
    ˵����
        1��ֻ��ʾ6�����ڵļ��鱨��
    */
    OPEN CUR_���ؽ���� FOR
      SELECT S.����ID AS ����ID,
             S.������ AS ������,
             J.���浥�� AS ���浥��,
             M.��� AS ˳���,
             M.ϸ������ AS ������Ŀ,
             M.ϸ����� AS ��Ŀ����,
             M.ϸ��ֵ AS ������,
             M.��λ AS ��λ,
             M.�ο�ֵ���� AS �ο���Χ,
             M.���� AS �������,
             NVL((SELECT ��Ա����
                   FROM ������Ŀ_��Ա���� Z
                  WHERE Z.�������� = J.��������
                    AND Z.��Ա���� = J.����ҽ������),
                 '') AS ����ҽ��,
             J.����ʱ�� AS ����ʱ��
        FROM ������_���� S, ������_��� J, ������_���_��ϸ M
       WHERE S.�������� = J.��������
         AND S.���뵥ID = J.���뵥ID
         AND S.�������� = M.��������
         AND J.���浥�� = M.���浥ID
         AND S.�������� = STR_ҽԺ����
         AND S.����ID = STR_����ID
         AND S.������ = STR_������
         AND J.���浥�� = STR_���浥��
         AND S.���״̬ = '�ѱ���'
         AND S.����ʱ�� > ADD_MONTHS(SYSDATE, -6);
  
    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�!';
  
    -- ��������־��
    PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
                 STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
                 STR_ҽԺ����   => STR_ҽԺ����,
                 STR_���ܱ���   => STR_���ܱ���,
                 STR_�������   => STR_�������,
                 DAT_����ʱ��   => DAT_ϵͳʱ��,
                 STR_������   => STR_������Ϣ,
                 STR_��������   => STR_���浥��,
                 STR_ִ����     => STR_ƽ̨��ʶ,
                 DAT_ִ��ʱ��   => SYSDATE,
                 STR_ִ��״̬   => INT_����ֵ);
  
    RETURN;
  
  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  RETURN;

END PR_ƽ̨�ӿ�_���鱨����;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_�����鱨���б�
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_�����鱨���б�(STR_�������   IN VARCHAR2,
                                             CUR_���ؽ���� OUT SYS_REFCURSOR,
                                             INT_����ֵ     OUT INTEGER,
                                             STR_������Ϣ   OUT VARCHAR2) IS
  -- �̶�����
  DAT_ϵͳʱ�� DATE;

  -- �������
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID   VARCHAR2(50);
  STR_������   VARCHAR2(50);
  STR_������Դ VARCHAR2(50);
  STR_�������� VARCHAR2(50);

BEGIN
  BEGIN

    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;

    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ϣ��Ч��';
      GOTO �˳�;
    END IF;

    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;

    -- ���������������
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);

    -- �����ܲ���������
    STR_����ID   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_������   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 7);
    STR_������Դ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 8);
    STR_�������� := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 9);

    -- ������֤
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;

    IF STR_������ IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ����ţ�';
      GOTO �˳�;
    END IF;

    IF STR_������Դ IS NULL
       OR (STR_������Դ <> '����' AND STR_������Դ <> 'סԺ') THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�ı�����Դ��';
      GOTO �˳�;
    END IF;

    IF STR_�������� IS NULL
       OR (STR_�������� <> '����' AND STR_�������� <> '���') THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�ı������ͣ�';
      GOTO �˳�;
    END IF;

    /*
    ˵����
        1��ֻ��ʾ6�����ڵļ���/�������
        2��ֻ��ʾ�ѱ��������
        3�����ر�����
    */
    OPEN CUR_���ؽ���� FOR
      SELECT S.����ID   AS ����ID,
             S.������   AS ������,
             J.���浥�� AS ���浥��,
             S.��Ŀ���� AS ��Ŀ����,
             J.����ʱ�� AS ����ʱ��
        FROM ������_���� S, ������_��� J
       WHERE S.�������� = J.��������
         AND S.���뵥ID = J.���뵥ID
         AND S.�������� = STR_ҽԺ����
         AND S.����ID = STR_����ID
         AND S.������ = DECODE(STR_������, '-1', S.������, STR_������)
         AND S.ID���� = STR_������Դ
         AND S.���� = STR_��������
         AND S.���״̬ = '�ѱ���'
         AND S.����ʱ�� > ADD_MONTHS(SYSDATE, -6);

    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�!';

    -- ��������־��
    PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
                 STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
                 STR_ҽԺ����   => STR_ҽԺ����,
                 STR_���ܱ���   => STR_���ܱ���,
                 STR_�������   => STR_�������,
                 DAT_����ʱ��   => DAT_ϵͳʱ��,
                 STR_������   => STR_������Ϣ,
                 STR_��������   => NULL,
                 STR_ִ����     => STR_ƽ̨��ʶ,
                 DAT_ִ��ʱ��   => SYSDATE,
                 STR_ִ��״̬   => INT_����ֵ);

    RETURN;

  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  RETURN;

END PR_ƽ̨�ӿ�_�����鱨���б�;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_�����˲�ѯ
prompt ================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_�����˲�ѯ(STR_�������   IN VARCHAR2,
                                          CUR_���ؽ���� OUT SYS_REFCURSOR,
                                          INT_����ֵ     OUT INTEGER,
                                          STR_������Ϣ   OUT VARCHAR2) IS
  -- �̶�����
  DAT_ϵͳʱ�� DATE;

  --�������
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

BEGIN
  BEGIN

    -- ������֤
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;

    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;

    -- ���������������
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);

    /*
    ˵����
        1��ֻ��ʾ��Ч�ľ������б�
    */
    OPEN CUR_���ؽ���� FOR
      SELECT ����ID,
             ���������,
             ����,
             �Ա�,
             ��������,
             ���֤��,
             �ֻ�����,
             ��ϵ��ַ,
             �໤������,
             �໤�����֤��,
             �໤���ֻ�����,
             �໤����ϵ��ַ,
             ����ʱ�� AS ע��ʱ��
        FROM ƽ̨�ӿ�_��������Ϣ J
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND �ͻ��˱�ʶ = STR_�ͻ��˱�ʶ
         AND ҽԺ���� = STR_ҽԺ����
         AND ״̬ = 0;

    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�!';

    -- ��������־��
    PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
                 STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
                 STR_ҽԺ����   => STR_ҽԺ����,
                 STR_���ܱ���   => STR_���ܱ���,
                 STR_�������   => STR_�������,
                 DAT_����ʱ��   => DAT_ϵͳʱ��,
                 STR_������   => STR_������Ϣ,
                 STR_��������   => NULL,
                 STR_ִ����     => STR_ƽ̨��ʶ,
                 DAT_ִ��ʱ��   => SYSDATE,
                 STR_ִ��״̬   => INT_����ֵ);

    RETURN;

  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  RETURN;

END PR_ƽ̨�ӿ�_�����˲�ѯ;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_������ע��
prompt ================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_������ע��(STR_�������   IN VARCHAR2,
                                          CUR_���ؽ���� OUT SYS_REFCURSOR,
                                          INT_����ֵ     OUT INTEGER,
                                          STR_������Ϣ   OUT VARCHAR2) IS
  -- �̶�����
  DAT_ϵͳʱ�� DATE;

  --�������
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ҵ�����
  STR_���������     VARCHAR2(50);
  STR_����ID         VARCHAR2(50);
  STR_����           VARCHAR2(50);
  STR_�Ա�           VARCHAR2(50);
  STR_��������       VARCHAR2(50);
  DAT_��������       DATE;
  STR_����           VARCHAR2(50);
  STR_�ֻ�����       VARCHAR2(50);
  STR_���֤��       VARCHAR2(50);
  STR_��ϵ��ַ       VARCHAR2(100);
  STR_�໤������     VARCHAR2(50);
  STR_�໤�����֤�� VARCHAR2(50);
  STR_�໤���ֻ����� VARCHAR2(50);
  STR_�໤����ϵ��ַ VARCHAR2(100);

BEGIN
  BEGIN
  
    -- ������֤
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�������ȷ��ƽ̨��ʶ����֤��Կ��ҽԺ���뼰���ܱ����Ƿ���ȷ��';
      GOTO �˳�;
    END IF;
  
    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    -- ���������������
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);
  
    --�����ܶ��岿�֡�
    STR_���������     := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_����           := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 7);
    STR_�Ա�           := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 8);
    STR_��������       := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 9);
    STR_���֤��       := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 10);
    STR_�ֻ�����       := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 11);
    STR_��ϵ��ַ       := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 12);
    STR_�໤������     := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 13);
    STR_�໤�����֤�� := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 14);
    STR_�໤���ֻ����� := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 15);
    STR_�໤����ϵ��ַ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 16);
  
    /*
    �������̣�
        1���ж������Ƿ��ǺϷ�����
        2�������������
        3�����������Ч���ж�
        4����������Ϊ���˻�����ʱ�����֤�������������֤�����Ա�/��������
        5����������Ϊ��Ů��ʱ�����֤�ɲ��������д�����֤���ƶ������Ա�/�������ڣ�
           ���δ���������д�Ա�/����/��������/�໤����Ϣ
        6����ѯ���֤�Ƿ��Ѿ��󶨣��ǣ����ذ󶨴��󣻷����ɲ�����Ϣ�����ɰ���Ϣ
    */
  
    -- ������У�顿
  
    -- 0)ͬһ�ͻ��˱�ʶֻ�ܰ�4��������
    SELECT COUNT(1)
      INTO INT_����ֵ
      FROM ƽ̨�ӿ�_��������Ϣ B
     WHERE B.ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND B.�ͻ��˱�ʶ = STR_�ͻ��˱�ʶ
       AND B.ҽԺ���� = STR_ҽԺ����
       AND B.״̬ = '0';
  
    IF INT_����ֵ >= 4 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��ע���������������4����';
      GOTO �˳�;
    END IF;
  
    -- 1������
    IF STR_���� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '������������';
      GOTO �˳�;
    END IF;
  
    -- 2���������
    IF STR_��������� = '��Ů' THEN
      -- 2.1)��Ů
      IF STR_���֤�� IS NULL THEN
        -- ���������֤ʱ��Ҫ������໤����Ϣ
        -- 2.1.1)�Ա�
        IF STR_�Ա� IS NULL OR FU_ƽ̨�ӿ�_��֤�Ա�(STR_�Ա�) <> 0 THEN
          INT_����ֵ   := -1;
          STR_������Ϣ := '��������Ч���Ա�';
          GOTO �˳�;
        END IF;
        -- 2.1.2)��������
        IF STR_�������� IS NULL OR FU_����ת����(STR_��������) IS NULL THEN
          INT_����ֵ   := -1;
          STR_������Ϣ := '��������Ч�ĳ������ڣ���';
          GOTO �˳�;
        END IF;
        -- 2.1.3)�໤�ˣ���Ҫ��֤���֤��Ч��
        IF STR_�໤������ IS NULL OR STR_�໤�����֤�� IS NULL OR STR_�໤���ֻ����� IS NULL OR
           FU_ƽ̨�ӿ�_��֤�ֻ���(STR_�໤���ֻ�����) <> 0 OR
           FU_ƽ̨�ӿ�_��֤���֤(STR_�໤�����֤��) <> 0 THEN
          INT_����ֵ   := -1;
          STR_������Ϣ := '��������Ч�ļ໤�����������֤�š��ֻ�������Ϣ��';
          GOTO �˳�;
        END IF;
        -- 2.1.4)����
        STR_����     := FU_�õ�_����(TO_DATE(STR_��������, 'yyyy-mm-dd'));
        DAT_�������� := TO_DATE(STR_��������, 'yyyy-mm-dd');
      ELSE
        -- �������֤ʱ���⹹��Ϣ
        IF FU_ƽ̨�ӿ�_��֤���֤(STR_���֤��) <> 0 THEN
          STR_������Ϣ := '��Ч�����֤����';
          INT_����ֵ   := -1;
          GOTO �˳�;
        ELSE
          INT_����ֵ := FU_ƽ̨�ӿ�_�⹹���֤(STR_���֤�� => STR_���֤��,
                                   DAT_�������� => DAT_��������,
                                   STR_����     => STR_����,
                                   STR_�Ա�     => STR_�Ա�,
                                   STR_������Ϣ => STR_������Ϣ);
          IF INT_����ֵ <> 0 THEN
            GOTO �˳�;
          END IF;
        END IF;
      END IF;
    
    ELSIF STR_��������� = '����' OR STR_��������� = '����' THEN
      -- 2.2)���˻�����
      -- 2.2.1)���֤
      INT_����ֵ := FU_ƽ̨�ӿ�_�⹹���֤(STR_���֤�� => STR_���֤��,
                               DAT_�������� => DAT_��������,
                               STR_����     => STR_����,
                               STR_�Ա�     => STR_�Ա�,
                               STR_������Ϣ => STR_������Ϣ);
      IF INT_����ֵ <> 0 THEN
        GOTO �˳�;
      END IF;
      -- 2.2.2)�ֻ�����
      IF STR_�ֻ����� IS NULL OR FU_ƽ̨�ӿ�_��֤�ֻ���(STR_�ֻ�����) <> 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '��������Ч���ֻ����룡';
        GOTO �˳�;
      END IF;
      -- 2.2.3)�����ʹ�õ��ֶ�����ֵ
      STR_�໤������     := NULL;
      STR_�໤�����֤�� := NULL;
      STR_�໤���ֻ����� := NULL;
      STR_�໤����ϵ��ַ := NULL;
    ELSE
      -- 2.3)�޷�ʶ��
      INT_����ֵ   := -1;
      STR_������Ϣ := '�޷�ʶ��Ĳ������';
      GOTO �˳�;
    END IF;
  
    -- ���жϲ�����Ϣ�Ƿ���ڡ�
    -- 1) �Ƿ��Ѿ�ע��
    -- 1.1)��Ů
    IF STR_��������� = '��Ů' THEN
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM ƽ̨�ӿ�_��������Ϣ B
       WHERE B.ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND B.ҽԺ���� = STR_ҽԺ����
         AND ((B.�໤�����֤�� = STR_�໤�����֤�� AND B.���� = STR_����) OR
             B.���֤�� = STR_���֤��)
         AND B.״̬ = '0';
    ELSE
      --1.2)���˻�����
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM ƽ̨�ӿ�_��������Ϣ B
       WHERE B.ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND B.ҽԺ���� = STR_ҽԺ����
         AND B.���֤�� = STR_���֤��
         AND B.״̬ = '0';
    END IF;
  
    IF INT_����ֵ <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�����֤�ѱ������û�ע�ᣡ';
      GOTO �˳�;
    END IF;
  
    -- 2) HISϵͳ���Ƿ����
    IF STR_��������� = '��Ů' THEN
      -- ��Ů
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM ������Ŀ_������Ϣ B
        LEFT JOIN ������Ŀ_������Ϣ_���� C
          ON B.�������� = C.��������
         AND B.����ID = C.����ID
       WHERE B.�������� = STR_ҽԺ����
         AND ((C.�໤�����֤�� = STR_�໤�����֤�� AND B.���� = STR_����) OR
             B.���֤�� = STR_���֤��);
    ELSE
      -- ���˻�����
      SELECT COUNT(1)
        INTO INT_����ֵ
        FROM ������Ŀ_������Ϣ B
       WHERE B.�������� = STR_ҽԺ����
         AND B.���֤�� = STR_���֤��;
    END IF;
  
    IF INT_����ֵ = 0 THEN
    
      -- 2.1) �����������ɲ�����Ϣ
      -- ���ɲ���ID
      PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '30',
                  PRM_��������   => STR_ҽԺ����,
                  PRM_��������   => '1',
                  PRM_����Ψһ�� => STR_����ID,
                  PRM_ִ�н��   => INT_����ֵ,
                  PRM_������Ϣ   => STR_������Ϣ);
    
      IF INT_����ֵ <> 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '���ɲ���IDʧ��,ԭ��:' + STR_������Ϣ;
        GOTO �˳�;
      END IF;
    
      -- ���벡����Ϣ
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
        (STR_ҽԺ����,
         STR_����ID,
         NULL,
         NULL,
         STR_����,
         STR_�Ա�,
         DAT_��������,
         STR_����,
         NULL,
         NULL,
         STR_�ֻ�����,
         NULL,
         NULL,
         NULL,
         SYSDATE,
         FU_ͨ��_����_ת��_��ƴ(STR_����),
         FU_ͨ��_����_ת��_���(STR_����),
         NULL,
         STR_���֤��,
         NULL,
         STR_ƽ̨��ʶ,
         NULL,
         NULL,
         NULL,
         NULL,
         NULL,
         NULL,
         NULL,
         NULL,
         NULL,
         NULL);
    
      -- ���벡�˸�����Ϣ
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
        (STR_ҽԺ����,
         STR_����ID,
         NULL,
         NULL,
         NULL,
         NULL,
         NULL,
         NULL,
         STR_���������,
         STR_�໤������,
         STR_�໤�����֤��,
         STR_�໤���ֻ�����,
         STR_�໤����ϵ��ַ,
         '1');
    ELSE
      -- 2.2) �����򷵻ز�����Ϣ
      /*
        ���ڵ����⣬�п��ܻ᷵�ض�����¼
      */
      IF STR_��������� = '��Ů' THEN
        -- ��Ů
        SELECT B.����ID
          INTO STR_����ID
          FROM ������Ŀ_������Ϣ B
          LEFT JOIN ������Ŀ_������Ϣ_���� C
            ON B.�������� = C.��������
           AND B.����ID = C.����ID
         WHERE B.�������� = STR_ҽԺ����
           AND ((C.�໤�����֤�� = STR_�໤�����֤�� AND B.���� = STR_����) OR
               B.���֤�� = STR_���֤��)
           AND ROWNUM = 1;
      ELSE
        -- ���˻�����
        SELECT ����ID
          INTO STR_����ID
          FROM ������Ŀ_������Ϣ B
         WHERE B.�������� = STR_ҽԺ����
           AND B.���֤�� = STR_���֤��
           AND ROWNUM = 1;
      END IF;
    END IF;
  
    -- ��ע�Ს����Ϣ��
    INSERT INTO ƽ̨�ӿ�_��������Ϣ
      (��ˮ��,
       ƽ̨��ʶ,
       �ͻ��˱�ʶ,
       ҽԺ����,
       ����ID,
       ���������,
       ����,
       �Ա�,
       ��������,
       ���֤��,
       �ֻ�����,
       ��ϵ��ַ,
       �໤������,
       �໤�����֤��,
       �໤���ֻ�����,
       �໤����ϵ��ַ,
       ������,
       ����ʱ��,
       ������,
       ����ʱ��,
       �����,
       ��ע,
       ״̬)
    VALUES
      (SEQ_ƽ̨�ӿ�_��������Ϣ_��ˮ��.NEXTVAL,
       STR_ƽ̨��ʶ,
       STR_�ͻ��˱�ʶ,
       STR_ҽԺ����,
       STR_����ID,
       STR_���������,
       STR_����,
       STR_�Ա�,
       DAT_��������,
       STR_���֤��,
       STR_�ֻ�����,
       STR_��ϵ��ַ,
       STR_�໤������,
       STR_�໤�����֤��,
       STR_�໤���ֻ�����,
       STR_�໤����ϵ��ַ,
       STR_ƽ̨��ʶ,
       DAT_ϵͳʱ��,
       NULL,
       NULL,
       0,
       NULL,
       0);
  
    -- �������˳���
    OPEN CUR_���ؽ���� FOR
      SELECT STR_����ID AS ����ID, DAT_ϵͳʱ�� AS ע��ʱ�� FROM DUAL;
  
    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�!';
  
    -- ��������־��
    PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
                 STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
                 STR_ҽԺ����   => STR_ҽԺ����,
                 STR_���ܱ���   => STR_���ܱ���,
                 STR_�������   => STR_�������,
                 DAT_����ʱ��   => DAT_ϵͳʱ��,
                 STR_������   => STR_������Ϣ,
                 STR_��������   => STR_����ID,
                 STR_ִ����     => STR_ƽ̨��ʶ,
                 DAT_ִ��ʱ��   => SYSDATE,
                 STR_ִ��״̬   => INT_����ֵ);
  
    COMMIT;
    RETURN;
  
  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  ROLLBACK;
  RETURN;

END PR_ƽ̨�ӿ�_������ע��;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_������ע��
prompt ================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_������ע��(STR_�������   IN VARCHAR2,
                                          CUR_���ؽ���� OUT SYS_REFCURSOR,
                                          INT_����ֵ     OUT INTEGER,
                                          STR_������Ϣ   OUT VARCHAR2) IS
  -- �̶�����
  DAT_ϵͳʱ�� DATE;

  --�������
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ҵ�����
  STR_����ID VARCHAR2(50);

BEGIN
  BEGIN

    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;

    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ϣ��Ч��';
      GOTO �˳�;
    END IF;

    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;

    -- ���������������
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);

    --�����ܶ��岿�֡�
    STR_����ID := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 6);

    -- ������У�顿
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;

    -- ��������ע����
    /*
       ֻ��ɾ��ָ��ƽ̨��ָ���ͻ��ˣ�ָ��ҽԺ���룬��״̬Ϊ0����Ч���İ󶨲���ID��¼��
    */
    UPDATE ƽ̨�ӿ�_��������Ϣ J
       SET J.������ = STR_ƽ̨��ʶ, J.����ʱ�� = DAT_ϵͳʱ��, J.״̬ = -1
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND �ͻ��˱�ʶ = STR_�ͻ��˱�ʶ
       AND ҽԺ���� = STR_ҽԺ����
       AND ״̬ = 0
       AND ����ID = STR_����ID;

    -- �������˳���
    OPEN CUR_���ؽ���� FOR
      SELECT NULL FROM DUAL;

    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�!';

    -- ��������־��
    PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
                 STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
                 STR_ҽԺ����   => STR_ҽԺ����,
                 STR_���ܱ���   => STR_���ܱ���,
                 STR_�������   => STR_�������,
                 DAT_����ʱ��   => DAT_ϵͳʱ��,
                 STR_������   => STR_������Ϣ,
                 STR_��������   => STR_����ID,
                 STR_ִ����     => STR_ƽ̨��ʶ,
                 DAT_ִ��ʱ��   => SYSDATE,
                 STR_ִ��״̬   => INT_����ֵ);

    COMMIT;
    RETURN;

  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  ROLLBACK;
  RETURN;

END PR_ƽ̨�ӿ�_������ע��;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_�����б�
prompt ===============================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_�����б�(STR_�������   IN VARCHAR2,
                                         CUR_���ؽ���� OUT SYS_REFCURSOR,
                                         INT_����ֵ     OUT INTEGER,
                                         STR_������Ϣ   OUT VARCHAR2) IS
  -- �̶�����
  DAT_ϵͳʱ�� DATE;

  --�������
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_�������� VARCHAR2(50);

BEGIN
  BEGIN
  
    -- ������֤
  
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;
  
    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    -- ���������������
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);
  
    -- �����ܲ���������
    STR_�������� := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 6);
  
    -- ����У��
    IF STR_�������� <> '����'
       AND STR_�������� <> 'סԺ' THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ŀ������࣡';
      GOTO �˳�;
    END IF;
  
    /*
    ˵����
        1��ֻ��ʾ��Ч�Ŀ����б�
        2������ֻ��ʾ�йҺ�Ȩ�޵Ŀ��ҡ�
    */
    IF STR_�������� = '����' THEN
      OPEN CUR_���ؽ���� FOR
        SELECT ���ұ���, ��������
          FROM ������Ŀ_��������
         WHERE �������� = STR_ҽԺ����
           AND ��Ч״̬ = '��Ч'
           AND ɾ����־ = '0'
           AND �Ƿ�Һ� = '��'
           AND �������� LIKE '%1.����,%';
    ELSIF STR_�������� = 'סԺ' THEN
      OPEN CUR_���ؽ���� FOR
        SELECT ���ұ���, ��������
          FROM ������Ŀ_��������
         WHERE �������� = STR_ҽԺ����
           AND ��Ч״̬ = '��Ч'
           AND ɾ����־ = '0'
           AND �������� LIKE '%2.סԺ,%';
    END IF;
  
    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�!';
  
    -- ��������־��
    PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
                 STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
                 STR_ҽԺ����   => STR_ҽԺ����,
                 STR_���ܱ���   => STR_���ܱ���,
                 STR_�������   => STR_�������,
                 DAT_����ʱ��   => DAT_ϵͳʱ��,
                 STR_������   => STR_������Ϣ,
                 STR_��������   => STR_��������,
                 STR_ִ����     => STR_ƽ̨��ʶ,
                 DAT_ִ��ʱ��   => SYSDATE,
                 STR_ִ��״̬   => INT_����ֵ);
  
    RETURN;
  
  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  RETURN;

END PR_ƽ̨�ӿ�_�����б�;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_����ҽ����ϸ
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_����ҽ����ϸ(STR_�������� IN VARCHAR2,
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

END PR_ƽ̨�ӿ�_����ҽ����ϸ;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_������ɷ�����
prompt ==================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_������ɷ�����(STR_�������   IN VARCHAR2,
                                            CUR_���ؽ���� OUT SYS_REFCURSOR,
                                            INT_����ֵ     OUT INTEGER,
                                            STR_������Ϣ   OUT VARCHAR2) IS

  DAT_ϵͳʱ�� DATE;

  -- �̶�����
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID     VARCHAR2(50);
  STR_���ﲡ���� VARCHAR2(50);

  -- �������
  STR_�Ƿ��������շ� VARCHAR2(50);
  I_����               INTEGER;

  STR_�շ���� VARCHAR2(50);
  STR_ҽ����   VARCHAR2(50);
  STR_�Һ���� VARCHAR2(50);

  NUM_���ɷ��ܶ� NUMBER(18, 3);

BEGIN
  begin
  
    -- ����������Ч����֤��
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;
  
    -- ����������Ч����֤��
    IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ϣ��Ч��';
      GOTO �˳�;
    END IF;
  
    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    -- ���̶�����������
  
    STR_ƽ̨��ʶ   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 5);
  
    -- �����ܲ���������
  
    STR_����ID     := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_���ﲡ���� := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 7);
  
    -- �������������֤��
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;
    IF STR_���ﲡ���� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч�����ﲡ���ţ�';
      GOTO �˳�;
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣0��' || SQLERRM;
      GOTO �˳�;
  END;

  -- ��ȡϵͳ����
  BEGIN
    SELECT TO_NUMBER(ֵ)
      INTO I_����
      FROM ������Ŀ_���������б�
     WHERE �������� = '48'
       AND �������� = STR_ҽԺ����;
  EXCEPTION
    WHEN OTHERS THEN
      I_���� := 15;
  END;

  BEGIN
    SELECT ֵ
      INTO STR_�Ƿ��������շ�
      FROM ������Ŀ_���������б�
     WHERE �������� = '311'
       AND �������� = STR_ҽԺ����;
  EXCEPTION
    WHEN OTHERS THEN
      STR_�Ƿ��������շ� := '��';
  END;

  --���������ݡ�

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
       AND A.�������� = STR_ҽԺ����
       AND A.���ﲡ���� = STR_���ﲡ����
       AND A.����ID = STR_����ID
       /*AND C.�Һ�ʱ�� > TRUNC(SYSDATE) - I_���� + 1
       AND A.¼��ʱ�� > TRUNC(SYSDATE) - I_���� + 1*/
       AND C.�Һ�ʱ�� > TRUNC(SYSDATE) - 11
       AND A.¼��ʱ�� > TRUNC(SYSDATE) - 11
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
  
    PR_ƽ̨�ӿ�_����ҽ����ϸ(STR_�������� => STR_ҽԺ����,
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

  -- �����ؽ����
  BEGIN
    SELECT SUM(�ܽ��)
      INTO NUM_���ɷ��ܶ�
      FROM �������_����ҽ����ϸ
     WHERE �������� = STR_ҽԺ����
       AND ���ﲡ���� = STR_���ﲡ����
       AND ����ID = STR_����ID
       AND �Һ���� = STR_�Һ����
       AND �շ���� = STR_�շ����;
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  OPEN CUR_���ؽ���� FOR
    SELECT ����ID,
           ���ﲡ����,
           �շ����,
           ��Ŀ����,
           ��Ŀ����,
           ���,
           ����,
           ��λ����,
           ����,
           �ܽ��,
           NUM_���ɷ��ܶ� AS ���ɷ��ܶ�,
           ����ʱ��
      FROM �������_����ҽ����ϸ
     WHERE �������� = STR_ҽԺ����
       AND ���ﲡ���� = STR_���ﲡ����
       AND ����ID = STR_����ID
       AND �Һ���� = STR_�Һ����
       AND �շ���� = STR_�շ����;

  INT_����ֵ   := 0;
  STR_������Ϣ := '����ɹ�!';

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => STR_�շ����,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  COMMIT;
  RETURN;

  --���쳣�˳���
  <<�˳�>>

  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  --��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  ROLLBACK;
  RETURN;

END PR_ƽ̨�ӿ�_������ɷ�����;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_������ɷ�����
prompt ==================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_������ɷ�����(STR_�������   IN VARCHAR2,
                                            CUR_���ؽ���� OUT SYS_REFCURSOR,
                                            INT_����ֵ     OUT INTEGER,
                                            STR_������Ϣ   OUT VARCHAR2) IS

  DAT_ϵͳʱ�� DATE;

  -- �̶�����
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID     VARCHAR2(50);
  STR_���ﲡ���� VARCHAR2(50);
  STR_�շ����   VARCHAR2(50);

  -- �������
  STR_������       VARCHAR2(50);
  DAT_��������ʱ�� DATE;
  STR_������ע     VARCHAR2(50);
  NUM_Ӧ�����     NUMBER(18, 3);

  CURSOR CUR_���ɷ���ϸ IS
    SELECT *
      FROM �������_����ҽ����ϸ
     WHERE �������� = STR_ҽԺ����
       AND ����ID = STR_����ID
       AND ���ﲡ���� = STR_���ﲡ����
       AND �շ���� = STR_�շ����;

  ROW_���ɷ���ϸ CUR_���ɷ���ϸ%ROWTYPE;

BEGIN

  -- ����������Ч����֤��
  IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '�Ƿ�����';
    GOTO �˳�;
  END IF;

  -- ����������Ч����֤��
  IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ϣ��Ч��';
    GOTO �˳�;
  END IF;

  -- �����ݳ�ʼ����
  SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  NUM_Ӧ����� := 0;

  -- ���̶�����������

  STR_ƽ̨��ʶ   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 1);
  STR_��֤�ܳ�   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 2);
  STR_�ͻ��˱�ʶ := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 3);
  STR_���ܱ���   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 4);
  STR_ҽԺ����   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 5);

  -- �����ܲ���������

  STR_����ID     := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 6);
  STR_���ﲡ���� := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 7);
  STR_�շ����   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 8);

  -- �������������֤��
  IF STR_����ID IS NULL THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ч�Ĳ���ID��';
    GOTO �˳�;
  END IF;
  IF STR_���ﲡ���� IS NULL THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ч�����ﲡ���ţ�';
    GOTO �˳�;
  END IF;
  IF STR_�շ���� IS NULL THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ч���շ���ţ�';
    GOTO �˳�;
  END IF;

  -- �жϽɷ�״̬
  -- ҽ����
  SELECT COUNT(1)
    INTO INT_����ֵ
    FROM �������_����ҽ����ϸ M, �������_����ҽ�� Y
   WHERE M.�������� = Y.��������
     AND M.����ID = Y.����ID
     AND M.���ﲡ���� = Y.���ﲡ����
     AND M.��� = Y.���
     AND M.ҽ���� = Y.ҽ����
     AND M.�������� = STR_ҽԺ����
     AND M.����ID = STR_����ID
     AND M.���ﲡ���� = STR_���ﲡ����
     AND M.�շ���� = STR_�շ����
     AND Y.�շ�״̬ = '����δ�շ�'
     AND Y.���۷�ʽ <> '�˷��Զ�����';

  IF INT_����ֵ <= 0 THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '�ɷѼ�¼��ʧЧ!';
    GOTO �˳�;
  END IF;

  -- ������
  SELECT COUNT(1)
    INTO INT_����ֵ
    FROM �������_����ҽ����ϸ M, �������_���ﴦ�� C
   WHERE M.�������� = C.��������
     AND M.����ID = C.����ID
     AND M.���ﲡ���� = C.���ﲡ����
     AND M.��� = C.���
     AND M.ҽ���� = C.ҽ����
     AND M.��ˮ�� = C.ҽ����ˮ��
     AND M.�������� = STR_ҽԺ����
     AND M.����ID = STR_����ID
     AND M.���ﲡ���� = STR_���ﲡ����
     AND M.�շ���� = STR_�շ����;

  IF INT_����ֵ > 0 THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '�ɷѼ�¼��ʧЧ!';
    GOTO �˳�;
  END IF;

  BEGIN
  
    IF INT_����ֵ <> -1 THEN
      -- 1)���ɶ�����
      PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '6001',
                  PRM_��������   => STR_ҽԺ����,
                  PRM_��������   => '1',
                  PRM_����Ψһ�� => STR_������,
                  PRM_ִ�н��   => INT_����ֵ,
                  PRM_������Ϣ   => STR_������Ϣ);
      IF INT_����ֵ <> 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '����������ʧ��!';
        GOTO �˳�;
      END IF;
    
      -- 2)���ɹ���ʱ��
      SELECT SYSDATE + (1 / (24 * 60)) * 15
        INTO DAT_��������ʱ��
        FROM DUAL;
    
      -- 3)���ɶ�����ע
      STR_������ע := '����15���������֧���������Զ�ȡ��';
    
      -- 4)���涩����ϸ
      OPEN CUR_���ɷ���ϸ;
      LOOP
        FETCH CUR_���ɷ���ϸ
          INTO ROW_���ɷ���ϸ;
        EXIT WHEN CUR_���ɷ���ϸ%NOTFOUND;
      
        INSERT INTO ƽ̨�ӿ�_������ϸ
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
          (SEQ_ƽ̨�ӿ�_������ϸ_��ˮ��.NEXTVAL,
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
    
      -- 5)���涩��
      INSERT INTO ƽ̨�ӿ�_����
        (��ˮ��,
         ƽ̨��ʶ,
         �ͻ��˱�ʶ,
         ҽԺ����,
         ����ID,
         ���ﲡ����,
         ��������,
         ������,
         ��������,
         ����ʱ��,
         Ӧ�����,
         �Żݽ��,
         ʵ�ս��,
         ����ʱ��,
         ����״̬,
         ������,
         ����ʱ��)
      VALUES
        (SEQ_ƽ̨�ӿ�_����_��ˮ��.NEXTVAL,
         STR_ƽ̨��ʶ,
         STR_�ͻ��˱�ʶ,
         STR_ҽԺ����,
         STR_����ID,
         STR_���ﲡ����,
         STR_�շ����,
         STR_������,
         '����ɷ�',
         DAT_ϵͳʱ��,
         NUM_Ӧ�����,
         0,
         0,
         DAT_��������ʱ��,
         '��֧��',
         STR_ƽ̨��ʶ,
         DAT_ϵͳʱ��);
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '���涩��ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  -- �����ؽ����
  OPEN CUR_���ؽ���� FOR
    SELECT STR_����ID AS ����ID,
           STR_���ﲡ���� AS ���ﲡ����,
           STR_�շ���� AS �շ����,
           STR_������ AS ������,
           NUM_Ӧ����� AS Ӧ�����,
           DAT_��������ʱ�� AS ����ʱ��,
           '����15���������֧��' AS ��ע
      FROM DUAL;

  INT_����ֵ   := 0;
  STR_������Ϣ := '����ɹ�!';

  CLOSE CUR_���ɷ���ϸ;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => STR_������,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  COMMIT;
  RETURN;

  --���쳣�˳���
  <<�˳�>>
  IF CUR_���ɷ���ϸ%ISOPEN THEN
    CLOSE CUR_���ɷ���ϸ;
  END IF;
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  --��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  ROLLBACK;
  RETURN;

END PR_ƽ̨�ӿ�_������ɷ�����;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_������ɷ�֧��
prompt ==================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_������ɷ�֧��(STR_�������   IN VARCHAR2,
                                            CUR_���ؽ���� OUT SYS_REFCURSOR,
                                            INT_����ֵ     OUT INTEGER,
                                            STR_������Ϣ   OUT VARCHAR2) IS

  DAT_ϵͳʱ�� DATE;
  STR_ƽ̨���� VARCHAR2(50);

  -- �̶�����
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID       VARCHAR2(50);
  STR_��������     VARCHAR2(50);
  STR_������       VARCHAR2(50);
  STR_ƽ̨������   VARCHAR2(50);
  STR_ƽ̨���׺�   VARCHAR2(50);
  STR_����Ӧ����� VARCHAR2(50);
  STR_ƽ̨ʵ�ս�� VARCHAR2(50);
  STR_ƽ̨����ʱ�� VARCHAR2(50);
  STR_ƽ̨����ʱ�� VARCHAR2(50);

  -- �������
  STR_��Ʊ��         VARCHAR2(50);
  STR_��Ʊ���       VARCHAR2(50);
  STR_ҽ����         VARCHAR2(50);
  STR_�շ����       VARCHAR2(50);
  STR_���ﲡ����     VARCHAR2(50);
  INT_С��λ��       INTEGER;
  STR_���뷽ʽ       VARCHAR2(50);
  STR_�շ�ֱ�ӿۿ�� VARCHAR2(50);
  STR_��ִ�п��ҷ�Ʊ VARCHAR2(50);

  STR_֧����ʽ     VARCHAR2(50);
  STR_�Һ����     VARCHAR2(50);
  STR_�������ͱ��� VARCHAR2(50);
  STR_������������ VARCHAR2(50);

  CUR_Ԥ����Ϣ     SYS_REFCURSOR;
  STR_Ԥ������ϸ VARCHAR2(4000);

  STR_ִ�п��ұ��� VARCHAR(50);
  NUM_�����ܶ�     NUMBER(18, 3);
  NUM_�Ը��ܶ�     NUMBER(18, 3);
  NUM_�Ż��ܶ�     NUMBER(18, 3);
  NUM_Ӧ���ܶ�     NUMBER(18, 3);
  NUM_�����ܶ�     NUMBER(18, 3);
  NUM_ʵ���ܶ�     NUMBER(18, 3);
  NUM_�����ܶ�     NUMBER(18, 3);

  NUM_������֧���ܶ� NUMBER(18, 3);

BEGIN
  BEGIN
    -- ����������Ч����֤��
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;
  
    -- ����������Ч����֤��
    IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ϣ��Ч��';
      GOTO �˳�;
    END IF;
  
    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
    STR_��������     := '����ɷ�';
    STR_�������ͱ��� := '1';
    STR_������������ := '�ֽ�';
  
    -- ���̶�����������
  
    STR_ƽ̨��ʶ   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 5);
  
    -- �����ܲ���������
  
    STR_����ID       := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_������       := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 7);
    STR_ƽ̨������   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 8);
    STR_ƽ̨����ʱ�� := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 9);
    STR_ƽ̨���׺�   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 10);
    STR_ƽ̨����ʱ�� := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 11);
    STR_����Ӧ����� := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 12);
    STR_ƽ̨ʵ�ս�� := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 13);
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣0��' || SQLERRM;
      GOTO �˳�;
  END;

  -- ����ȡƽ̨��Ϣ��
  BEGIN
    SELECT P.֧����ʽ, P.ƽ̨����
      INTO STR_֧����ʽ, STR_ƽ̨����
      FROM ƽ̨�ӿ�_ƽ̨���� P
     WHERE P.ƽ̨��ʶ = STR_ƽ̨��ʶ;
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'δ�ҵ���Ч��ƽ̨��Ϣ��';
      GOTO �˳�;
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  -- ��������������֤��
  IF STR_����ID IS NULL THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ч�Ĳ���ID��';
    GOTO �˳�;
  END IF;
  IF STR_������ IS NULL THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ч�Ķ����ţ�';
    GOTO �˳�;
  END IF;
  IF STR_ƽ̨������ IS NULL THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ч��ƽ̨�����ţ�';
    GOTO �˳�;
  END IF;
  IF STR_ƽ̨���׺� IS NULL THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ч��ƽ̨���׺ţ�';
    GOTO �˳�;
  END IF;
  IF STR_����Ӧ����� IS NULL THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ч�Ķ���Ӧ����';
    GOTO �˳�;
  END IF;
  IF STR_ƽ̨ʵ�ս�� IS NULL THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ч��ƽ̨ʵ�ս�';
    GOTO �˳�;
  END IF;
  IF STR_ƽ̨����ʱ�� IS NULL OR FU_����ת����(STR_ƽ̨����ʱ��) IS NULL THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ч�Ķ���ʱ�䣡';
    GOTO �˳�;
  END IF;
  IF STR_ƽ̨����ʱ�� IS NULL OR FU_����ת����(STR_ƽ̨����ʱ��) IS NULL THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ч�Ľ���ʱ�䣡';
    GOTO �˳�;
  END IF;

  IF STR_����Ӧ����� <> STR_ƽ̨ʵ�ս�� THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := 'Ӧ�������ʵ�ս�����';
    GOTO �˳�;
  END IF;

  -- ϵͳ����
  BEGIN
    SELECT ֵ
      INTO STR_���뷽ʽ
      FROM ������Ŀ_���������б�
     WHERE �������� = '53'
       AND �������� = STR_ҽԺ����;
  EXCEPTION
    WHEN OTHERS THEN
      STR_���뷽ʽ := '2';
  END;

  BEGIN
    SELECT TO_NUMBER(ֵ)
      INTO INT_С��λ��
      FROM ������Ŀ_���������б�
     WHERE �������� = '52'
       AND �������� = STR_ҽԺ����;
  EXCEPTION
    WHEN OTHERS THEN
      INT_С��λ�� := 2;
  END;

  BEGIN
    SELECT ֵ
      INTO STR_�շ�ֱ�ӿۿ��
      FROM ������Ŀ_���������б�
     WHERE �������� = '164'
       AND �������� = STR_ҽԺ����;
  EXCEPTION
    WHEN OTHERS THEN
      STR_�շ�ֱ�ӿۿ�� := '��';
  END;

  BEGIN
    SELECT ֵ
      INTO STR_��ִ�п��ҷ�Ʊ
      FROM ������Ŀ_���������б�
     WHERE �������� = '50'
       AND �������� = STR_ҽԺ����;
  EXCEPTION
    WHEN OTHERS THEN
      STR_��ִ�п��ҷ�Ʊ := '0';
  END;

  -- ��֤����
  BEGIN
    SELECT ��������, ���ﲡ����
      INTO STR_�շ����, STR_���ﲡ����
      FROM ƽ̨�ӿ�_����
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND ҽԺ���� = STR_ҽԺ����
       AND �ͻ��˱�ʶ = STR_�ͻ��˱�ʶ
       AND ����ID = STR_����ID
       AND ������ = STR_������
       AND �������� = STR_��������
       AND Ӧ����� = TO_NUMBER(STR_����Ӧ�����)
       AND ����ʱ�� >= SYSDATE
       AND ����״̬ = '��֧��';
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ķ�����';
      GOTO �˳�;
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  -- ��֤ҽ��״̬
  SELECT COUNT(1)
    INTO INT_����ֵ
    FROM �������_����ҽ����ϸ M, �������_����ҽ�� Y
   WHERE M.�������� = Y.��������
     AND M.����ID = Y.����ID
     AND M.���ﲡ���� = Y.���ﲡ����
     AND M.��� = Y.���
     AND M.ҽ���� = Y.ҽ����
     AND M.�������� = STR_ҽԺ����
     AND M.����ID = STR_����ID
     AND M.���ﲡ���� = STR_���ﲡ����
     AND M.�շ���� = STR_�շ����
     AND Y.�շ�״̬ = '����δ�շ�'
     AND Y.���۷�ʽ <> '�˷��Զ�����';

  IF INT_����ֵ <= 0 THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '�ɷѼ�¼��ʧЧ!';
    GOTO �˳�;
  END IF;

  -- ��֤����״̬
  SELECT COUNT(1)
    INTO INT_����ֵ
    FROM �������_����ҽ����ϸ M, �������_���ﴦ�� C
   WHERE M.�������� = C.��������
     AND M.����ID = C.����ID
     AND M.���ﲡ���� = C.���ﲡ����
     AND M.��� = C.���
     AND M.ҽ���� = C.ҽ����
     AND M.��ˮ�� = C.ҽ����ˮ��
     AND M.�������� = STR_ҽԺ����
     AND M.����ID = STR_����ID
     AND M.���ﲡ���� = STR_���ﲡ����
     AND M.�շ���� = STR_�շ����;

  IF INT_����ֵ > 0 THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '�ɷѼ�¼��ʧЧ!';
    GOTO �˳�;
  END IF;

  -- ��֤ҽ����ϸ
  BEGIN
    SELECT DISTINCT �Һ����, ҽ����
      INTO STR_�Һ����, STR_ҽ����
      FROM �������_����ҽ����ϸ
     WHERE �������� = STR_ҽԺ����
       AND ����ID = STR_����ID
       AND ���ﲡ���� = STR_���ﲡ����
       AND �շ���� = STR_�շ����;
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ľɷ���Ϣ��';
      GOTO �˳�;
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  -- ���ɷ�Ʊ��
  SELECT FU_����_��ȡ��ǰƱ�ݺ�(STR_ҽԺ����, STR_ƽ̨��ʶ, '1')
    INTO STR_��Ʊ��
    FROM DUAL;

  IF STR_��Ʊ�� = '�뵽����������Ʊ��' THEN
    STR_������Ϣ := '�ò���Ա�޷�Ʊ��,��֪ͨ����������Ʊ��!';
    GOTO �˳�;
  END IF;

  -- ���ɷ�Ʊ���
  SELECT SEQ_�������_��Ʊ�Ǽ�_��Ʊ���.NEXTVAL
    INTO STR_��Ʊ���
    FROM DUAL;

  -- �����ܴ���
  BEGIN
  
    PR_�������_Ԥ����(STR_��������       => STR_ҽԺ����,
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
      INT_����ֵ   := -1;
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
      INT_����ֵ   := -1;
      STR_������Ϣ := '����Ԥ�����¼ʧ��!';
      GOTO �˳�;
    END IF;
  
    STR_Ԥ������ϸ := '��Ʊ��,ִ�п��ұ���,�����ܶ�,�����ܶ�,�Ը��ܶ�,�Ż��ܶ�,Ӧ���ܶ�,�����ܶ�,ʵ���ܶ�,��Ʊ���,ԭ��Ʊҽ��֧ͨ�����,�����˷��ܶ�,���ο��˷��ܶ�,�����ֽ��˷��ܶ�,������֧���ܶ�##' ||
                  STR_Ԥ������ϸ;
  
    dbms_output.put_line(STR_Ԥ������ϸ);
  
    PR_�������_�����շ�(STR_��������       => STR_ҽԺ����,
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
                 STR_����Ա����     => STR_ƽ̨����,
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
      INT_����ֵ   := -1;
      STR_������Ϣ := '����ɷѼ�¼ʧ��!';
      GOTO �˳�;
    END IF;
  
    -- ���¶���״̬
    UPDATE ƽ̨�ӿ�_����
       SET ����״̬     = '��֧��',
           ʵ�ս��     = TO_NUMBER(STR_ƽ̨ʵ�ս��),
           ƽ̨������   = STR_ƽ̨������,
           ƽ̨����ʱ�� = TO_DATE(STR_ƽ̨����ʱ��, 'yyyy/mm/dd hh24:mi:ss'),
           ƽ̨���׺�   = STR_ƽ̨���׺�,
           ƽ̨����ʱ�� = TO_DATE(STR_ƽ̨����ʱ��, 'yyyy/mm/dd hh24:mi:ss'),
           ƽ̨�˿��   = NULL,
           ƽ̨�˿�ʱ�� = NULL,
           ������       = STR_ƽ̨��ʶ,
           ����ʱ��     = DAT_ϵͳʱ��
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND �ͻ��˱�ʶ = STR_�ͻ��˱�ʶ
       AND ����ID = STR_����ID
       AND ������ = STR_������
       AND �������� = STR_��������
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

  -- �����ؽ����
  OPEN CUR_���ؽ���� FOR
    SELECT STR_����ID AS ����ID,
           STR_���ﲡ���� AS ���ﲡ����,
           STR_������ AS ������,
           '' AS Ʊ�ݺ�,
           '' AS ��ע
      FROM DUAL;

  INT_����ֵ   := 0;
  STR_������Ϣ := '����ɹ�!';

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => STR_������,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  COMMIT;
  RETURN;

  --���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  --��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  ROLLBACK;
  RETURN;

END PR_ƽ̨�ӿ�_������ɷ�֧��;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_�����������
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_�����������(STR_�������   IN VARCHAR2,
                                             CUR_���ؽ���� OUT SYS_REFCURSOR,
                                             INT_����ֵ     OUT INTEGER,
                                             STR_������Ϣ   OUT VARCHAR2) IS
  -- �̶�����
  DAT_ϵͳʱ�� DATE;

  -- �������
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID VARCHAR2(50);

BEGIN
  BEGIN
  
    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;
  
    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ϣ��Ч��';
      GOTO �˳�;
    END IF;
  
    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    -- ���������������
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);
  
    -- �����ܲ���������
    STR_����ID := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 6);
  
    -- ������֤
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;
  
    /*
    ˵����
        1��ֻ��ʾ6�����ڵļ��鱨��
    */
    OPEN CUR_���ؽ���� FOR
      SELECT S.����ID AS ����ID,
             S.������ AS ������,
             J.���浥�� AS ���浥��,
             M.��� AS ˳���,
             M.ϸ������ AS ������Ŀ,
             M.ϸ����� AS ��Ŀ����,
             M.ϸ��ֵ AS ������,
             M.��λ AS ��λ,
             M.�ο�ֵ���� AS �ο���Χ,
             M.���� AS �������,
             NVL((SELECT ��Ա����
                   FROM ������Ŀ_��Ա���� Z
                  WHERE Z.�������� = J.��������
                    AND Z.��Ա���� = J.����ҽ������),
                 '') AS ����ҽ��,
             J.����ʱ�� AS ����ʱ��
        FROM ������_���� S, ������_��� J, ������_���_��ϸ M
       WHERE S.�������� = J.��������
         AND S.���뵥ID = J.���뵥ID
         AND S.�������� = M.��������
         AND J.���浥�� = M.���浥ID
         AND S.�������� = STR_ҽԺ����
         AND S.����ID = STR_����ID
         AND S.���״̬ = '�ѱ���'
         AND S.��Ŀ���� = 'S250403007' --��������Ŀ
         AND S.����ʱ�� > ADD_MONTHS(SYSDATE, -6)
       ORDER BY S.����ʱ�� DESC;
  
    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�!';
  
    -- ��������־��
    PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
                 STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
                 STR_ҽԺ����   => STR_ҽԺ����,
                 STR_���ܱ���   => STR_���ܱ���,
                 STR_�������   => STR_�������,
                 DAT_����ʱ��   => DAT_ϵͳʱ��,
                 STR_������   => STR_������Ϣ,
                 STR_��������   => NULL,
                 STR_ִ����     => STR_ƽ̨��ʶ,
                 DAT_ִ��ʱ��   => SYSDATE,
                 STR_ִ��״̬   => INT_����ֵ);
  
    RETURN;
  
  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  RETURN;

END PR_ƽ̨�ӿ�_�����������;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_���������ԤԼ
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_���������ԤԼ(STR_�������   IN VARCHAR2,
                                             CUR_���ؽ���� OUT SYS_REFCURSOR,
                                             INT_����ֵ     OUT INTEGER,
                                             STR_������Ϣ   OUT VARCHAR2) IS
  STR_ƽ̨���� VARCHAR2(50);
  DAT_ϵͳʱ�� DATE;

  -- �̶�����
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID VARCHAR2(50);

  -- �������
  ----������Ϣ
  STR_����     VARCHAR2(50);
  STR_�Ա�     VARCHAR2(50);
  STR_�������� VARCHAR2(50);
  STR_����״�� VARCHAR2(50);
  STR_��ϵ�绰 VARCHAR2(50);
  STR_��ͥ��ַ VARCHAR2(200);
  STR_������λ VARCHAR2(200);
  STR_���֤�� VARCHAR2(50);

  ----�Һ����
  STR_�Һ����     VARCHAR2(50);
  STR_�Һŵ���     VARCHAR2(50);
  STR_���ﲡ����   VARCHAR2(50);
  STR_�Һ����ͱ��� VARCHAR2(50);
  STR_�Һ��������� VARCHAR2(50);
  NUM_�Һŷ�       NUMBER(18, 3);
  NUM_����       NUMBER(18, 3);
  NUM_�ܷ���       NUMBER(18, 3);
  STR_�������     VARCHAR2(50);
  STR_�������ͱ��� VARCHAR2(50);
  STR_������������ VARCHAR2(50);
  STR_����״̬     VARCHAR2(50);
  STR_�Һ���Դ     VARCHAR2(50);
  STR_���ʽ     VARCHAR2(50);

  ----ҽ����Ŀ���
  STR_�������     VARCHAR2(50);
  STR_С�����     VARCHAR2(50);
  STR_��Ŀ����     VARCHAR2(50);
  STR_��Ŀ����     VARCHAR2(100);
  STR_��λ����     VARCHAR2(50);
  STR_��λ����     VARCHAR2(50);
  STR_ִ�п��ұ��� VARCHAR2(50);
  STR_ҽ����       VARCHAR2(50);
  STR_��ĿID       VARCHAR2(50);
  STR_���         VARCHAR2(50);
  STR_���뵥ID     VARCHAR2(50);

  TYPE REF_CURSOR_TYPE IS REF CURSOR;
  CUR_��ʱ���� REF_CURSOR_TYPE;
  STR_SQL      VARCHAR2(1000);

BEGIN

  -- ����������Ч����֤��
  IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '�Ƿ�����';
    GOTO �˳�;
  END IF;

  -- ����������Ч����֤��
  IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ϣ��Ч��';
    GOTO �˳�;
  END IF;

  -- �����ݳ�ʼ����
  SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;

  STR_�������ͱ��� := '1';
  STR_������������ := '�ֽ�';
  STR_����״̬     := '�ȴ�����';
  STR_�Һ���Դ     := 'ԤԼ';

  -- ���̶�����������
  STR_ƽ̨��ʶ   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 1);
  STR_��֤�ܳ�   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 2);
  STR_�ͻ��˱�ʶ := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 3);
  STR_���ܱ���   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 4);
  STR_ҽԺ����   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 5);

  -- �����ܲ���������
  STR_����ID := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 6);

  BEGIN
  
    BEGIN
      SELECT P.֧����ʽ, P.ƽ̨����
        INTO STR_���ʽ, STR_ƽ̨����
        FROM ƽ̨�ӿ�_ƽ̨���� P
       WHERE P.ƽ̨��ʶ = STR_ƽ̨��ʶ;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'δ�ҵ���Ч��ƽ̨��Ϣ��';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
  
    -- �������������֤��
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;
  
    --����ȡ�Һŷ��������Ϣ��
    BEGIN
      SELECT ���ͱ���, ��������, �Һŷ�, ����, �Һŷ� + ����, �������
        INTO STR_�Һ����ͱ���,
             STR_�Һ���������,
             NUM_�Һŷ�,
             NUM_����,
             NUM_�ܷ���,
             STR_�������
        FROM ������Ŀ_�Һ�����
       WHERE �������� = STR_ҽԺ����
         AND ���ͱ��� = '000002' --��Ѻ�
         AND ��Ч״̬ = '��Ч'
         AND ɾ����־ = '0';
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'δ�ҵ���Ч�ĹҺ����ͣ�';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
  
    --����ȡ������Ϣ��
    BEGIN
      SELECT ����,
             �Ա�,
             ��������,
             ����״��,
             �ֻ�����,
             ��ͥ��ַ,
             ������λ,
             ���֤��
        INTO STR_����,
             STR_�Ա�,
             STR_��������,
             STR_����״��,
             STR_��ϵ�绰,
             STR_��ͥ��ַ,
             STR_������λ,
             STR_���֤��
        FROM ������Ŀ_������Ϣ
       WHERE �������� = STR_ҽԺ����
         AND ����ID = STR_����ID;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'δ�ҵ���Ч�Ĳ�����Ϣ��';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
  
    -- ���������ݡ�
    ---- ���������ﲡ���š�
    PR_����_ȡ��ҵ������(STR_��������   => STR_ҽԺ����,
                  STR_���������� => '���ﲡ����',
                  STR_���ز����� => STR_���ﲡ����,
                  INT_����ֵ     => INT_����ֵ,
                  STR_������Ϣ   => STR_������Ϣ);
    IF INT_����ֵ <> 1 THEN
      STR_������Ϣ := '�������ﲡ����ʧ��,ԭ��:' + STR_������Ϣ;
      GOTO �˳�;
    END IF;
  
    ---- �������Һ���š�
    PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '26',
                PRM_��������   => STR_ҽԺ����,
                PRM_��������   => '1',
                PRM_����Ψһ�� => STR_�Һ����,
                PRM_ִ�н��   => INT_����ֵ,
                PRM_������Ϣ   => STR_������Ϣ);
    IF INT_����ֵ <> 0 THEN
      STR_������Ϣ := '�����Һ����ʧ��!';
      GOTO �˳�;
    END IF;
    ---- �������Һŵ��š�
    SELECT FU_����_��ȡ��ǰƱ�ݺ�(STR_ҽԺ����, STR_ƽ̨��ʶ, '4')
      INTO STR_�Һŵ���
      FROM DUAL;
  
    IF STR_�Һŵ��� = '�뵽����������Ʊ��' THEN
      STR_������Ϣ := '�ò���Ա�޹Һŵ���,��֪ͨ����������Ʊ��!';
      GOTO �˳�;
    END IF;
  
    -- �����ɹҺż�¼�� 
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
         ��֧�����)
      VALUES
        (STR_ҽԺ����,
         STR_����ID,
         STR_���ﲡ����,
         STR_�Һ����,
         STR_�Һŵ���,
         '', --�Һſ��ұ���
         NULL,
         STR_ƽ̨��ʶ,
         STR_�Һ����ͱ���,
         STR_ƽ̨��ʶ,
         DAT_ϵͳʱ��,
         '��',
         STR_�������,
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
         '', --�Һſ��ұ���
         STR_ƽ̨��ʶ,
         0,
         0,
         '-1',
         0);
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '����Һ�����ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      -- ��������֧���¼��
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
        (STR_ҽԺ����,
         STR_�Һŵ���,
         NUM_�ܷ���,
         STR_���ʽ,
         '�Һ�',
         STR_ƽ̨��ʶ,
         STR_ƽ̨����,
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
    
    EXCEPTION
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := STR_������Ϣ || SQLERRM;
        GOTO �˳�;
    END;
  
    ---- ������ҽ���š�
    PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '8',
                PRM_��������   => STR_ҽԺ����,
                PRM_��������   => '1',
                PRM_����Ψһ�� => STR_ҽ����,
                PRM_ִ�н��   => INT_����ֵ,
                PRM_������Ϣ   => STR_������Ϣ);
    IF INT_����ֵ <> 0 THEN
      STR_������Ϣ := '����ҽ����ʧ��!';
      GOTO �˳�;
    END IF;
  
    STR_SQL := 'SELECT �������,
             С�����,
             ��λ����,
             ��λ����,
             ��Ŀ����,
             ��Ŀ����,
             ����ִ�п��ұ���,
             �������,
             ����� FROM ������Ŀ_�����ֵ�
       WHERE �������� = ''' || STR_ҽԺ���� || '''
         AND ��Ŀ���� in ( ''S250403007'')';--��д���к��������Ŀ����
    OPEN CUR_��ʱ���� FOR STR_SQL;
  
    LOOP
      FETCH CUR_��ʱ����
        INTO STR_�������,
             STR_С�����,
             STR_��λ����,
             STR_��λ����,
             STR_��Ŀ����,
             STR_��Ŀ����,
             STR_ִ�п��ұ���,
             STR_�������,
             NUM_�ܷ���;
      EXIT WHEN CUR_��ʱ����%NOTFOUND;
    
      IF STR_��Ŀ���� = 'S250403007' THEN
        ---- ���������뵥ID��
        PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '33',
                    PRM_��������   => STR_ҽԺ����,
                    PRM_��������   => '1',
                    PRM_����Ψһ�� => STR_���뵥ID,
                    PRM_ִ�н��   => INT_����ֵ,
                    PRM_������Ϣ   => STR_������Ϣ);
        IF INT_����ֵ <> 0 THEN
          STR_������Ϣ := '�������뵥IDʧ��!';
          GOTO �˳�;
        END IF;
      ELSE
        STR_���뵥ID := '';
      END IF;
    
      ----��������ĿID��
      SELECT SYS_GUID() INTO STR_��ĿID FROM DUAL;
    
      ----��������š�
      SELECT SEQ_����ҽ��_���.NEXTVAL INTO STR_��� FROM DUAL;
    
      --����������ҽ����
      INSERT INTO �������_����ҽ��
        (��������,
         ���,
         ����ID,
         ���ﲡ����,
         ҽ����,
         �������ұ���,
         ������ұ���,
         ִ�п��ұ���,
         ���˿��ұ���,
         ����ҽ������,
         ����Ա����,
         ����Ա����,
         ¼��ʱ��,
         �������,
         С�����,
         ��Ŀ����,
         ��Ŀ����,
         ���,
         ����,
         ��������,
         ��������,
         ����,
         ����,
         ��λ����,
         ��λ����,
         �÷�����,
         �÷�����,
         Ƶ�ʱ���,
         Ƶ������,
         ��ʼʱ��,
         ����,
         ҽ��״̬,
         �����,
         �������,
         ��������ID,
         �Һ����,
         �������,
         ����,
         Ƥ�Ա�־,
         �շ�״̬,
         ���۷�ʽ,
         �ܽ��,
         ����,
         ��ĿID,
         ҽ������,
         ҽ������)
      VALUES
        (STR_ҽԺ����,
         STR_���,
         STR_����ID,
         STR_���ﲡ����,
         STR_ҽ����,
         '', --�������ұ���
         '', --������ұ���
         STR_ִ�п��ұ���,
         '', --���˿��ұ���
         STR_ƽ̨��ʶ,
         STR_ƽ̨��ʶ,
         STR_ƽ̨����,
         SYSDATE,
         STR_�������,
         STR_С�����,
         STR_��Ŀ����,
         STR_��Ŀ����,
         SEQ_����ҽ��_���.NEXTVAL,
         1,
         '37',
         '��',
         1,
         1,
         STR_��λ����,
         STR_��λ����,
         '0000000002',
         '����',
         '1003',
         'һ����',
         SYSDATE,
         '��',
         '��Ч',
         1,
         1,
         STR_���뵥ID,
         STR_�Һ����,
         SEQ_����ҽ��_�������.NEXTVAL,
         1,
         '-1',
         '����δ�շ�',
         'ҽ������',
         NUM_�ܷ���,
         NUM_�ܷ���,
         STR_��ĿID,
         STR_��Ŀ����,
         '0000003920');
    
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '��������ҽ��ʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      --����������ҽ����Ŀ��
      INSERT INTO �������_����ҽ����Ŀ
        (��������,
         ����ID,
         ���ﲡ����,
         ҽ����,
         ��ĿID,
         �������,
         С�����,
         ��Ŀ����,
         ��Ŀ����,
         �������,
         ����,
         ��������,
         ��������,
         ����,
         ����,
         ����,
         �ܽ��,
         ��λ����,
         ��λ����,
         �÷�����,
         �÷�����,
         ִ�п��ұ���,
         ����ʱ��,
         ��λ����,
         С��λ����,
         �Ƽ�ID,
         ���۷�ʽ,
         ����Ա����,
         ����ҽ������,
         �������ұ���,
         ���,
         �������)
      VALUES
        (STR_ҽԺ����,
         STR_����ID,
         STR_���ﲡ����,
         STR_ҽ����,
         STR_��ĿID,
         STR_�������,
         STR_С�����,
         STR_��Ŀ����,
         STR_��Ŀ����,
         1,
         1,
         '37',
         '��',
         1,
         1,
         NUM_�ܷ���,
         NUM_�ܷ���,
         STR_��λ����,
         STR_��λ����,
         '0000000002',
         '����',
         STR_ִ�п��ұ���,
         SYSDATE,
         0,
         NUM_�ܷ���,
         SYS_GUID(),
         'ҽ������',
         STR_ƽ̨��ʶ,
         STR_ƽ̨��ʶ,
         '', --�������ұ���
         STR_���,
         STR_�������);
      INT_����ֵ := SQL%ROWCOUNT;
      IF INT_����ֵ = 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '��������ҽ����Ŀʧ�ܣ�';
        GOTO �˳�;
      END IF;
    
      IF STR_��Ŀ���� = 'S250403007' THEN
        --�����ɼ�����_���롿
        INSERT INTO ������_����
          (��������,
           ���뵥ID,
           ��Ŀ����,
           ��Ŀ����,
           ִ�п��ұ���,
           ҽ������,
           ����ʱ��,
           ���״̬,
           ΨһID,
           ID����,
           ҽ����,
           ����ID,
           ������,
           �Һ����,
           �ײ�����,
           ����)
        VALUES
          (STR_ҽԺ����,
           STR_���뵥ID,
           STR_��Ŀ����,
           STR_��Ŀ����,
           STR_ִ�п��ұ���,
           STR_ƽ̨��ʶ,
           SYSDATE,
           'δ����',
           SYS_GUID(),
           '����',
           STR_ҽ����,
           STR_����ID,
           STR_���ﲡ����,
           STR_�Һ����,
           1,
           '����');
        INT_����ֵ := SQL%ROWCOUNT;
        IF INT_����ֵ = 0 THEN
          INT_����ֵ   := -1;
          STR_������Ϣ := '�������������ʧ�ܣ�';
          GOTO �˳�;
        END IF;
      END IF;
    
    END LOOP;
  
    PR_ƽ̨�ӿ�_������ɷ�����(STR_�������   => STR_������� || '|' || STR_���ﲡ����,
                    CUR_���ؽ���� => CUR_���ؽ����,
                    INT_����ֵ     => INT_����ֵ,
                    STR_������Ϣ   => STR_������Ϣ);
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

  INT_����ֵ   := 0;
  STR_������Ϣ := '����ɹ�!';

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  COMMIT;
  RETURN;

  --���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  --��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  ROLLBACK;
  RETURN;

END PR_ƽ̨�ӿ�_���������ԤԼ;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_���������ʷ��ѯ
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_���������ʷ��ѯ(STR_�������   IN VARCHAR2,
                                             CUR_���ؽ���� OUT SYS_REFCURSOR,
                                             INT_����ֵ     OUT INTEGER,
                                             STR_������Ϣ   OUT VARCHAR2) IS
  -- �̶�����
  DAT_ϵͳʱ�� DATE;

  -- �������
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID     VARCHAR2(50);
  STR_���ﲡ���� VARCHAR2(50);
  STR_����״̬   VARCHAR2(50);

BEGIN
  BEGIN

    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;

    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ϣ��Ч��';
      GOTO �˳�;
    END IF;

    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;

    -- ���������������
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);

    -- �����ܲ���������
    STR_����ID     := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_���ﲡ���� := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 7);
    STR_����״̬   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 8);

    -- ������֤
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;

    IF STR_���ﲡ���� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�����ﲡ���ţ�';
      GOTO �˳�;
    END IF;

    IF STR_����״̬ IS NULL
       OR STR_����״̬ NOT IN ('ȫ��', '������', '�ѽ���') THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�ľ���״̬��';
      GOTO �˳�;
    END IF;

    /*
    ˵����
        1��ֻ��ʾ1���ڵľ����¼
    */
    OPEN CUR_���ؽ���� FOR
      SELECT P.����ID AS ����ID,
             P.���ﲡ���� AS ���ﲡ����,
             P.������ұ��� AS ������ұ���,
             K.�������� AS �����������,
             P.����ҽ������ AS ����ҽ������,
             R.��Ա���� AS ����ҽ������,
             P.����ʱ�� AS ����ʱ��,
             (CASE
               WHEN P.����״̬ = '�ȴ�����' THEN
                '������'
               WHEN P.����״̬ IN ('���ڽ���', '��ɽ���') THEN
                '�ѽ���'
             END) AS ԤԼ״̬
        FROM �������_�ҺŵǼ� P
        LEFT JOIN ������Ŀ_�������� K
          ON P.�������� = K.��������
         AND P.������ұ��� = K.���ұ���
        LEFT JOIN ������Ŀ_��Ա���� R
          ON P.�������� = R.��������
         AND P.����ҽ������ = R.��Ա����
       WHERE P.�������� = STR_ҽԺ����
         AND P.����ID = STR_����ID
         AND ((STR_����״̬ = 'ȫ��' AND P.����״̬ = P.����״̬) OR
             (STR_����״̬ = '������' AND P.����״̬ = '�ȴ�����') OR
             (STR_����״̬ = '�ѽ���' AND P.����״̬ IN ('���ڽ���', '��ɽ���')))
         AND P.�Һ�ʱ�� >= ADD_MONTHS(SYSDATE, -1);

    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�!';

    -- ��������־��
    PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
                 STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
                 STR_ҽԺ����   => STR_ҽԺ����,
                 STR_���ܱ���   => STR_���ܱ���,
                 STR_�������   => STR_�������,
                 DAT_����ʱ��   => DAT_ϵͳʱ��,
                 STR_������   => STR_������Ϣ,
                 STR_��������   => STR_����ID,
                 STR_ִ����     => STR_ƽ̨��ʶ,
                 DAT_ִ��ʱ��   => SYSDATE,
                 STR_ִ��״̬   => INT_����ֵ);

    RETURN;

  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  RETURN;

END PR_ƽ̨�ӿ�_���������ʷ��ѯ;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_�Ű���Ϣ
prompt ===============================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_�Ű���Ϣ(STR_�������   IN VARCHAR2,
                                         CUR_���ؽ���� OUT SYS_REFCURSOR,
                                         INT_����ֵ     OUT INTEGER,
                                         STR_������Ϣ   OUT VARCHAR2) IS
  -- �̶�����
  DAT_ϵͳʱ�� DATE;

  --�������
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_���ұ��� VARCHAR2(50);
  STR_ҽ������ VARCHAR2(50);
  STR_�Ű����� VARCHAR2(50);

  STR_�޺����� VARCHAR2(50);

BEGIN
  BEGIN
  
    -- ������֤
  
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;
  
    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    -- ���������������
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);
  
    -- �����ܲ���������
    STR_���ұ��� := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_ҽ������ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 7);
    STR_�Ű����� := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 8);
  
    -- ����ȡƽ̨��Ϣ��
    BEGIN
      SELECT P.�޺�����
        INTO STR_�޺�����
        FROM ƽ̨�ӿ�_ƽ̨���� P
       WHERE P.ƽ̨��ʶ = STR_ƽ̨��ʶ;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'δ�ҵ���Ч��ƽ̨��Ϣ��';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
  
    -- ������У�顿
    IF STR_���ұ��� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч�Ŀ��ұ��룡��';
      GOTO �˳�;
    END IF;
    IF STR_ҽ������ IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч��ҽ�����룡��';
      GOTO �˳�;
    END IF;
    IF STR_�Ű����� IS NULL
       OR FU_����ת����(STR_�Ű�����) IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч���Ű����ڣ���';
      GOTO �˳�;
    END IF;
  
    -- ���������ݡ�
    /*
    ˵����
        1)ֻ�ܰ����ѯ�Ű��¼,����ֻ�ܲ�ѯ���켰�Ժ���Ű��¼��
        2)��ѯ��������ҽ���Ű���Ϣʱ��ҽ�������봫��-1��
        3)����ʣ�����ʱ���޷�ԤԼ
    */
    OPEN CUR_���ؽ���� FOR
      SELECT S.�հ�α�ʶ AS ��α�ʶ,
             D.���ұ���,
             D.��������,
             D.ҽ������,
             D.ҽ������,
             D.�Һ����ͱ���,
             D.�Һ���������,
             G.�Һŷ�,
             G.����,
             D.�Ű�����,
             S.��ʼʱ��,
             S.����ʱ��,
             S.�޺���,
             DECODE(S.�޺���,
                    -1,
                    -1,
                    S.�޺��� - NVL(S.�ѹҺ���, 0) -
                    (SELECT COUNT(1)
                       FROM �������_ԤԼ�Һ�
                      WHERE �հ�α�ʶ = S.�հ�α�ʶ
                        AND (ȥ���־ = 'ռ��' and ��ʱʱ�� > SYSDATE))) AS ʣ�����
        FROM �������_���Ű�ʱ�α� S,
             �������_�����Ű��¼ D,
             ������Ŀ_�Һ�����     G
       WHERE S.�������� = D.��������
         AND S.�Ű���� = D.�Ű����
         AND S.��¼ID = D.��¼ID
         AND D.�������� = G.��������
         AND D.�Һ����ͱ��� = G.���ͱ���
         AND D.�������� = STR_ҽԺ����
         AND D.���ұ��� = STR_���ұ���
         AND D.ҽ������ = DECODE(STR_ҽ������, -1, D.ҽ������, STR_ҽ������)
         AND S.�޺����ͱ��� = STR_�޺�����
         AND TO_CHAR(D.�Ű�����, 'yyyy-mm-dd') = STR_�Ű�����
         AND D.�Ű����� >= TRUNC(SYSDATE);
  
    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�!';
  
    -- ��������־��
    PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
                 STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
                 STR_ҽԺ����   => STR_ҽԺ����,
                 STR_���ܱ���   => STR_���ܱ���,
                 STR_�������   => STR_�������,
                 DAT_����ʱ��   => DAT_ϵͳʱ��,
                 STR_������   => STR_������Ϣ,
                 STR_��������   => NULL,
                 STR_ִ����     => STR_ƽ̨��ʶ,
                 DAT_ִ��ʱ��   => SYSDATE,
                 STR_ִ��״̬   => INT_����ֵ);
  
    RETURN;
  
  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  RETURN;

END PR_ƽ̨�ӿ�_�Ű���Ϣ;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_ҽ���б�
prompt ===============================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_ҽ���б�(STR_�������   IN VARCHAR2,
                                         CUR_���ؽ���� OUT SYS_REFCURSOR,
                                         INT_����ֵ     OUT INTEGER,
                                         STR_������Ϣ   OUT VARCHAR2) IS
  -- �̶�����
  DAT_ϵͳʱ�� DATE;

  --�������
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_���ұ��� VARCHAR2(50);

BEGIN
  BEGIN

    -- ������֤

    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;

    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;

    -- ���������������
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);

    -- �����ܲ���������
    STR_���ұ��� := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 6);

    -- ����У��
    IF STR_���ұ��� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ŀ��ұ��룡';
      GOTO �˳�;
    END IF;

    /*
    ˵����
        1��ֻ��ʾ��Ч��ҽ���б�
    */
    OPEN CUR_���ؽ���� FOR
      SELECT ��Ա����, ��Ա����
        FROM ������Ŀ_��Ա����
       WHERE �������� = STR_ҽԺ����
         AND ��Ч״̬ = '��Ч'
         AND ɾ����־ = '0'
         AND ��Ա���ұ��� LIKE '%' || STR_���ұ��� || ',%';

    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�!';

    -- ��������־��
    PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
                 STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
                 STR_ҽԺ����   => STR_ҽԺ����,
                 STR_���ܱ���   => STR_���ܱ���,
                 STR_�������   => STR_�������,
                 DAT_����ʱ��   => DAT_ϵͳʱ��,
                 STR_������   => STR_������Ϣ,
                 STR_��������   => STR_���ұ���,
                 STR_ִ����     => STR_ƽ̨��ʶ,
                 DAT_ִ��ʱ��   => SYSDATE,
                 STR_ִ��״̬   => INT_����ֵ);

    RETURN;

  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  RETURN;

END PR_ƽ̨�ӿ�_ҽ���б�;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_ԤԼ�ҺŵǼ�
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_ԤԼ�ҺŵǼ�(STR_�������   IN VARCHAR2,
                                           CUR_���ؽ���� OUT SYS_REFCURSOR,
                                           INT_����ֵ     OUT INTEGER,
                                           STR_������Ϣ   OUT VARCHAR2) IS

  DAT_ϵͳʱ�� DATE;

  -- �̶�����
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID   VARCHAR2(50);
  STR_��α�ʶ VARCHAR2(50);

  -- �������
  STR_������       VARCHAR2(50);
  DAT_��������ʱ�� DATE;
  STR_������ע     VARCHAR2(50);
  NUM_Ӧ�����     NUMBER(18, 3);

  STR_ԤԼ����     VARCHAR2(50);
  STR_�Ű��¼ID   VARCHAR2(50);
  DAT_�Ű�����     DATE;
  STR_�Һſ��ұ��� VARCHAR2(50);
  STR_�Һſ������� VARCHAR2(50);
  STR_�Һſ���λ�� VARCHAR2(50);
  STR_�Һ�ҽ������ VARCHAR2(50);
  STR_�Һ�ҽ������ VARCHAR2(50);
  STR_�Һ����ͱ��� VARCHAR2(50);
  STR_�Һ��������� VARCHAR2(50);

  NUM_�Һŷ�   NUMBER(18, 3);
  NUM_����   NUMBER(18, 3);
  STR_������� VARCHAR2(50);

  STR_����     VARCHAR2(50);
  STR_�Ա�     VARCHAR2(50);
  STR_�������� VARCHAR2(50);
  STR_����״�� VARCHAR2(50);
  STR_��ϵ�绰 VARCHAR2(50);
  STR_��ͥ��ַ VARCHAR2(200);
  STR_������λ VARCHAR2(200);
  STR_���֤�� VARCHAR2(50);

  STR_ԤԼʱ�α��� VARCHAR2(50);
  STR_ԤԼʱ�ο�ʼ VARCHAR2(50);
  STR_ԤԼʱ�ν��� VARCHAR2(50);

BEGIN
  BEGIN
    -- ����������Ч����֤��
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;
  
    -- ����������Ч����֤��
    IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ϣ��Ч��';
      GOTO �˳�;
    END IF;
  
    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    -- ���̶�����������
  
    STR_ƽ̨��ʶ   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 5);
  
    -- �����ܲ���������
  
    STR_����ID   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_��α�ʶ := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 7);
  
    -- �������������֤��
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;
    IF STR_��α�ʶ IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч�İ�α�ʶ��';
      GOTO �˳�;
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣0��' || SQLERRM;
      GOTO �˳�;
  END;
  -- ����ȡ�������ݡ�
  -- 1)�Ű��¼
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
       AND D.�������� = STR_ҽԺ����
       AND S.�հ�α�ʶ = STR_��α�ʶ
       AND TO_DATE(TO_CHAR(D.�Ű�����, 'yyyy/mm/dd'), 'yyyy/mm/dd') >=
           TO_DATE(TO_CHAR(SYSDATE, 'yyyy/mm/dd'), 'yyyy/mm/dd');
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'δ�ҵ���Ч���Ű��¼���Դ���㣡';
      GOTO �˳�;
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  -- 1.1)ԤԼ��¼
  -- �Ѿ�ԤԼ�ģ������ٴ�ԤԼ
  SELECT COUNT(1)
    INTO INT_����ֵ
    FROM �������_ԤԼ�Һ�
   WHERE �������� = STR_ҽԺ����
     AND ����ID = STR_����ID
     AND �հ�α�ʶ = STR_��α�ʶ
     AND ȥ���־ IN ('ԤԼ', 'ռ��', '����')
     and (ȥ���־ = 'ռ��' and ��ʱʱ�� > sysdate);

  IF INT_����ֵ > 0 THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '�Ѿ�����ԤԼ��¼�������ظ�ԤԼ��';
    GOTO �˳�;
  END IF;

  -- 2)�Һ�����
  BEGIN
    SELECT ���ͱ���, ��������, �Һŷ�, ����, �������
      INTO STR_�Һ����ͱ���,
           STR_�Һ���������,
           NUM_�Һŷ�,
           NUM_����,
           STR_�������
      FROM ������Ŀ_�Һ�����
     WHERE �������� = STR_ҽԺ����
       AND ���ͱ��� = STR_�Һ����ͱ���
       AND ��Ч״̬ = '��Ч'
       AND ɾ����־ = '0';
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'δ�ҵ���Ч�ĹҺ����ͣ�';
      GOTO �˳�;
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  -- 3)������Ϣ
  BEGIN
    SELECT ����,
           �Ա�,
           ��������,
           ����״��,
           �ֻ�����,
           ��ͥ��ַ,
           ������λ,
           ���֤��
      INTO STR_����,
           STR_�Ա�,
           STR_��������,
           STR_����״��,
           STR_��ϵ�绰,
           STR_��ͥ��ַ,
           STR_������λ,
           STR_���֤��
      FROM ������Ŀ_������Ϣ
     WHERE �������� = STR_ҽԺ����
       AND ����ID = STR_����ID;
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'δ�ҵ���Ч�Ĳ�����Ϣ��';
      GOTO �˳�;
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  -- ���������ݡ�
  -- 1)���ɶ�����
  PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '6001',
              PRM_��������   => STR_ҽԺ����,
              PRM_��������   => '1',
              PRM_����Ψһ�� => STR_������,
              PRM_ִ�н��   => INT_����ֵ,
              PRM_������Ϣ   => STR_������Ϣ);
  IF INT_����ֵ <> 0 THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '����������ʧ��!';
    GOTO �˳�;
  END IF;

  -- 2)���ɹ���ʱ��
  SELECT SYSDATE + (1 / (24 * 60)) * 15 INTO DAT_��������ʱ�� FROM DUAL;

  -- 3)���ɶ�����ע
  STR_������ע := '����15���������֧���������Զ�ȡ��';

  -- 4)���ɽ��
  NUM_Ӧ����� := NUM_�Һŷ� + NUM_����;

  -- 5)����ԤԼ����
  SELECT SEQ_�������_ԤԼ�Һ�_ΨһID.NEXTVAL INTO STR_ԤԼ���� FROM DUAL;

  -- ���������ݡ�
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
      (STR_ҽԺ����,
       STR_ԤԼ����,
       STR_����,
       STR_�Ա�,
       STR_��������,
       STR_����״��,
       STR_��ϵ�绰,
       STR_��ͥ��ַ,
       STR_������λ,
       STR_���֤��,
       FU_ͨ��_����_ת��_��ƴ(STR_����),
       FU_ͨ��_����_ת��_���(STR_����),
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
      INT_����ֵ   := -1;
      STR_������Ϣ := '����ԤԼ��¼ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
    -- ���붩��
    INSERT INTO ƽ̨�ӿ�_����
      (��ˮ��,
       ƽ̨��ʶ,
       �ͻ��˱�ʶ,
       ҽԺ����,
       ����ID,
       ��������,
       ������,
       ��������,
       ����ʱ��,
       Ӧ�����,
       �Żݽ��,
       ʵ�ս��,
       ����ʱ��,
       ����״̬,
       ������,
       ����ʱ��)
    VALUES
      (SEQ_ƽ̨�ӿ�_����_��ˮ��.NEXTVAL,
       STR_ƽ̨��ʶ,
       STR_�ͻ��˱�ʶ,
       STR_ҽԺ����,
       STR_����ID,
       STR_ԤԼ����,
       STR_������,
       'ԤԼ�Һ�',
       DAT_ϵͳʱ��,
       NUM_Ӧ�����,
       0,
       0,
       DAT_��������ʱ��,
       '��֧��',
       STR_ƽ̨��ʶ,
       DAT_ϵͳʱ��);
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '���涩��ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
    -- ���붩����ϸ
    -- �Һŷ�
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
      (SEQ_ƽ̨�ӿ�_������ϸ_��ˮ��.NEXTVAL,
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
      INT_����ֵ   := -1;
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
      (SEQ_ƽ̨�ӿ�_������ϸ_��ˮ��.NEXTVAL,
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
      INT_����ֵ   := -1;
      STR_������Ϣ := '���涩����ϸʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  -- �����ؽ����
  OPEN CUR_���ؽ���� FOR
    SELECT STR_����ID       AS ����ID,
           STR_������       AS ������,
           NUM_Ӧ�����     AS Ӧ�����,
           DAT_��������ʱ�� AS ����ʱ��,
           STR_������ע     AS ��ע
      FROM DUAL;

  INT_����ֵ   := 0;
  STR_������Ϣ := '����ɹ�!';

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => STR_������,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  COMMIT;
  RETURN;

  --���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  --��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  ROLLBACK;
  RETURN;

END PR_ƽ̨�ӿ�_ԤԼ�ҺŵǼ�;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_ԤԼ�Һ�ȡ��
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_ԤԼ�Һ�ȡ��(STR_�������   IN VARCHAR2,
                                           CUR_���ؽ���� OUT SYS_REFCURSOR,
                                           INT_����ֵ     OUT INTEGER,
                                           STR_������Ϣ   OUT VARCHAR2) IS

  DAT_ϵͳʱ�� DATE;
  STR_ƽ̨���� VARCHAR2(50);

  -- �̶�����
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_ȡ����Դ VARCHAR2(50); --1΢�� 2����
  STR_����ID   VARCHAR2(50);
  STR_ԤԼ���� VARCHAR2(50);

  -- �������

  STR_�Ű�ID       VARCHAR2(50);
  STR_�Һ����     VARCHAR2(50);
  STR_�Һŵ���     VARCHAR2(50);
  STR_���ﲡ����   VARCHAR2(50);
  NUM_�Һŷ�       NUMBER(18, 2);
  NUM_����       NUMBER(18, 2);
  NUM_�ܷ���       NUMBER(18, 2);
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
  
    -- ������ʼ��
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    STR_�������ͱ��� := '1';
    STR_������������ := '�ֽ�';
    STR_����״̬     := '�ȴ�����';
    STR_�Һ���Դ     := 'ԤԼ';
  
    -- ���̶�����������
    STR_ƽ̨��ʶ   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 5);
  
    -- �����ܲ���������
    STR_ȡ����Դ := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_����ID   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 7);
    STR_ԤԼ���� := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 8);
  
    if STR_ȡ����Դ = '1' then
      -- ����������Ч����֤��
      IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '�Ƿ�����';
        GOTO �˳�;
      END IF;
    
      -- ����������Ч����֤��
      IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '��������Ϣ��Ч��';
        GOTO �˳�;
      END IF;
    end if;
  
    -- ����ȡƽ̨��Ϣ��
    BEGIN
      SELECT P.֧����ʽ, P.ƽ̨����
        INTO STR_���ʽ, STR_ƽ̨����
        FROM ƽ̨�ӿ�_ƽ̨���� P
       WHERE P.ƽ̨��ʶ = STR_ƽ̨��ʶ;
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'δ�ҵ���Ч��ƽ̨��Ϣ��';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
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
       WHERE G.�������� = STR_ҽԺ����
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
    PR_����_ȡ��ҵ������(STR_��������   => STR_ҽԺ����,
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
                PRM_��������   => STR_ҽԺ����,
                PRM_��������   => '1',
                PRM_����Ψһ�� => STR_�Һ����,
                PRM_ִ�н��   => INT_����ֵ,
                PRM_������Ϣ   => STR_������Ϣ);
    IF INT_����ֵ <> 0 THEN
      STR_������Ϣ := '�����Һ����ʧ��!';
      GOTO �˳�;
    END IF;
  
    -- �������Һŵ��š�
    SELECT FU_����_��ȡ��ǰƱ�ݺ�(STR_ҽԺ����, STR_ƽ̨��ʶ, '4')
      INTO STR_�Һŵ���
      FROM DUAL;
  
    IF STR_�Һŵ��� = '�뵽����������Ʊ��' THEN
      STR_������Ϣ := '�ò���Ա�޹Һŵ���,��֪ͨ����������Ʊ��!';
      GOTO �˳�;
    END IF;
  
    -- ���ɹҺż�¼
  
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
      (STR_ҽԺ����,
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
      (STR_ҽԺ����,
       STR_�Һŵ���,
       NUM_�ܷ���,
       STR_���ʽ,
       '�Һ�',
       STR_ƽ̨��ʶ,
       STR_ƽ̨����,
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
     WHERE �������� = STR_ҽԺ����
       AND ����ID = STR_ԤԼ����;
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '����ԤԼ״̬ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
    -- ���¶���
    UPDATE ƽ̨�ӿ�_����
       SET ���ﲡ���� = STR_���ﲡ����
     WHERE �������� = STR_ԤԼ����
       AND ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND ҽԺ���� = STR_ҽԺ����
       AND ����ID = STR_����ID
       AND �������� = 'ԤԼ�Һ�'
       AND ״̬ = '0';
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '���¶���״̬ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := STR_������Ϣ || SQLERRM;
      GOTO �˳�;
  END;

  -- �����ؽ����
  INT_����ֵ   := 0;
  STR_������Ϣ := '����ɹ�!';
  OPEN CUR_���ؽ���� FOR
    SELECT STR_�Һŵ��� as �Һŵ��� FROM DUAL;
  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => STR_ԤԼ����,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  COMMIT;
  RETURN;

  --���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  --��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  ROLLBACK;
  RETURN;

END PR_ƽ̨�ӿ�_ԤԼ�Һ�ȡ��;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_ԤԼ�Һ�ȡ��
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_ԤԼ�Һ�ȡ��(STR_�������   IN VARCHAR2,
                                           CUR_���ؽ���� OUT SYS_REFCURSOR,
                                           INT_����ֵ     OUT INTEGER,
                                           STR_������Ϣ   OUT VARCHAR2) IS

  DAT_ϵͳʱ�� DATE;

  -- �̶�����
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID       VARCHAR2(50);
  STR_ԤԼ����     VARCHAR2(50);
  STR_ƽ̨�˿��   VARCHAR2(50);
  STR_ƽ̨�˿�ʱ�� VARCHAR2(50);

  -- �������
  STR_������   VARCHAR2(50);
  STR_ȥ���־ VARCHAR2(50);
  STR_����״̬ VARCHAR2(50);

BEGIN
  BEGIN
    -- ����������Ч����֤��
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;
  
    -- ����������Ч����֤��
    IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ϣ��Ч��';
      GOTO �˳�;
    END IF;
  
    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    -- ���̶�����������
  
    STR_ƽ̨��ʶ   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 5);
  
    -- �����ܲ���������
  
    STR_����ID       := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_ԤԼ����     := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 7);
    STR_������       := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 8);
    STR_ƽ̨�˿��   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 9);
    STR_ƽ̨�˿�ʱ�� := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 10);
  
    -- ��������������֤��
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;
    IF STR_ԤԼ���� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч��ԤԼ���ţ�';
      GOTO �˳�;
    END IF;
    IF STR_������ IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч�Ķ����ţ�';
      GOTO �˳�;
    END IF;
  
    -- ��֤ԤԼ��
    BEGIN
      SELECT ȥ���־
        INTO STR_ȥ���־
        FROM �������_ԤԼ�Һ�
       WHERE �������� = STR_ҽԺ����
         AND ����ID = STR_ԤԼ����
         AND (ȥ���־ = 'ԤԼ' OR ȥ���־ = 'ռ��');
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '��Ч��ԤԼ����';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
  
    -- ��֤����
    BEGIN
      SELECT ����״̬
        INTO STR_����״̬
        FROM ƽ̨�ӿ�_����
       WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
         AND �ͻ��˱�ʶ = STR_�ͻ��˱�ʶ
         AND ����ID = STR_����ID
         AND ������ = STR_������
         AND �������� = STR_ԤԼ����
         AND (����״̬ = '��֧��' OR ����״̬ = '��֧��')
         AND ״̬ = '0';
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '��Ч�Ķ�����';
        GOTO �˳�;
      WHEN OTHERS THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
        GOTO �˳�;
    END;
  
    IF STR_����״̬ = '��֧��' THEN
      IF STR_ƽ̨�˿�� IS NULL THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '��������Ч���˿�ţ�';
        GOTO �˳�;
      END IF;
      IF STR_ƽ̨�˿�ʱ�� IS NULL OR FU_����ת����(STR_ƽ̨�˿�ʱ��) IS NULL THEN
        INT_����ֵ   := -1;
        STR_������Ϣ := '��������Ч���˿�ʱ�䣡';
        GOTO �˳�;
      END IF;
    END IF;
  
    -- �����ܴ���
  
    -- ����ԤԼ��
    UPDATE �������_ԤԼ�Һ�
       SET ȥ���־ = '����'
     WHERE �������� = STR_ҽԺ����
       AND ����ID = STR_ԤԼ����
       AND (ȥ���־ = 'ԤԼ' OR ȥ���־ = 'ռ��');
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '����ԤԼ��ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
    -- ���¶���״̬
    UPDATE ƽ̨�ӿ�_����
       SET ����״̬     = DECODE(STR_����״̬,
                             '��֧��',
                             '��ȡ��',
                             '��֧��',
                             '���˿�'),
           ƽ̨�˿��   = DECODE(STR_����״̬, '��֧��', STR_ƽ̨�˿��, ''),
           ƽ̨�˿�ʱ�� = DECODE(STR_����״̬,
                           '��֧��',
                           to_date(STR_ƽ̨�˿�ʱ��, 'yyyy/mm/dd hh24:mi:ss'),
                           ''),
           ������       = STR_ƽ̨��ʶ,
           ����ʱ��     = DAT_ϵͳʱ��
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND �ͻ��˱�ʶ = STR_�ͻ��˱�ʶ
       AND ����ID = STR_����ID
       AND ������ = STR_������
       AND �������� = STR_ԤԼ����
       AND (����״̬ = '��֧��' OR ����״̬ = '��֧��');
  
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

  -- �����ؽ����
  OPEN CUR_���ؽ���� FOR
    SELECT STR_����ID AS ����ID,
           STR_������ AS ������,
           STR_ԤԼ���� AS ԤԼ����,
           '' AS ��ע
      FROM DUAL;

  INT_����ֵ   := 0;
  STR_������Ϣ := '����ɹ�!';

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => STR_ԤԼ����,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  COMMIT;
  RETURN;

  --���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  --��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  ROLLBACK;
  RETURN;

END PR_ƽ̨�ӿ�_ԤԼ�Һ�ȡ��;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_ԤԼ�Һ�֧��
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_ԤԼ�Һ�֧��(STR_�������   IN VARCHAR2,
                                           CUR_���ؽ���� OUT SYS_REFCURSOR,
                                           INT_����ֵ     OUT INTEGER,
                                           STR_������Ϣ   OUT VARCHAR2) IS

  DAT_ϵͳʱ�� DATE;

  -- �̶�����
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID       VARCHAR2(50);
  STR_��������     VARCHAR2(50);
  STR_������       VARCHAR2(50);
  STR_ƽ̨������   VARCHAR2(50);
  STR_ƽ̨���׺�   VARCHAR2(50);
  STR_����Ӧ����� VARCHAR2(50);
  STR_ƽ̨ʵ�ս�� VARCHAR2(50);
  STR_ƽ̨����ʱ�� VARCHAR2(50);
  STR_ƽ̨����ʱ�� VARCHAR2(50);

  -- �������
  STR_ԤԼ���� VARCHAR2(50);
  STR_��α�ʶ VARCHAR2(50);

BEGIN
  BEGIN
    -- ����������Ч����֤��
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;
  
    -- ����������Ч����֤��
    IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ϣ��Ч��';
      GOTO �˳�;
    END IF;
  
    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
    STR_�������� := 'ԤԼ�Һ�';
  
    -- ���̶�����������
  
    STR_ƽ̨��ʶ   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 5);
  
    -- �����ܲ���������
  
    STR_����ID       := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_������       := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 7);
    STR_ƽ̨������   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 8);
    STR_ƽ̨����ʱ�� := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 9);
    STR_ƽ̨���׺�   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 10);
    STR_ƽ̨����ʱ�� := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 11);
    STR_����Ӧ����� := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 12);
    STR_ƽ̨ʵ�ս�� := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 13);
  
    -- ��������������֤��
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;
    IF STR_������ IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч�Ķ����ţ�';
      GOTO �˳�;
    END IF;
    IF STR_ƽ̨������ IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч��ƽ̨�����ţ�';
      GOTO �˳�;
    END IF;
    IF STR_ƽ̨���׺� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч��ƽ̨���׺ţ�';
      GOTO �˳�;
    END IF;
    IF STR_����Ӧ����� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч�Ķ���Ӧ����';
      GOTO �˳�;
    END IF;
    IF STR_ƽ̨ʵ�ս�� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч��ƽ̨ʵ�ս�';
      GOTO �˳�;
    END IF;
    IF STR_ƽ̨����ʱ�� IS NULL OR FU_����ת����(STR_ƽ̨����ʱ��) IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч�Ķ���ʱ�䣡';
      GOTO �˳�;
    END IF;
    IF STR_ƽ̨����ʱ�� IS NULL OR FU_����ת����(STR_ƽ̨����ʱ��) IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч�Ľ���ʱ�䣡';
      GOTO �˳�;
    END IF;
  
    IF STR_����Ӧ����� <> STR_ƽ̨ʵ�ս�� THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'Ӧ�������ʵ�ս�����';
      GOTO �˳�;
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣0��' || SQLERRM;
      GOTO �˳�;
  END;

  -- ��֤����
  BEGIN
    SELECT ��������
      INTO STR_ԤԼ����
      FROM ƽ̨�ӿ�_����
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND �ͻ��˱�ʶ = STR_�ͻ��˱�ʶ
       AND ����ID = STR_����ID
       AND ������ = STR_������
       AND �������� = STR_��������
       AND Ӧ����� = TO_NUMBER(STR_����Ӧ�����)
       AND ����ʱ�� >= SYSDATE
       AND ����״̬ = '��֧��';
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ķ�����';
      GOTO �˳�;
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  -- ��֤ԤԼ��
  BEGIN
    SELECT �հ�α�ʶ
      INTO STR_��α�ʶ
      FROM �������_ԤԼ�Һ�
     WHERE �������� = STR_ҽԺ����
       AND ����ID = STR_ԤԼ����
       AND ȥ���־ = 'ռ��'
       AND ��ʱʱ�� >= SYSDATE;
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч��ԤԼ����';
      GOTO �˳�;
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  -- �����ܴ���
  BEGIN
  
    -- ����ԤԼ��
    UPDATE �������_ԤԼ�Һ� G
       SET ֧����־ = '��', ȥ���־ = 'ԤԼ'
     WHERE �������� = STR_ҽԺ����
       AND ����ID = STR_ԤԼ����;
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '����ԤԼ��ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
    -- ���¶���״̬
    UPDATE ƽ̨�ӿ�_����
       SET ����״̬     = '��֧��',
           ʵ�ս��     = TO_NUMBER(STR_ƽ̨ʵ�ս��),
           ƽ̨������   = STR_ƽ̨������,
           ƽ̨����ʱ�� = TO_DATE(STR_ƽ̨����ʱ��, 'yyyy/mm/dd hh24:mi:ss'),
           ƽ̨���׺�   = STR_ƽ̨���׺�,
           ƽ̨����ʱ�� = TO_DATE(STR_ƽ̨����ʱ��, 'yyyy/mm/dd hh24:mi:ss'),
           ƽ̨�˿��   = NULL,
           ƽ̨�˿�ʱ�� = NULL,
           ������       = STR_ƽ̨��ʶ,
           ����ʱ��     = DAT_ϵͳʱ��
     WHERE ƽ̨��ʶ = STR_ƽ̨��ʶ
       AND �ͻ��˱�ʶ = STR_�ͻ��˱�ʶ
       AND ����ID = STR_����ID
       AND ������ = STR_������
       AND �������� = STR_��������
       AND ����״̬ = '��֧��';
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '���¶���ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
    -- �����ѹҺ���
    UPDATE �������_���Ű�ʱ�α�
       SET �ѹҺ��� = �ѹҺ��� + 1
     WHERE �������� = STR_ҽԺ����
       AND �հ�α�ʶ = STR_��α�ʶ;
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '������Դʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := STR_������Ϣ || SQLERRM;
      GOTO �˳�;
  END;

  -- �����ؽ����
  OPEN CUR_���ؽ���� FOR
    SELECT STR_����ID AS ����ID,
           STR_������ AS ������,
           STR_ԤԼ���� AS ԤԼ����,
           '' AS ��ע
      FROM DUAL;

  INT_����ֵ   := 0;
  STR_������Ϣ := '����ɹ�!';

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => STR_ԤԼ����,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  COMMIT;
  RETURN;

  --���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  --��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  ROLLBACK;
  RETURN;

END PR_ƽ̨�ӿ�_ԤԼ�Һ�֧��;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_ԤԼ��ʷ��ѯ
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_ԤԼ��ʷ��ѯ(STR_�������   IN VARCHAR2,
                                           CUR_���ؽ���� OUT SYS_REFCURSOR,
                                           INT_����ֵ     OUT INTEGER,
                                           STR_������Ϣ   OUT VARCHAR2) IS
  -- �̶�����
  DAT_ϵͳʱ�� DATE;

  -- �������
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID   VARCHAR2(50);
  STR_ԤԼ���� VARCHAR2(50);
  STR_������   VARCHAR2(50);
  STR_ԤԼ״̬ VARCHAR2(50);

BEGIN
  BEGIN
  
    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;
  
    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ϣ��Ч��';
      GOTO �˳�;
    END IF;
  
    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    -- ���������������
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);
  
    -- �����ܲ���������
    STR_����ID   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_ԤԼ���� := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 7);
    STR_������   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 8);
    STR_ԤԼ״̬ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 9);
  
    -- ������֤
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;
  
    IF STR_ԤԼ���� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч��ԤԼ���ţ�';
      GOTO �˳�;
    END IF;
  
    IF STR_������ IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ķ����ţ�';
      GOTO �˳�;
    END IF;
  
    IF STR_ԤԼ״̬ IS NULL OR
       STR_ԤԼ״̬ NOT IN
       ('ȫ��', '��ԤԼ', '��ȡ��', '��֧��', '������', '�Ѿ���') THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�ľ���״̬��';
      GOTO �˳�;
    END IF;
  
    /*
    ˵����
        1��ֻ��ʾ1���ڵ�ԤԼ��¼
    */
    OPEN CUR_���ؽ���� FOR
      SELECT G.����ID AS ����ID,
             G.����ID AS ԤԼ����,
             R.������ AS ������,
             P.���ﲡ���� AS ���ﲡ����,
             G.�Һſ��ұ��� AS ���ұ���,
             G.�Һſ������� AS ��������,
             G.�Һ�ҽ������ AS ҽ������,
             G.�Һ�ҽ������ AS ҽ������,
             TO_CHAR(G.ԤԼʱ��, 'yyyy/mm/dd') AS ԤԼ����,
             TO_CHAR(G.ԤԼʱ�ο�ʼ, 'hh24:mi:ss') || '-' ||
             TO_CHAR(G.ԤԼʱ�ν���, 'hh24:mi:ss') AS ԤԼʱ��,
             (CASE
               WHEN G.ȥ���־ = 'ԤԼ' AND G.֧����־ = '��' THEN
                '��ԤԼ'
               WHEN G.ȥ���־ = '����' THEN
                '��ȡ��'
               WHEN G.ȥ���־ = 'ԤԼ' AND G.֧����־ = '��' THEN
                '��֧��'
               when G.ȥ���־ = 'ռ��' and g.��ʱʱ�� > sysdate then
                '��֧��'
               when g.ȥ���־ = 'ռ��' and g.��ʱʱ�� <= sysdate then
                '֧����ʱ'
               WHEN G.ȥ���־ = '����' AND P.����״̬ = '�ȴ�����' THEN
                '������'
               WHEN G.ȥ���־ = '����' AND P.����״̬ IN ('���ڽ���', '��ɽ���') THEN
                '�Ѿ���'
             END) AS ״̬,
             R.Ӧ����� AS �������,
             (CASE
               WHEN (G.ȥ���־ = 'ԤԼ' AND G.֧����־ = '��') OR
                    (G.ȥ���־ = 'ռ��' AND G.��ʱʱ�� > SYSDATE) THEN
                R.����ʱ��
               ELSE
                NULL
             END) AS ����ʱ��
        FROM �������_ԤԼ�Һ� G
        LEFT JOIN ƽ̨�ӿ�_���� R
          ON G.�������� = R.ҽԺ����
         AND G.����ID = R.��������
         AND G.����ID = R.����ID
        LEFT JOIN �������_�ҺŵǼ� P
          ON G.�������� = P.��������
         AND G.�Ű�ID = P.�Ű�ID
         AND G.����ID = P.����ID
         AND G.�Һ���� = P.�Һ����
         AND G.�հ�α�ʶ = P.�հ�α�ʶ
       WHERE G.�������� = STR_ҽԺ����
         AND G.����ID = STR_����ID
         AND G.����ID = DECODE(STR_ԤԼ����, '-1', G.����ID, STR_ԤԼ����)
         AND R.������ = DECODE(STR_������, '-1', R.������, STR_������)
         AND G.ԤԼ���� = '����ԤԼ'
         AND ((STR_ԤԼ״̬ = 'ȫ��' AND G.ȥ���־ = G.ȥ���־) OR
             (STR_ԤԼ״̬ = '��ԤԼ' AND G.ȥ���־ = 'ԤԼ' AND G.֧����־ = '��') OR
             (STR_ԤԼ״̬ = '��ȡ��' AND G.ȥ���־ = '����') OR
             (STR_ԤԼ״̬ = '��֧��' AND G.ȥ���־ = 'ԤԼ' AND G.֧����־ = '��') OR
             (STR_ԤԼ״̬ = '������' AND G.ȥ���־ = '����' AND P.����״̬ = '�ȴ�����') OR
             (STR_ԤԼ״̬ = '�Ѿ���' AND G.ȥ���־ = '����' AND
             P.����״̬ IN ('���ڽ���', '��ɽ���')))
         AND G.ԤԼʱ�� > ADD_MONTHS(SYSDATE, -1);
  
    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�!';
  
    -- ��������־��
    PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
                 STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
                 STR_ҽԺ����   => STR_ҽԺ����,
                 STR_���ܱ���   => STR_���ܱ���,
                 STR_�������   => STR_�������,
                 DAT_����ʱ��   => DAT_ϵͳʱ��,
                 STR_������   => STR_������Ϣ,
                 STR_��������   => STR_����ID,
                 STR_ִ����     => STR_ƽ̨��ʶ,
                 DAT_ִ��ʱ��   => SYSDATE,
                 STR_ִ��״̬   => INT_����ֵ);
  
    RETURN;
  
  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  RETURN;

END PR_ƽ̨�ӿ�_ԤԼ��ʷ��ѯ;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_ԤԼ״̬��ѯ
prompt =================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_ԤԼ״̬��ѯ(STR_�������   IN VARCHAR2,
                                           CUR_���ؽ���� OUT SYS_REFCURSOR,
                                           INT_����ֵ     OUT INTEGER,
                                           STR_������Ϣ   OUT VARCHAR2) IS
  -- �̶�����
  DAT_ϵͳʱ�� DATE;

  -- �������
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID   VARCHAR2(50);
  STR_ԤԼ���� VARCHAR2(50);

BEGIN
  BEGIN
  
    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;
  
    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ϣ��Ч��';
      GOTO �˳�;
    END IF;
  
    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    -- ���������������
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);
  
    -- �����ܲ���������
    STR_����ID   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_ԤԼ���� := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 7);
  
    -- ������֤
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;
  
    IF STR_ԤԼ���� IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч��ԤԼ���ţ�';
      GOTO �˳�;
    END IF;
  
    /*
    ˵����
        1��ֻ��ʾ1���ڵ�ԤԼ��¼
    */
    OPEN CUR_���ؽ���� FOR
      SELECT G.����ID AS ����ID,
             G.����ID AS ԤԼ����,
             R.������ AS ������,
             P.���ﲡ���� AS ���ﲡ����,
             (CASE
               WHEN G.ȥ���־ = 'ԤԼ' AND G.֧����־ = '��' THEN
                '��ԤԼ'
               WHEN G.ȥ���־ = '����' THEN
                '��ȡ��'
               WHEN G.ȥ���־ = 'ԤԼ' AND G.֧����־ = '��' THEN
                '��֧��'
               WHEN G.ȥ���־ = 'ռ��' AND G.��ʱʱ�� > SYSDATE THEN
                '��֧��'
               WHEN G.ȥ���־ = 'ռ��' AND G.��ʱʱ�� <= SYSDATE THEN
                '֧����ʱ'
               WHEN G.ȥ���־ = '����' AND P.����״̬ = '�ȴ�����' THEN
                '������'
               WHEN G.ȥ���־ = '����' AND P.����״̬ IN ('���ڽ���', '��ɽ���') THEN
                '�Ѿ���'
             END) AS ԤԼ״̬
      
        FROM �������_ԤԼ�Һ� G
        LEFT JOIN ƽ̨�ӿ�_���� R
          ON G.�������� = R.ҽԺ����
         AND G.����ID = R.��������
         AND G.����ID = R.����ID
        LEFT JOIN �������_�ҺŵǼ� P
          ON G.�������� = P.��������
         AND G.�Ű�ID = P.�Ű�ID
         AND G.����ID = P.����ID
         AND G.�Һ���� = P.�Һ����
         AND G.�հ�α�ʶ = P.�հ�α�ʶ
       WHERE G.�������� = STR_ҽԺ����
         AND G.����ID = STR_����ID
         AND G.����ID = STR_ԤԼ����
         AND G.ԤԼ���� = '����ԤԼ';
  
    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�!';
  
    -- ��������־��
    PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
                 STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
                 STR_ҽԺ����   => STR_ҽԺ����,
                 STR_���ܱ���   => STR_���ܱ���,
                 STR_�������   => STR_�������,
                 DAT_����ʱ��   => DAT_ϵͳʱ��,
                 STR_������   => STR_������Ϣ,
                 STR_��������   => STR_����ID,
                 STR_ִ����     => STR_ƽ̨��ʶ,
                 DAT_ִ��ʱ��   => SYSDATE,
                 STR_ִ��״̬   => INT_����ֵ);
  
    RETURN;
  
  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  RETURN;

END PR_ƽ̨�ӿ�_ԤԼ״̬��ѯ;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_סԺ���˷������嵥
prompt ====================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_סԺ���˷������嵥(STR_�������   IN VARCHAR2,
                                            CUR_���ؽ���� OUT SYS_REFCURSOR,
                                            INT_����ֵ     OUT INTEGER,
                                            STR_������Ϣ   OUT VARCHAR2) IS
  -- �̶�����
  DAT_ϵͳʱ�� DATE;

  -- �������
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID     VARCHAR2(50);
  STR_סԺ������ VARCHAR2(50);
  STR_��������   VARCHAR2(50);

  -- �����ڲ���
  NUM_�嵥�ܶ� NUMBER(18, 4);

BEGIN
  BEGIN

    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;

    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ϣ��Ч��';
      GOTO �˳�;
    END IF;

    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;

    -- ���������������
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);

    -- �����ܲ���������
    STR_����ID     := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_סԺ������ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 7);
    STR_��������   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 8);

    -- ������֤
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;

    IF STR_סԺ������ IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ����ţ�';
      GOTO �˳�;
    END IF;

    IF STR_�������� IS NULL
       OR FU_����ת����(STR_��������) IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�ķ������ڣ�';
      GOTO �˳�;
    END IF;

    /*
    ˵����
        1��ֻ��ʾ��Ժ���˵�ָ�������嵥
    */

    -- ȡ�嵥�ܶ�
    SELECT NVL(SUM(�ܽ��), 0)
      INTO NUM_�嵥�ܶ�
      FROM סԺ����_��Ժ���˴���
     WHERE �������� = STR_ҽԺ����
       AND סԺ������ = STR_סԺ������
       AND TO_CHAR(����ʱ��, 'yyyy-mm-dd') = STR_��������;

    -- ���ؽ��
    OPEN CUR_���ؽ���� FOR
      SELECT ����ID AS ����ID,
             סԺ������ AS סԺ������,
             TO_CHAR(����ʱ��, 'yyyy-mm-dd') AS ��������,
             ��Ŀ���� AS ��Ŀ����,
             ���� AS ����,
             �ܽ�� AS ���,
             NUM_�嵥�ܶ� AS �嵥�ܶ�
        FROM סԺ����_��Ժ���˴��� C
       WHERE �������� = STR_ҽԺ����
         AND ����ID = STR_����ID
         AND סԺ������ = STR_סԺ������
         AND TO_CHAR(����ʱ��, 'yyyy-mm-dd') = STR_��������;

    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�!';

    -- ��������־��
    PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
                 STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
                 STR_ҽԺ����   => STR_ҽԺ����,
                 STR_���ܱ���   => STR_���ܱ���,
                 STR_�������   => STR_�������,
                 DAT_����ʱ��   => DAT_ϵͳʱ��,
                 STR_������   => STR_������Ϣ,
                 STR_��������   => STR_סԺ������,
                 STR_ִ����     => STR_ƽ̨��ʶ,
                 DAT_ִ��ʱ��   => SYSDATE,
                 STR_ִ��״̬   => INT_����ֵ);

    RETURN;

  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  RETURN;

END PR_ƽ̨�ӿ�_סԺ���˷������嵥;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_סԺ������Ϣ��ѯ
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_סԺ������Ϣ��ѯ(STR_�������   IN VARCHAR2,
                                             CUR_���ؽ���� OUT SYS_REFCURSOR,
                                             INT_����ֵ     OUT INTEGER,
                                             STR_������Ϣ   OUT VARCHAR2) IS
  -- �̶�����
  DAT_ϵͳʱ�� DATE;

  -- �������
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID     VARCHAR2(50);
  STR_סԺ������ VARCHAR2(50);
  STR_סԺ״̬   VARCHAR2(50);

BEGIN
  BEGIN

    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;

    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ϣ��Ч��';
      GOTO �˳�;
    END IF;

    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;

    -- ���������������
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);

    -- �����ܲ���������
    STR_����ID     := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_סԺ������ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 7);
    STR_סԺ״̬   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 8);

    -- ������֤
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;

    IF STR_סԺ������ IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ����ţ�';
      GOTO �˳�;
    END IF;

    IF STR_סԺ״̬ IS NULL
       OR (STR_סԺ״̬ <> 'ȫ��' AND STR_סԺ״̬ <> '��Ժ' AND STR_סԺ״̬ <> '��Ժ') THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч��סԺ״̬��';
      GOTO �˳�;
    END IF;

    /*
    ˵����
        1��ֻ��ʾ1���ڵļ�¼
    */
    OPEN CUR_���ؽ���� FOR
      SELECT G.����ID       AS ����ID,
             G.סԺ������   AS סԺ������,
             G.���˿��ұ��� AS ���ұ���,
             K.��������     AS ��������,
             G.סԺҽ������ AS ҽ������,
             R.��Ա����     AS ҽ������,
             C.��λ����     AS ����,
             G.��Ժʱ��     AS ��Ժʱ��,
             G.��Ժʱ��     AS ��Ժʱ��,
             G.�����ܶ�     AS �����ܶ�,
             G.Ѻ�����     AS Ѻ�����,
             G.סԺ״̬     AS סԺ״̬
        FROM (SELECT ��������,
                     ����ID,
                     סԺ������,
                     ���˿��ұ���,
                     סԺҽ������,
                     ��λ,
                     ��Ժʱ��,
                     NULL AS ��Ժʱ��,
                     FU_����_�ܷ���_��Ժ����(STR_ҽԺ����, סԺ������) AS �����ܶ�,
                     FU_����_ʣ����_��Ժ����(STR_ҽԺ����, סԺ������) AS Ѻ�����,
                     '��Ժ' AS סԺ״̬
                FROM סԺ����_��Ժ������Ϣ
               WHERE �������� = STR_ҽԺ����
                 AND ����ID = STR_����ID
                 AND סԺ������ =
                     DECODE(STR_סԺ������, '-1', סԺ������, STR_סԺ������)
              UNION ALL
              SELECT ��������,
                     ����ID,
                     סԺ������,
                     ���˿��ұ���,
                     סԺҽ������,
                     '' AS ��λ,
                     ��Ժʱ��,
                     ��Ժʱ��,
                     0 AS �����ܶ�,
                     0 AS Ѻ�����,
                     '��Ժ' AS סԺ״̬
                FROM סԺ����_��Ժ������Ϣ
               WHERE �������� = STR_ҽԺ����
                 AND ����ID = STR_����ID
                 AND סԺ������ =
                     DECODE(STR_סԺ������, '-1', סԺ������, STR_סԺ������)
                 AND ��Ժʱ�� > ADD_MONTHS(SYSDATE, -12)) G
        LEFT JOIN ������Ŀ_��Ա���� R
          ON G.�������� = R.��������
         AND G.סԺҽ������ = R.��Ա����
        LEFT JOIN ������Ŀ_�������� K
          ON G.�������� = K.��������
         AND G.���˿��ұ��� = K.���ұ���
        LEFT JOIN סԺ����_���Ҵ�λ C
          ON G.�������� = C.��������
         AND G.��λ = C.��λ����
       WHERE G.סԺ״̬ = DECODE(STR_סԺ״̬, 'ȫ��', G.סԺ״̬, STR_סԺ״̬);

    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�!';

    -- ��������־��
    PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
                 STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
                 STR_ҽԺ����   => STR_ҽԺ����,
                 STR_���ܱ���   => STR_���ܱ���,
                 STR_�������   => STR_�������,
                 DAT_����ʱ��   => DAT_ϵͳʱ��,
                 STR_������   => STR_������Ϣ,
                 STR_��������   => STR_����ID,
                 STR_ִ����     => STR_ƽ̨��ʶ,
                 DAT_ִ��ʱ��   => SYSDATE,
                 STR_ִ��״̬   => INT_����ֵ);

    RETURN;

  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  RETURN;

END PR_ƽ̨�ӿ�_סԺ������Ϣ��ѯ;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_סԺ���û����嵥
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_סԺ���û����嵥(STR_�������   IN VARCHAR2,
                                             CUR_���ؽ���� OUT SYS_REFCURSOR,
                                             INT_����ֵ     OUT INTEGER,
                                             STR_������Ϣ   OUT VARCHAR2) IS
  -- �̶�����
  DAT_ϵͳʱ�� DATE;

  -- �������
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID     VARCHAR2(50);
  STR_סԺ������ VARCHAR2(50);

  -- �����ڲ���
  NUM_�嵥�ܶ� NUMBER(18, 4);

  STR_ĸӤһ�廯 VARCHAR2(50);

BEGIN
  BEGIN
  
    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;
  
    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ϣ��Ч��';
      GOTO �˳�;
    END IF;
  
    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  
    -- ���������������
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);
  
    -- �����ܲ���������
    STR_����ID     := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_סԺ������ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 7);
  
    -- ������֤
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;
  
    IF STR_סԺ������ IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ����ţ�';
      GOTO �˳�;
    END IF;
  
    -- ��ȡϵͳ����
  
    BEGIN
      SELECT ֵ
        INTO STR_ĸӤһ�廯
        FROM ������Ŀ_���������б�
       WHERE �������� = '274'
         AND �������� = STR_ҽԺ����;
    EXCEPTION
      WHEN OTHERS THEN
        STR_ĸӤһ�廯 := '��';
    END;
  
    STR_������Ϣ := FU_ת��_סԺ������(STR_ҽԺ����,
                            STR_סԺ������,
                            '��',
                            STR_ĸӤһ�廯);
  
    /*
    ˵����
        1��ֻ��ʾ��Ժ���˵ķ����嵥
    */
  
    -- ȡ�嵥�ܶ�
    SELECT NVL(SUM(�ܽ��), 0)
      INTO NUM_�嵥�ܶ�
      FROM סԺ����_��Ժ������Ϣ A,
           סԺ����_��Ժ���˴��� B,
           ��ʱ��_������         C
     WHERE A.�������� = B.��������
       AND A.סԺ������ = B.סԺ������
       AND A.�������� = STR_ҽԺ����
       AND A.סԺ������ = (CASE STR_ĸӤһ�廯
             WHEN '��' THEN
              C.������
             ELSE
              STR_סԺ������
           END);
  
    -- ���ؽ��
    OPEN CUR_���ؽ���� FOR
      SELECT ����ID AS ����ID,
             סԺ������ AS סԺ������,
             M.���� AS ���ù���,
             SUM(�ܽ��) AS ���,
             NUM_�嵥�ܶ� AS �嵥�ܶ�
        FROM (SELECT A.����ID, A.סԺ������, B.�������, B.�ܽ��
                FROM סԺ����_��Ժ������Ϣ A,
                     סԺ����_��Ժ���˴��� B,
                     ��ʱ��_������         C
               WHERE A.�������� = B.��������
                 AND A.סԺ������ = B.סԺ������
                 AND A.�������� = STR_ҽԺ����
                 AND A.סԺ������ = (CASE STR_ĸӤһ�廯
                       WHEN '��' THEN
                        C.������
                       ELSE
                        STR_סԺ������
                     END)) Z
        LEFT JOIN ������Ŀ_�ֵ���ϸ M
          ON Z.������� = M.����
         AND ������� = 'GB_009001'
         AND ɾ����־ = '0'
       GROUP BY ����ID, סԺ������, M.����;
  
    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�!';
  
    -- ��������־��
    PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
                 STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
                 STR_ҽԺ����   => STR_ҽԺ����,
                 STR_���ܱ���   => STR_���ܱ���,
                 STR_�������   => STR_�������,
                 DAT_����ʱ��   => DAT_ϵͳʱ��,
                 STR_������   => STR_������Ϣ,
                 STR_��������   => STR_סԺ������,
                 STR_ִ����     => STR_ƽ̨��ʶ,
                 DAT_ִ��ʱ��   => SYSDATE,
                 STR_ִ��״̬   => INT_����ֵ);
  
    RETURN;
  
  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  RETURN;

END PR_ƽ̨�ӿ�_סԺ���û����嵥;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_סԺѺ������ѯ
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_סԺѺ������ѯ(STR_�������   IN VARCHAR2,
                                             CUR_���ؽ���� OUT SYS_REFCURSOR,
                                             INT_����ֵ     OUT INTEGER,
                                             STR_������Ϣ   OUT VARCHAR2) IS
  -- �̶�����
  DAT_ϵͳʱ�� DATE;

  -- �������
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID     VARCHAR2(50);
  STR_סԺ������ VARCHAR2(50);

BEGIN
  BEGIN

    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '�Ƿ�����';
      GOTO �˳�;
    END IF;

    -- ��������Ч����֤
    IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ϣ��Ч��';
      GOTO �˳�;
    END IF;

    -- �����ݳ�ʼ����
    SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;

    -- ���������������
    STR_ƽ̨��ʶ   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 1);
    STR_��֤�ܳ�   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 2);
    STR_�ͻ��˱�ʶ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 3);
    STR_���ܱ���   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 4);
    STR_ҽԺ����   := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 5);

    -- �����ܲ���������
    STR_����ID     := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 6);
    STR_סԺ������ := FU_ƽ̨�ӿ�_��ȡ�ַ���ֵ(STR_�������, '|', 7);

    -- ������֤
    IF STR_����ID IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ���ID��';
      GOTO �˳�;
    END IF;

    IF STR_סԺ������ IS NULL THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��Ч�Ĳ����ţ�';
      GOTO �˳�;
    END IF;

    /*
    ˵����
        1��ֻ��ʾ��Ժ���˵������Ϣ
    */
    OPEN CUR_���ؽ���� FOR
      SELECT ����ID AS ����ID,
             סԺ������ AS סԺ������,
             FU_����_ʣ����_��Ժ����(STR_ҽԺ����, סԺ������) AS Ѻ�����,
             DAT_ϵͳʱ�� AS ��ѯʱ��
        FROM סԺ����_��Ժ������Ϣ
       WHERE �������� = STR_ҽԺ����
         AND ����ID = STR_����ID
         AND סԺ������ = STR_סԺ������;

    INT_����ֵ   := 0;
    STR_������Ϣ := '�ɹ�!';

    -- ��������־��
    PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
                 STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
                 STR_ҽԺ����   => STR_ҽԺ����,
                 STR_���ܱ���   => STR_���ܱ���,
                 STR_�������   => STR_�������,
                 DAT_����ʱ��   => DAT_ϵͳʱ��,
                 STR_������   => STR_������Ϣ,
                 STR_��������   => STR_����ID,
                 STR_ִ����     => STR_ƽ̨��ʶ,
                 DAT_ִ��ʱ��   => SYSDATE,
                 STR_ִ��״̬   => INT_����ֵ);

    RETURN;

  EXCEPTION
    -- δ����ϵͳ�쳣
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := SQLERRM;
      GOTO �˳�;
  END;

  -- ���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  RETURN;

END PR_ƽ̨�ӿ�_סԺѺ������ѯ;
/

prompt
prompt Creating procedure PR_ƽ̨�ӿ�_סԺѺ��֧���ӿ�
prompt ===================================
prompt
CREATE OR REPLACE PROCEDURE PR_ƽ̨�ӿ�_סԺѺ��֧���ӿ�(STR_�������   IN VARCHAR2,
                                           CUR_���ؽ���� OUT SYS_REFCURSOR,
                                           INT_����ֵ     OUT INTEGER,
                                           STR_������Ϣ   OUT VARCHAR2) IS

  DAT_ϵͳʱ�� DATE;

  -- �̶�����
  STR_ƽ̨��ʶ   VARCHAR2(50);
  STR_��֤�ܳ�   VARCHAR2(50);
  STR_�ͻ��˱�ʶ VARCHAR2(50);
  STR_���ܱ���   VARCHAR2(50);
  STR_ҽԺ����   VARCHAR2(50);

  -- ���ܲ���
  STR_����ID       VARCHAR2(50);
  STR_סԺ������   VARCHAR2(50);
  STR_ƽ̨������   VARCHAR2(50);
  STR_ƽ̨���׺�   VARCHAR2(50);
  STR_ƽ̨����ʱ�� VARCHAR2(50);
  STR_ƽ̨����ʱ�� VARCHAR2(50);
  STR_Ԥ�����     VARCHAR2(50);

  -- �������
  STR_�������� VARCHAR2(50);
  STR_������   VARCHAR2(50);
  STR_��ˮ��   VARCHAR2(50);
  STR_Ԥ������ VARCHAR2(50);
  NUM_Ԥ����� NUMBER(18, 4);

  STR_ƽ̨���� VARCHAR2(50);
  STR_֧����ʽ VARCHAR2(50);

BEGIN

  -- ����������Ч����֤��
  IF FU_ƽ̨�ӿ�_��֤��������(STR_�������) <> 0 THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '�Ƿ�����';
    GOTO �˳�;
  END IF;

  -- ����������Ч����֤��
  IF FU_ƽ̨�ӿ�_��֤��������Ϣ(STR_�������) <> 0 THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ϣ��Ч��';
    GOTO �˳�;
  END IF;

  -- �����ݳ�ʼ����
  SELECT SYSDATE INTO DAT_ϵͳʱ�� FROM DUAL;
  STR_�������� := 'Ԥ��Ѻ��';

  -- ���̶�����������

  STR_ƽ̨��ʶ   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 1);
  STR_��֤�ܳ�   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 2);
  STR_�ͻ��˱�ʶ := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 3);
  STR_���ܱ���   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 4);
  STR_ҽԺ����   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 5);

  -- �����ܲ���������

  STR_����ID       := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 6);
  STR_סԺ������   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 7);
  STR_ƽ̨������   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 8);
  STR_ƽ̨����ʱ�� := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 9);
  STR_ƽ̨���׺�   := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 10);
  STR_ƽ̨����ʱ�� := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 11);
  STR_Ԥ�����     := FU_ͨ��_��ȡ�ַ���ֵ(STR_�������, '|', 12);

  -- ����ȡƽ̨��Ϣ��
  BEGIN
    SELECT P.֧����ʽ, P.ƽ̨����
      INTO STR_֧����ʽ, STR_ƽ̨����
      FROM ƽ̨�ӿ�_ƽ̨���� P
     WHERE P.ƽ̨��ʶ = STR_ƽ̨��ʶ;
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'δ�ҵ���Ч��ƽ̨��Ϣ��';
      GOTO �˳�;
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := 'ϵͳ�쳣��' || SQLERRM;
      GOTO �˳�;
  END;

  -- ��������������֤��
  IF STR_����ID IS NULL THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ч�Ĳ���ID��';
    GOTO �˳�;
  END IF;
  IF STR_סԺ������ IS NULL THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ч��סԺ�����ţ�';
    GOTO �˳�;
  END IF;
  IF STR_ƽ̨������ IS NULL THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ч��ƽ̨�����ţ�';
    GOTO �˳�;
  END IF;
  IF STR_ƽ̨���׺� IS NULL THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ч��ƽ̨���׺ţ�';
    GOTO �˳�;
  END IF;
  IF STR_ƽ̨����ʱ�� IS NULL
     OR FU_����ת����(STR_ƽ̨����ʱ��) IS NULL THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ч�Ķ���ʱ�䣡';
    GOTO �˳�;
  END IF;
  IF STR_ƽ̨����ʱ�� IS NULL
     OR FU_����ת����(STR_ƽ̨����ʱ��) IS NULL THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ч�Ľ���ʱ�䣡';
    GOTO �˳�;
  END IF;
  IF STR_Ԥ����� IS NULL THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '��������Ч��Ԥ����';
    GOTO �˳�;
  END IF;

  -- ��֤Ԥ�����
  BEGIN
    SELECT TO_NUMBER(STR_Ԥ�����) INTO NUM_Ԥ����� FROM DUAL;
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '��������Ч��Ԥ�����';
      GOTO �˳�;
  END;
  IF NUM_Ԥ����� <= 0 THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := 'Ԥ���������ڵ���0��';
    GOTO �˳�;
  END IF;

  -- ��֤סԺ����
  SELECT COUNT(1)
    INTO INT_����ֵ
    FROM סԺ����_��Ժ������Ϣ
   WHERE �������� = STR_ҽԺ����
     AND סԺ������ = STR_סԺ������;

  IF INT_����ֵ <= 0 THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '�����Ѿ���Ϊ��Ժ״̬,���ܽ�Ԥ����!��';
    GOTO �˳�;
  END IF;

  -- ����Ԥ������
  SELECT FU_����_��ȡ��ǰƱ�ݺ�(STR_ҽԺ����, STR_ƽ̨��ʶ, '3')
    INTO STR_Ԥ������
    FROM DUAL;

  IF STR_Ԥ������ = '�뵽����������Ʊ��' THEN
    STR_������Ϣ := '��֪ͨ����������Ʊ��!';
    GOTO �˳�;
  END IF;

  -- ���ɶ�����
  PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '6001',
              PRM_��������   => STR_ҽԺ����,
              PRM_��������   => '1',
              PRM_����Ψһ�� => STR_������,
              PRM_ִ�н��   => INT_����ֵ,
              PRM_������Ϣ   => STR_������Ϣ);
  IF INT_����ֵ <> 0 THEN
    INT_����ֵ   := -1;
    STR_������Ϣ := '����������ʧ��!';
    GOTO �˳�;
  END IF;

  -- ������ˮ��
  PR_��ȡ_ϵͳΨһ��(PRM_Ψһ�ű��� => '29',
              PRM_��������   => STR_ҽԺ����,
              PRM_��������   => '1',
              PRM_����Ψһ�� => STR_��ˮ��,
              PRM_ִ�н��   => INT_����ֵ,
              PRM_������Ϣ   => STR_������Ϣ);
  IF INT_����ֵ <> 0 THEN
    STR_������Ϣ := '������ˮ��ʧ��!';
    GOTO �˳�;
  END IF;

  -- �����ܴ���
  BEGIN
  
    -- ����Ԥ�����¼
    INSERT INTO סԺ����_��Ժ����Ԥ����
      (��������,
       ����ID,
       סԺ������,
       Ԥ������,
       �ɷѽ��,
       �ɷ�ʱ��,
       Ԥ������,
       ����������,
       ��ע,
       ����Ա����,
       ����Ա����,
       ������־,
       ��ˮ��,
       ֧����ʽ)
    VALUES
      (STR_ҽԺ����,
       STR_����ID,
       STR_סԺ������,
       STR_Ԥ������,
       NUM_Ԥ�����,
       DAT_ϵͳʱ��,
       'POS�ɷ�',
       NULL,
       NULL,
       STR_ƽ̨��ʶ,
       STR_ƽ̨����,
       '��',
       STR_��ˮ��,
       STR_֧����ʽ);
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '����ɷ�����ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
    -- ���붩��
    INSERT INTO ƽ̨�ӿ�_����
      (��ˮ��,
       ƽ̨��ʶ,
       �ͻ��˱�ʶ,
       ҽԺ����,
       ����ID,
       ���ﲡ����,
       ��������,
       ������,
       ��������,
       ����ʱ��,
       Ӧ�����,
       �Żݽ��,
       ʵ�ս��,
       ����ʱ��,
       ����״̬,
       ƽ̨������,
       ƽ̨����ʱ��,
       ƽ̨���׺�,
       ƽ̨����ʱ��,
       ������,
       ����ʱ��)
    VALUES
      (SEQ_ƽ̨�ӿ�_����_��ˮ��.NEXTVAL,
       STR_ƽ̨��ʶ,
       STR_�ͻ��˱�ʶ,
       STR_ҽԺ����,
       STR_����ID,
       STR_סԺ������,
       STR_Ԥ������,
       STR_������,
       STR_��������,
       DAT_ϵͳʱ��,
       NUM_Ԥ�����,
       0,
       NUM_Ԥ�����,
       NULL,
       '��֧��',
       STR_ƽ̨������,
       TO_DATE(STR_ƽ̨����ʱ��, 'yyyy/mm/dd hh24:mi:ss'),
       STR_ƽ̨���׺�,
       TO_DATE(STR_ƽ̨����ʱ��, 'yyyy/mm/dd hh24:mi:ss'),
       STR_ƽ̨��ʶ,
       DAT_ϵͳʱ��);
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '���涩��ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
    -- ���붩����ϸ
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
      (SEQ_ƽ̨�ӿ�_������ϸ_��ˮ��.NEXTVAL,
       STR_������,
       'Ԥ��Ѻ��',
       'Ԥ��Ѻ��',
       1,
       '��',
       NUM_Ԥ�����,
       NUM_Ԥ�����,
       NULL);
  
    INT_����ֵ := SQL%ROWCOUNT;
    IF INT_����ֵ = 0 THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := '���涩��ʧ�ܣ�';
      GOTO �˳�;
    END IF;
  
  EXCEPTION
    WHEN OTHERS THEN
      INT_����ֵ   := -1;
      STR_������Ϣ := STR_������Ϣ || SQLERRM;
      GOTO �˳�;
  END;

  -- �����ؽ����
  OPEN CUR_���ؽ���� FOR
    SELECT STR_����ID AS ����ID,
           STR_סԺ������ AS סԺ������,
           STR_������ AS ������,
           STR_Ԥ������ AS Ʊ�ݺ�,
           '' AS ��ע
      FROM DUAL;

  INT_����ֵ   := 0;
  STR_������Ϣ := '����ɹ�!';

  -- ��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => STR_Ԥ������,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  COMMIT;
  RETURN;

  --���쳣�˳���
  <<�˳�>>
  INT_����ֵ   := INT_����ֵ;
  STR_������Ϣ := STR_������Ϣ;
  OPEN CUR_���ؽ���� FOR
    SELECT NULL FROM DUAL;

  --��������־��
  PR_ƽ̨�ӿ�_������־(STR_ƽ̨��ʶ   => STR_ƽ̨��ʶ,
               STR_�ͻ��˱�ʶ => STR_�ͻ��˱�ʶ,
               STR_ҽԺ����   => STR_ҽԺ����,
               STR_���ܱ���   => STR_���ܱ���,
               STR_�������   => STR_�������,
               DAT_����ʱ��   => DAT_ϵͳʱ��,
               STR_������   => STR_������Ϣ,
               STR_��������   => NULL,
               STR_ִ����     => STR_ƽ̨��ʶ,
               DAT_ִ��ʱ��   => SYSDATE,
               STR_ִ��״̬   => INT_����ֵ);
  ROLLBACK;
  RETURN;

END PR_ƽ̨�ӿ�_סԺѺ��֧���ӿ�;
/


prompt Done
spool off
set define on
