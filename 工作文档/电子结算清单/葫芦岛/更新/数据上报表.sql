
prompt
prompt Creating table ��������_�����б�
prompt ========================
prompt
create table ��������_�����б�
(
  �������� VARCHAR2(50) not null,
  ������  VARCHAR2(50) not null,
  ��ʼʱ�� DATE,
  ��ֹʱ�� DATE,
  ����ʱ�� DATE,
  ��ˮ��  VARCHAR2(50),
  ��Ŀ���� VARCHAR2(50) not null,
  ������� VARCHAR2(50) not null
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
comment on column ��������_�����б�.��������
  is '��������';
comment on column ��������_�����б�.������
  is '������';
comment on column ��������_�����б�.��ʼʱ��
  is '����ʱ������������ʼʱ��';
comment on column ��������_�����б�.��ֹʱ��
  is '����ʱ���������Ľ�ֹʱ��';
comment on column ��������_�����б�.����ʱ��
  is '����ʱ��';
comment on column ��������_�����б�.��ˮ��
  is 'ÿ������������ˮ����ͬ';
comment on column ��������_�����б�.��Ŀ����
  is '��Ŀ����';
comment on column ��������_�����б�.�������
  is '�����Ժ��סԺ';
alter table ��������_�����б�
  add constraint PK_��������_�����б� primary key (��Ŀ����, ������, �������)
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
prompt Creating table ��������_��Ŀ������¼
prompt ==========================
prompt
create table ��������_��Ŀ������¼
(
  �������� VARCHAR2(50),
  ��Ŀ���� VARCHAR2(50),
  �������� VARCHAR2(100),
  ������Ա VARCHAR2(50),
  ����ʱ�� DATE
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
comment on table ��������_��Ŀ������¼
  is '��������_��Ŀ������¼';
comment on column ��������_��Ŀ������¼.��������
  is '��������';
comment on column ��������_��Ŀ������¼.��Ŀ����
  is '��Ŀ����';
comment on column ��������_��Ŀ������¼.��������
  is '��������';
comment on column ��������_��Ŀ������¼.������Ա
  is '������Ա';
comment on column ��������_��Ŀ������¼.����ʱ��
  is '����ʱ��';

prompt
prompt Creating table ��������_��Ŀ�ӿڶ��շ���
prompt ============================
prompt
create table ��������_��Ŀ�ӿڶ��շ���
(
  �������� VARCHAR2(50) not null,
  ��Ŀ���� VARCHAR2(50) not null,
  ��ˮ��  VARCHAR2(50) not null,
  ������� VARCHAR2(50) not null,
  �������� VARCHAR2(50) not null,
  �������� VARCHAR2(50) not null,
  �������� VARCHAR2(100)
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
comment on table ��������_��Ŀ�ӿڶ��շ���
  is '��������_��Ŀ�ӿڶ��շ���';
comment on column ��������_��Ŀ�ӿڶ��շ���.��������
  is '��������';
comment on column ��������_��Ŀ�ӿڶ��շ���.��Ŀ����
  is '��Ŀ����';
comment on column ��������_��Ŀ�ӿڶ��շ���.��ˮ��
  is '��ˮ��';
comment on column ��������_��Ŀ�ӿڶ��շ���.�������
  is '�������';
comment on column ��������_��Ŀ�ӿڶ��շ���.��������
  is '��������';
comment on column ��������_��Ŀ�ӿڶ��շ���.��������
  is '��������';
comment on column ��������_��Ŀ�ӿڶ��շ���.��������
  is '��������';

prompt
prompt Creating table ��������_��Ŀ�ӿڶ�����ϸ
prompt ============================
prompt
create table ��������_��Ŀ�ӿڶ�����ϸ
(
  ��ˮ��      VARCHAR2(50),
  ���id     VARCHAR2(50),
  �ӿڶ�����Ϣ���� VARCHAR2(50) not null,
  �ӿڶ�����Ϣ���� VARCHAR2(100)
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
comment on table ��������_��Ŀ�ӿڶ�����ϸ
  is '��������_��Ŀ�ӿڶ�����ϸ';
comment on column ��������_��Ŀ�ӿڶ�����ϸ.��ˮ��
  is '��ˮ��';
comment on column ��������_��Ŀ�ӿڶ�����ϸ.���id
  is '��������_��Ŀ�ӿڶ�����Ϣ�������ˮ��';
comment on column ��������_��Ŀ�ӿڶ�����ϸ.�ӿڶ�����Ϣ����
  is '�ӿڶ�����Ϣ����';
comment on column ��������_��Ŀ�ӿڶ�����ϸ.�ӿڶ�����Ϣ����
  is '�ӿڶ�����Ϣ����';

prompt
prompt Creating table ��������_��Ŀϵͳ������ϸ
prompt ============================
prompt
create table ��������_��Ŀϵͳ������ϸ
(
  ���id     VARCHAR2(50) not null,
  ϵͳ������Ϣ���� VARCHAR2(50) not null,
  ϵͳ������Ϣ���� VARCHAR2(50)
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
comment on table ��������_��Ŀϵͳ������ϸ
  is '��������_��Ŀϵͳ������ϸ';
comment on column ��������_��Ŀϵͳ������ϸ.���id
  is '��������_��Ŀ�ӿڶ�����Ϣ����ˮ��';
comment on column ��������_��Ŀϵͳ������ϸ.ϵͳ������Ϣ����
  is 'ϵͳ������Ϣ����';
comment on column ��������_��Ŀϵͳ������ϸ.ϵͳ������Ϣ����
  is 'ϵͳ������Ϣ����';

prompt
prompt Creating table ��������_��Ŀ��Ϣ
prompt ========================
prompt
create table ��������_��Ŀ��Ϣ
(
  ��������   VARCHAR2(50) not null,
  ��Ŀ����   VARCHAR2(50) not null,
  ��Ŀ����   VARCHAR2(100),
  �����ļ����� VARCHAR2(50),
  �洢������  VARCHAR2(50),
  ƴ����    VARCHAR2(50),
  �����    VARCHAR2(50),
  ��Ч״̬   VARCHAR2(50),
  ɾ����־   VARCHAR2(2) default '0',
  ����ʱ��   DATE,
  ������Ա   VARCHAR2(50),
  �ϼ���Ŀ���� VARCHAR2(50),
  �Ƿ�ش����� VARCHAR2(10),
  �ļ�����ʽ  VARCHAR2(50),
  �Ƿ��Զ����� VARCHAR2(50),
  ����ʱ��   VARCHAR2(50),
  ����·��   VARCHAR2(1000)
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
comment on table ��������_��Ŀ��Ϣ
  is '�ϱ���Ϣ���ͱ�';
comment on column ��������_��Ŀ��Ϣ.��������
  is '��������';
comment on column ��������_��Ŀ��Ϣ.��Ŀ����
  is '��Ŀ����';
comment on column ��������_��Ŀ��Ϣ.��Ŀ����
  is '��Ŀ����';
comment on column ��������_��Ŀ��Ϣ.�����ļ�����
  is '�����ļ�����';
comment on column ��������_��Ŀ��Ϣ.�洢������
  is '�洢������';
comment on column ��������_��Ŀ��Ϣ.ƴ����
  is 'ƴ����';
comment on column ��������_��Ŀ��Ϣ.�����
  is '�����';
comment on column ��������_��Ŀ��Ϣ.��Ч״̬
  is '��Ч״̬';
comment on column ��������_��Ŀ��Ϣ.ɾ����־
  is 'ɾ����־';
comment on column ��������_��Ŀ��Ϣ.����ʱ��
  is '����ʱ��';
comment on column ��������_��Ŀ��Ϣ.������Ա
  is '������Ա';
alter table ��������_��Ŀ��Ϣ
  add constraint PK_��������_��Ŀ��Ϣ primary key (��������, ��Ŀ����)
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
prompt Creating table ��������_��Ŀ�Զ�������
prompt ===========================
prompt
create table ��������_��Ŀ�Զ�������
(
  �������� VARCHAR2(50) not null,
  ��Ŀ���� VARCHAR2(50) not null,
  ��ˮ��  VARCHAR2(50) not null,
  ������  VARCHAR2(50),
  ����   VARCHAR2(255)
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
comment on column ��������_��Ŀ�Զ�������.��������
  is '��������';
comment on column ��������_��Ŀ�Զ�������.��Ŀ����
  is '��Ŀ����';
comment on column ��������_��Ŀ�Զ�������.��ˮ��
  is '��ˮ��';
comment on column ��������_��Ŀ�Զ�������.������
  is '������';
comment on column ��������_��Ŀ�Զ�������.����
  is '����';
alter table ��������_��Ŀ�Զ�������
  add constraint PK_��������_��Ŀ�Զ������� primary key (��������, ��Ŀ����, ��ˮ��)
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
prompt Creating table ��������_��Ŀ�ֵ��Ӧ��ϵ
prompt ============================
prompt
create table ��������_��Ŀ�ֵ��Ӧ��ϵ
(
  ��������     VARCHAR2(50) not null,
  ��Ŀ����     VARCHAR2(50) not null,
  ϵͳ�ֵ������� VARCHAR2(50) not null,
  ϵͳ�ֵ���ϸ���� VARCHAR2(50) not null,
  �ӿ��ֵ������� VARCHAR2(50),
  �ӿ��ֵ���ϸ���� VARCHAR2(50),
  ɾ����־     VARCHAR2(2) default '0',
  ����ʱ��     DATE,
  ������Ա     VARCHAR2(50)
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
comment on table ��������_��Ŀ�ֵ��Ӧ��ϵ
  is '��������_��Ŀ�ֵ��Ӧ��ϵ';
comment on column ��������_��Ŀ�ֵ��Ӧ��ϵ.��������
  is '��������';
comment on column ��������_��Ŀ�ֵ��Ӧ��ϵ.��Ŀ����
  is '��Ŀ����';
comment on column ��������_��Ŀ�ֵ��Ӧ��ϵ.ϵͳ�ֵ�������
  is 'ϵͳ�ֵ�������';
comment on column ��������_��Ŀ�ֵ��Ӧ��ϵ.ϵͳ�ֵ���ϸ����
  is 'ϵͳ�ֵ���ϸ����';
comment on column ��������_��Ŀ�ֵ��Ӧ��ϵ.�ӿ��ֵ�������
  is '�ӿ��ֵ�������';
comment on column ��������_��Ŀ�ֵ��Ӧ��ϵ.�ӿ��ֵ���ϸ����
  is '�ӿ��ֵ���ϸ����';
comment on column ��������_��Ŀ�ֵ��Ӧ��ϵ.ɾ����־
  is 'ɾ����־';
comment on column ��������_��Ŀ�ֵ��Ӧ��ϵ.����ʱ��
  is '����ʱ��';
comment on column ��������_��Ŀ�ֵ��Ӧ��ϵ.������Ա
  is '������Ա';
alter table ��������_��Ŀ�ֵ��Ӧ��ϵ
  add constraint PK_��������_��Ŀ�ֵ��Ӧ��ϵ primary key (��������, ��Ŀ����, ϵͳ�ֵ�������, ϵͳ�ֵ���ϸ����)
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
prompt Creating table ��������_��Ŀ�ֵ����
prompt ==========================
prompt
create table ��������_��Ŀ�ֵ����
(
  ��������   VARCHAR2(50) not null,
  ��Ŀ����   VARCHAR2(50) not null,
  �ֵ������� VARCHAR2(50) not null,
  �ֵ�������� VARCHAR2(100),
  ƴ����    VARCHAR2(50),
  �����    VARCHAR2(50),
  ��Ч״̬   VARCHAR2(50),
  ɾ����־   VARCHAR2(2) default '0',
  ����ʱ��   DATE,
  ������Ա   VARCHAR2(50)
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
comment on table ��������_��Ŀ�ֵ����
  is '��������_��Ŀ�ֵ����';
comment on column ��������_��Ŀ�ֵ����.��������
  is '��������';
comment on column ��������_��Ŀ�ֵ����.��Ŀ����
  is '��Ŀ����';
comment on column ��������_��Ŀ�ֵ����.�ֵ�������
  is '�ֵ�������';
comment on column ��������_��Ŀ�ֵ����.�ֵ��������
  is '�ֵ��������';
comment on column ��������_��Ŀ�ֵ����.ƴ����
  is 'ƴ����';
comment on column ��������_��Ŀ�ֵ����.�����
  is '�����';
comment on column ��������_��Ŀ�ֵ����.��Ч״̬
  is '��Ч״̬';
comment on column ��������_��Ŀ�ֵ����.ɾ����־
  is 'ɾ����־';
comment on column ��������_��Ŀ�ֵ����.����ʱ��
  is '����ʱ��';
comment on column ��������_��Ŀ�ֵ����.������Ա
  is '������Ա';
alter table ��������_��Ŀ�ֵ����
  add constraint PK_��������_��Ŀ�ֵ���� primary key (��������, ��Ŀ����, �ֵ�������)
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
prompt Creating table ��������_��Ŀ�ֵ���ϸ
prompt ==========================
prompt
create table ��������_��Ŀ�ֵ���ϸ
(
  ��������   VARCHAR2(50) not null,
  ��Ŀ����   VARCHAR2(50) not null,
  �ֵ������� VARCHAR2(50) not null,
  �ֵ���ϸ���� VARCHAR2(50) not null,
  �ֵ���ϸ���� VARCHAR2(100),
  ƴ����    VARCHAR2(50),
  �����    VARCHAR2(50),
  ��Ч״̬   VARCHAR2(50),
  ɾ����־   VARCHAR2(2) default '0',
  ����ʱ��   DATE,
  ������Ա   VARCHAR2(50)
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
comment on table ��������_��Ŀ�ֵ���ϸ
  is '��������_��Ŀ�ֵ���ϸ';
comment on column ��������_��Ŀ�ֵ���ϸ.��������
  is '��������';
comment on column ��������_��Ŀ�ֵ���ϸ.��Ŀ����
  is '��Ŀ����';
comment on column ��������_��Ŀ�ֵ���ϸ.�ֵ�������
  is '�ֵ�������';
comment on column ��������_��Ŀ�ֵ���ϸ.�ֵ���ϸ����
  is '�ֵ���ϸ����';
comment on column ��������_��Ŀ�ֵ���ϸ.�ֵ���ϸ����
  is '�ֵ���ϸ����';
comment on column ��������_��Ŀ�ֵ���ϸ.ƴ����
  is 'ƴ����';
comment on column ��������_��Ŀ�ֵ���ϸ.�����
  is '�����';
comment on column ��������_��Ŀ�ֵ���ϸ.��Ч״̬
  is '��Ч״̬';
comment on column ��������_��Ŀ�ֵ���ϸ.ɾ����־
  is 'ɾ����־';
comment on column ��������_��Ŀ�ֵ���ϸ.����ʱ��
  is '����ʱ��';
comment on column ��������_��Ŀ�ֵ���ϸ.������Ա
  is '������Ա';
alter table ��������_��Ŀ�ֵ���ϸ
  add constraint PK_��������_��Ŀ�ֵ���ϸ primary key (��������, ��Ŀ����, �ֵ�������, �ֵ���ϸ����)
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
prompt Creating table ��������_��Ŀ�ֶζ���
prompt ==========================
prompt
create table ��������_��Ŀ�ֶζ���
(
  ��������    VARCHAR2(50) not null,
  ��Ŀ����    VARCHAR2(50) not null,
  ���      NUMBER not null,
  ��Ŀ�ֶ���   VARCHAR2(50),
  ϵͳ�ֶ���   VARCHAR2(50),
  �ֶ�˵��    VARCHAR2(100),
  �Ƿ���ʾ    VARCHAR2(50),
  ��ʾ����    VARCHAR2(50),
  ��ʾ˳��    NUMBER,
  �Ƿ�Ϊ�ֵ�   VARCHAR2(50),
  �ֵ����    VARCHAR2(50),
  ��Ч״̬    VARCHAR2(50),
  ɾ����־    VARCHAR2(2) default '0',
  ����ʱ��    DATE,
  ������Ա    VARCHAR2(50),
  ȷ�ϱ�־    VARCHAR2(50),
  �Ƿ�Ĭ��ֵ   VARCHAR2(50),
  Ĭ��ֵ     VARCHAR2(100),
  �Ƿ�ȡ�Բ���  VARCHAR2(50),
  Ԫ�ػ��ĵ���  VARCHAR2(50),
  �Ƿ����ת���� VARCHAR2(50),
  ת����     VARCHAR2(50),
  ת���ֵ�    VARCHAR2(50)
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
comment on table ��������_��Ŀ�ֶζ���
  is '��������_��Ŀ�ֶζ���';
comment on column ��������_��Ŀ�ֶζ���.��������
  is '��������';
comment on column ��������_��Ŀ�ֶζ���.��Ŀ����
  is '��Ŀ����';
comment on column ��������_��Ŀ�ֶζ���.���
  is '���';
comment on column ��������_��Ŀ�ֶζ���.��Ŀ�ֶ���
  is '��������ʱ��ʾ���ֶ���';
comment on column ��������_��Ŀ�ֶζ���.ϵͳ�ֶ���
  is '���ر��ж�Ӧ���ֶ���';
comment on column ��������_��Ŀ�ֶζ���.�ֶ�˵��
  is '�ֶ�˵��';
comment on column ��������_��Ŀ�ֶζ���.�Ƿ���ʾ
  is '�Ƿ���ʾ';
comment on column ��������_��Ŀ�ֶζ���.��ʾ����
  is '��ʾ����';
comment on column ��������_��Ŀ�ֶζ���.��ʾ˳��
  is '��ʾ˳��';
comment on column ��������_��Ŀ�ֶζ���.�Ƿ�Ϊ�ֵ�
  is '�Ƿ�Ϊ�ֵ�';
comment on column ��������_��Ŀ�ֶζ���.�ֵ����
  is '�ֵ����';
comment on column ��������_��Ŀ�ֶζ���.��Ч״̬
  is '��Ч״̬';
comment on column ��������_��Ŀ�ֶζ���.ɾ����־
  is 'ɾ����־';
comment on column ��������_��Ŀ�ֶζ���.����ʱ��
  is '����ʱ��';
comment on column ��������_��Ŀ�ֶζ���.������Ա
  is '������Ա';
comment on column ��������_��Ŀ�ֶζ���.ȷ�ϱ�־
  is 'ȷ�Ϻ���Ŀ�ֶ�����ϵͳ�ֶ��������޸�';
alter table ��������_��Ŀ�ֶζ���
  add constraint PK_��������_��Ŀ�ֶζ��� primary key (��������, ��Ŀ����, ���)
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


prompt Done
spool off
set define on
