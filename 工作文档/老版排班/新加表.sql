alter table �������_�����Ű��¼ add ����״̬ varchar2(50);
COMMENT ON COLUMN �������_�����Ű��¼.����״̬ IS '0ͣ�� 1���� 2��δ����';


alter table ������Ŀ_�������� add �Ƿ��Ű� varchar2(50);
update ������Ŀ_�������� set �Ƿ��Ű�='��';


alter table �������_ԤԼ�Һ� add ԤԼʱ�α��� varchar2(50);
alter table �������_ԤԼ�Һ� add ԤԼʱ�ο�ʼ date;
alter table �������_ԤԼ�Һ� add ԤԼʱ�ν��� date;
alter table �������_ԤԼ�Һ� add ֧����־ varchar2(50);
alter table �������_ԤԼ�Һ� add ��ʱʱ�� date;
alter table �������_ԤԼ�Һ� add ȡ��ʱ�� date;
alter table �������_ԤԼ�Һ� add �Һŷ� NUMBER(18,3);
alter table �������_ԤԼ�Һ� add ���� NUMBER(18,3);
alter table �������_ԤԼ�Һ� add ������� varchar2(50);
alter table �������_ԤԼ�Һ� add �Һ��������� varchar2(50);
alter table �������_ԤԼ�Һ� add �Һ�ҽ������ varchar2(50);
alter table �������_ԤԼ�Һ� add �Һſ������� varchar2(50);
alter table �������_ԤԼ�Һ� add �հ�α�ʶ varchar2(50);

alter table �������_�ҺŵǼ� add ����ʱ�� date;
alter table �������_�ҺŵǼ� add ԤԼ��ʼʱ�� date;
alter table �������_�ҺŵǼ� add ԤԼ����ʱ�� date;
alter table �������_�ҺŵǼ� add �հ�α�ʶ varchar2(50);


create sequence SEQ_�������_���Ű�_�Ű��ʶ
minvalue 1
maxvalue 9999999999
start with 1
increment by 1
cache 10;



-- Create table
create table �������_���Ű�ʱ�α�
(
  �հ�α�ʶ  VARCHAR2(50) not null,
  ��������   VARCHAR2(50),
  �Ű����   VARCHAR2(50),
  ��¼id   VARCHAR2(50),
  �޺����ͱ��� VARCHAR2(50),
  ʱ�α���   VARCHAR2(50),
  ��ʼʱ��   VARCHAR2(50),
  ����ʱ��   VARCHAR2(50),
  �޺���    INTEGER,
  ˳���    INTEGER default 0 not null,
  ��Ч״̬   VARCHAR2(50),
  �ѹҺ���   INTEGER,
  ֧�ֹ���   VARCHAR2(50),
  ʱ�η������ VARCHAR2(50),
  CONSTRAINT KP_�������_���Ű�ʱ�α� PRIMARY KEY (�հ�α�ʶ)
);

-- Add comments to the table 
comment on table �������_���Ű�ʱ�α�
  is '�������_���Ű�ʱ�α�';
-- Add comments to the columns 
comment on column �������_���Ű�ʱ�α�.�հ�α�ʶ
  is '�հ�α�ʶ';
comment on column �������_���Ű�ʱ�α�.��������
  is '��������';
comment on column �������_���Ű�ʱ�α�.�Ű����
  is '�Ű����';
comment on column �������_���Ű�ʱ�α�.��¼id
  is '��¼ID';
comment on column �������_���Ű�ʱ�α�.�޺����ͱ���
  is '�޺����ͱ���';
comment on column �������_���Ű�ʱ�α�.ʱ�α���
  is 'ʱ�α���';
comment on column �������_���Ű�ʱ�α�.��ʼʱ��
  is '��ʼʱ��';
comment on column �������_���Ű�ʱ�α�.����ʱ��
  is '����ʱ��';
comment on column �������_���Ű�ʱ�α�.�޺���
  is '�޺���';
comment on column �������_���Ű�ʱ�α�.˳���
  is '˳���';
comment on column �������_���Ű�ʱ�α�.��Ч״̬
  is '��Ч����Ч';
comment on column �������_���Ű�ʱ�α�.�ѹҺ���
  is '�ѹҺ���';
comment on column �������_���Ű�ʱ�α�.֧�ֹ���
  is '֧�ֹ���';
comment on column �������_���Ű�ʱ�α�.ʱ�η������
  is 'ʱ�η������';


  
  
  
  
  
  
  
  -- Create table
create table �������_���Ű�ʱ�α�
(
  ��������   VARCHAR2(50) not null,
  �Ű����   VARCHAR2(50) not null,
  ʱ�α���   VARCHAR2(50) not null,
  �޺����ͱ��� VARCHAR2(50) not null,
  ��ʼʱ��   VARCHAR2(50),
  ����ʱ��   VARCHAR2(50),
  �޺���    INTEGER,
  ˳���    INTEGER default 0 not null,
  ��Ч״̬   VARCHAR2(50),
  ֧�ֹ���   VARCHAR2(50),
  ʱ�η������ VARCHAR2(50),
  CONSTRAINT KP_�������_���Ű�ʱ�α� PRIMARY KEY (��������, �Ű����, ʱ�α���, �޺����ͱ���)
);

-- Add comments to the table 
comment on table �������_���Ű�ʱ�α�
  is '�������_���Ű�ʱ�α�';
-- Add comments to the columns 
comment on column �������_���Ű�ʱ�α�.��������
  is '��������';
comment on column �������_���Ű�ʱ�α�.�Ű����
  is '�Ű����';
comment on column �������_���Ű�ʱ�α�.ʱ�α���
  is 'ʱ�α���';
comment on column �������_���Ű�ʱ�α�.�޺����ͱ���
  is '�޺����ͱ���';
comment on column �������_���Ű�ʱ�α�.��ʼʱ��
  is '��ʼʱ��';
comment on column �������_���Ű�ʱ�α�.����ʱ��
  is '����ʱ��';
comment on column �������_���Ű�ʱ�α�.�޺���
  is '�޺���';
comment on column �������_���Ű�ʱ�α�.˳���
  is '˳���';
comment on column �������_���Ű�ʱ�α�.��Ч״̬
  is '��Ч����Ч';
comment on column �������_���Ű�ʱ�α�.֧�ֹ���
  is '֧�ֹ���';
comment on column �������_���Ű�ʱ�α�.ʱ�η������
  is 'ʱ�η������';

  
  
  
  
  
  
  
  -- Create table
create table �������_ԤԼʱ���ֵ�
(
  ��������   VARCHAR2(50) not null,
  ʱ�α���   VARCHAR2(50) not null,
  ��ʼʱ��   VARCHAR2(50),
  ����ʱ��   VARCHAR2(50),
  ˳���    INTEGER default 0 not null,
  ��Ч״̬   VARCHAR2(50) default '��Ч' not null,
  ʱ�η������ VARCHAR2(50),
  CONSTRAINT KP_�������_ԤԼʱ���ֵ� PRIMARY KEY (��������, ʱ�α���)
);

-- Add comments to the columns 
comment on column �������_ԤԼʱ���ֵ�.��������
  is '��������';
comment on column �������_ԤԼʱ���ֵ�.ʱ�α���
  is 'ʱ�α���';
comment on column �������_ԤԼʱ���ֵ�.��ʼʱ��
  is '��ʼʱ��';
comment on column �������_ԤԼʱ���ֵ�.����ʱ��
  is '����ʱ��';
comment on column �������_ԤԼʱ���ֵ�.˳���
  is '˳���';
comment on column �������_ԤԼʱ���ֵ�.��Ч״̬
  is '��Ч����Ч';
comment on column �������_ԤԼʱ���ֵ�.ʱ�η������
  is 'ʱ�η������';

  
  
  
  
  -- Create table
create table �������_�޺������ֵ�
(
  �޺����ͱ��� VARCHAR2(50) not null,
  �޺��������� VARCHAR2(50),
  ֧�ֹ���   VARCHAR2(50),
  ˳���    INTEGER default 0 not null,
  ��Ч״̬   VARCHAR2(50) not null,
  CONSTRAINT KP_�������_�޺������ֵ� PRIMARY KEY (�޺����ͱ���)
);

-- Add comments to the table 
comment on table �������_�޺������ֵ�
  is '�������_�޺������ֵ�';
-- Add comments to the columns 
comment on column �������_�޺������ֵ�.�޺����ͱ���
  is '�޺����ͱ���';
comment on column �������_�޺������ֵ�.�޺���������
  is '�޺���������';
comment on column �������_�޺������ֵ�.֧�ֹ���
  is '֧�ֹ���';
comment on column �������_�޺������ֵ�.˳���
  is '˳���';
comment on column �������_�޺������ֵ�.��Ч״̬
  is '��Ч����Ч';

  
insert into �������_�޺������ֵ� (�޺����ͱ���, �޺���������, ֧�ֹ���, ˳���, ��Ч״̬)
values ('-1', '��������', '��', 1, '��Ч');

insert into �������_�޺������ֵ� (�޺����ͱ���, �޺���������, ֧�ֹ���, ˳���, ��Ч״̬)
values ('1', '���ڹҺ�', '��', 2, '��Ч');

insert into �������_�޺������ֵ� (�޺����ͱ���, �޺���������, ֧�ֹ���, ˳���, ��Ч״̬)
values ('2', '����ԤԼ', '��', 3, '��Ч');

  
  
  
  
insert into ������Ŀ_�ֵ���� (�������, ��������, ƴ����, �����, ���, �ϼ�����, ������׼, ���б�־, ��Ч״̬, ɾ����־, ����ʱ��)
values ('ԤԼʱ�η���', 'ԤԼʱ�η���', null, null, null, 'GB_02', null, '0', '1', '0', to_date('27-10-2011', 'dd-mm-yyyy'));

insert into ������Ŀ_�ֵ���ϸ (����, ����, ƴ����, �����, �Ա���, �������, ��Ч״̬, �ϼ�����, �Ƿ�Ĭ��, ɾ����־, ����ʱ��, ������)
values ('1', 'ȫ��', null, null, null, 'ԤԼʱ�η���', '��Ч', 'GB_02', null, '0', to_date('22-04-2012 16:20:10', 'dd-mm-yyyy hh24:mi:ss'), null);

insert into ������Ŀ_�ֵ���ϸ (����, ����, ƴ����, �����, �Ա���, �������, ��Ч״̬, �ϼ�����, �Ƿ�Ĭ��, ɾ����־, ����ʱ��, ������)
values ('2', '��ʱ', null, null, null, 'ԤԼʱ�η���', '��Ч', 'GB_02', null, '0', to_date('22-04-2012 16:20:10', 'dd-mm-yyyy hh24:mi:ss'), null);
