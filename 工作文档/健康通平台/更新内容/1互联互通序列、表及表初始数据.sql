create sequence SEQ_������ͨ_������־_��ˮ��
minvalue 1
maxvalue 9999999999
start with 1
increment by 1
cache 10;


create sequence SEQ_������ͨ_����_��ˮ��
minvalue 1
maxvalue 9999999999
start with 1
increment by 1
cache 10;


create sequence SEQ_������ͨ_������ϸ_��ˮ��
minvalue 1
maxvalue 9999999999
start with 1
increment by 1
cache 10;


create sequence SEQ_������ͨ_�û���Ϣ_��ˮ��
minvalue 1
maxvalue 9999999999
start with 1
increment by 1
cache 10;



CREATE TABLE ������ͨ_����(
    ��ˮ�� NUMBER NOT NULL,
    ƽ̨��ʶ VARCHAR2(50),
    ҽԺ���� VARCHAR2(50),
    ����ID VARCHAR2(50),
    ���ﲡ���� VARCHAR2(50),
    �������� VARCHAR2(50),
    ����״̬ VARCHAR2(50),
    ҽԺ������ VARCHAR2(50),
    ƽ̨������ VARCHAR2(50),
    ����ʱ�� DATE,
    �Һŷ��� NUMBER(18,3),
    ���Ʒ��� NUMBER(18,3),
    �Һ����� VARCHAR2(50),
    �Һ����� VARCHAR2(50),
    ԤԼ�Һ����� VARCHAR2(50),
    ҽԺ֧���� VARCHAR2(50),
    ƽ̨֧���� VARCHAR2(50),
    ֧��ʱ�� DATE,
    ƽ̨������ˮ�� VARCHAR2(50),
    ֧������ VARCHAR2(50),
    �ܽ�� NUMBER(18,3),
    Ӧ����� NUMBER(18,3),
    ʵ����� NUMBER(18,3),
    ҽ��ͳ��֧����� NUMBER(18,3),
    ҽԺ�˿�� VARCHAR2(50),
    ƽ̨�˿�� VARCHAR2(50),
    �˿�ʱ�� DATE,
    ƽ̨�˿���ˮ�� VARCHAR2(50),
    �˿��� NUMBER(18,3),
    �˿�ԭ�� VARCHAR2(100),
    �˿��־ VARCHAR2(50),
    �������� VARCHAR2(50),
    ����ʱ�� DATE,
    ȡ��ʱ�� DATE,
    ȡ��ԭ�� VARCHAR2(100),
    ҽԺ����� VARCHAR2(50),
    ������ VARCHAR2(50),
    ����ʱ�� DATE,
    ������ VARCHAR2(50),
    ����ʱ�� DATE,
    ����� INTEGER,
    ��ע VARCHAR2(1000),
    ״̬ VARCHAR2(50),
    CONSTRAINT PK_������ͨ_���� PRIMARY KEY (��ˮ��)
);

COMMENT ON TABLE ������ͨ_���� IS '������ͨ_����';
COMMENT ON COLUMN ������ͨ_����.��ˮ�� IS '��ˮ��';
COMMENT ON COLUMN ������ͨ_����.ƽ̨��ʶ IS 'ƽ̨��ʶ';
COMMENT ON COLUMN ������ͨ_����.ҽԺ���� IS 'ҽԺ����';
COMMENT ON COLUMN ������ͨ_����.����ID IS '����ID';
COMMENT ON COLUMN ������ͨ_����.���ﲡ���� IS '���ﲡ����';
COMMENT ON COLUMN ������ͨ_����.�������� IS 'ԤԼ�Һű������ֵ����������ϸ���շ����';
COMMENT ON COLUMN ������ͨ_����.����״̬ IS '�����ţ���֧������֧�������˿��ȡ������ɾ����';
COMMENT ON COLUMN ������ͨ_����.ҽԺ������ IS 'ҽԺ������';
COMMENT ON COLUMN ������ͨ_����.ƽ̨������ IS 'ƽ̨������';
COMMENT ON COLUMN ������ͨ_����.����ʱ�� IS '����ʱ��';
COMMENT ON COLUMN ������ͨ_����.�Һŷ��� IS '�Һŷ���';
COMMENT ON COLUMN ������ͨ_����.���Ʒ��� IS '���Ʒ���';
COMMENT ON COLUMN ������ͨ_����.�Һ����� IS '1���ˣ�2��Ů��3����';
COMMENT ON COLUMN ������ͨ_����.�Һ����� IS '�Һ�����';
COMMENT ON COLUMN ������ͨ_����.ԤԼ�Һ����� IS '1����Һţ�2ԤԼ�Һţ�3���ŹҺ�';
COMMENT ON COLUMN ������ͨ_����.ҽԺ֧���� IS '��Ʊ���';
COMMENT ON COLUMN ������ͨ_����.ƽ̨֧���� IS 'ƽ̨֧����';
COMMENT ON COLUMN ������ͨ_����.֧��ʱ�� IS '֧��ʱ��';
COMMENT ON COLUMN ������ͨ_����.ƽ̨������ˮ�� IS 'ƽ̨������ˮ��';
COMMENT ON COLUMN ������ͨ_����.֧������ IS '1-5ƽ̨��6����';
COMMENT ON COLUMN ������ͨ_����.�ܽ�� IS '�ܽ��';
COMMENT ON COLUMN ������ͨ_����.Ӧ����� IS 'Ӧ�����';
COMMENT ON COLUMN ������ͨ_����.ʵ����� IS 'ʵ�����';
COMMENT ON COLUMN ������ͨ_����.ҽ��ͳ��֧����� IS 'ҽ��ͳ��֧�����';
COMMENT ON COLUMN ������ͨ_����.ҽԺ�˿�� IS 'ҽԺ�˿��';
COMMENT ON COLUMN ������ͨ_����.ƽ̨�˿�� IS 'ƽ̨�˿��';
COMMENT ON COLUMN ������ͨ_����.�˿�ʱ�� IS '�˿�ʱ��';
COMMENT ON COLUMN ������ͨ_����.ƽ̨�˿���ˮ�� IS 'ƽ̨�˿���ˮ��';
COMMENT ON COLUMN ������ͨ_����.�˿��� IS '�˿���';
COMMENT ON COLUMN ������ͨ_����.�˿�ԭ�� IS '�˿�ԭ��';
COMMENT ON COLUMN ������ͨ_����.�˿��־ IS '0ʧ�ܣ�1�ҷ��˿2Ժ���˿�';
COMMENT ON COLUMN ������ͨ_����.�������� IS 'ԤԼ�Һţ�ԤԼ�˺ţ������շѣ�Ԥ��Ѻ�𣻳�Ժ����';
COMMENT ON COLUMN ������ͨ_����.����ʱ�� IS '����ʱ��';
COMMENT ON COLUMN ������ͨ_����.ȡ��ʱ�� IS 'ȡ��ʱ��';
COMMENT ON COLUMN ������ͨ_����.ȡ��ԭ�� IS 'ȡ��ԭ��';
COMMENT ON COLUMN ������ͨ_����.ҽԺ����� IS 'ҽԺ�����';
COMMENT ON COLUMN ������ͨ_����.������ IS '������';
COMMENT ON COLUMN ������ͨ_����.����ʱ�� IS '����ʱ��';
COMMENT ON COLUMN ������ͨ_����.������ IS '������';
COMMENT ON COLUMN ������ͨ_����.����ʱ�� IS '����ʱ��';
COMMENT ON COLUMN ������ͨ_����.����� IS '�����';
COMMENT ON COLUMN ������ͨ_����.��ע IS '��ע';
COMMENT ON COLUMN ������ͨ_����.״̬ IS '״̬';






CREATE TABLE ������ͨ_������ϸ(
    ��ˮ�� NUMBER NOT NULL,
    ������ VARCHAR2(50),
    ������� VARCHAR2(50),
    С����� VARCHAR2(50),
    ��Ŀ���� VARCHAR2(50),
    ��Ŀ���� VARCHAR2(100),
    ��� VARCHAR2(100),
    ���κ� VARCHAR2(50),
    ���� NUMBER(10,4),
    ��λ VARCHAR2(50),
    ���� NUMBER(10,4),
    �ܽ�� NUMBER(10,4),
    ������� VARCHAR2(50),
    Ψһ���� VARCHAR2(50),
    CONSTRAINT PK_������ͨ_������ϸ PRIMARY KEY (��ˮ��)
);

COMMENT ON TABLE ������ͨ_������ϸ IS '������ͨ_������ϸ';
COMMENT ON COLUMN ������ͨ_������ϸ.��ˮ�� IS '��ˮ��';
COMMENT ON COLUMN ������ͨ_������ϸ.������ IS '������';
COMMENT ON COLUMN ������ͨ_������ϸ.������� IS '�������';
COMMENT ON COLUMN ������ͨ_������ϸ.С����� IS 'С�����';
COMMENT ON COLUMN ������ͨ_������ϸ.��Ŀ���� IS '��Ŀ����';
COMMENT ON COLUMN ������ͨ_������ϸ.��Ŀ���� IS '��Ŀ����';
COMMENT ON COLUMN ������ͨ_������ϸ.��� IS '���';
COMMENT ON COLUMN ������ͨ_������ϸ.���κ� IS '���κ�';
COMMENT ON COLUMN ������ͨ_������ϸ.���� IS '����';
COMMENT ON COLUMN ������ͨ_������ϸ.��λ IS '��λ';
COMMENT ON COLUMN ������ͨ_������ϸ.���� IS '����';
COMMENT ON COLUMN ������ͨ_������ϸ.�ܽ�� IS '�ܽ��';
COMMENT ON COLUMN ������ͨ_������ϸ.������� IS '�������';
COMMENT ON COLUMN ������ͨ_������ϸ.Ψһ���� IS 'Ψһ����';





CREATE TABLE ������ͨ_������־(
    ��ˮ�� VARCHAR2(50) NOT NULL,
    ƽ̨��ʶ VARCHAR2(50),
    ҽԺ���� VARCHAR2(50),
    ���ܱ��� VARCHAR2(50),
    ������� VARCHAR2(4000),
    ����ʱ�� DATE,
    ����ֵ NUMBER,
    ������Ϣ VARCHAR2(1000),
    ִ��ʱ�� DATE,
    CONSTRAINT PK_������ͨ_������־ PRIMARY KEY (��ˮ��)
);

COMMENT ON TABLE ������ͨ_������־ IS '������ͨ_������־';
COMMENT ON COLUMN ������ͨ_������־.��ˮ�� IS '��ˮ��';
COMMENT ON COLUMN ������ͨ_������־.ƽ̨��ʶ IS 'ƽ̨��ʶ';
COMMENT ON COLUMN ������ͨ_������־.ҽԺ���� IS 'ҽԺ����';
COMMENT ON COLUMN ������ͨ_������־.���ܱ��� IS '���ܱ���';
COMMENT ON COLUMN ������ͨ_������־.������� IS '�������';
COMMENT ON COLUMN ������ͨ_������־.����ʱ�� IS '����ʱ��';
COMMENT ON COLUMN ������ͨ_������־.����ֵ IS '����ֵ';
COMMENT ON COLUMN ������ͨ_������־.������Ϣ IS '������Ϣ';
COMMENT ON COLUMN ������ͨ_������־.ִ��ʱ�� IS 'ִ��ʱ��';








CREATE TABLE ������ͨ_�û���Ϣ(
    ��ˮ�� VARCHAR2(50) NOT NULL,
    ҽԺ���� VARCHAR2(50),
    ƽ̨��ʶ VARCHAR2(50),
    ����ID VARCHAR2(50),
    �û���� VARCHAR2(50),
    ���� VARCHAR2(50),
    �Ա� VARCHAR2(50),
    �������� DATE,
    ֤������ VARCHAR2(50),
    ֤������ VARCHAR2(50),
    ֤����֤���� VARCHAR2(50),
    ֤����Ч���� VARCHAR2(50),
    �ֻ����� VARCHAR2(50),
    ��ϵ��ַ VARCHAR2(100),
    �໤��֤������ VARCHAR2(50),
    �໤��֤������ VARCHAR2(50),
    �໤������ VARCHAR2(50),
    �û������� VARCHAR2(50),
    �û����� VARCHAR2(50),
    ������ VARCHAR2(50),
    ����ʱ�� DATE,
    CONSTRAINT PK_������ͨ_�û���Ϣ PRIMARY KEY (��ˮ��)
);

COMMENT ON TABLE ������ͨ_�û���Ϣ IS '������ͨ_�û���Ϣ';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.��ˮ�� IS '��ˮ��';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.ҽԺ���� IS 'ҽԺ����';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.ƽ̨��ʶ IS 'ƽ̨��ʶ';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.����ID IS '����ID';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.�û���� IS '�û����';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.���� IS '����';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.�Ա� IS '�Ա�';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.�������� IS '��������';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.֤������ IS '֤������';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.֤������ IS '֤������';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.֤����֤���� IS '֤����֤����';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.֤����Ч���� IS '֤����Ч����';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.�ֻ����� IS '�ֻ�����';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.��ϵ��ַ IS '��ϵ��ַ';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.�໤��֤������ IS '�໤��֤������';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.�໤��֤������ IS '�໤��֤������';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.�໤������ IS '�໤������';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.�û������� IS '�û�������';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.�û����� IS '�û�����';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.������ IS '������';
COMMENT ON COLUMN ������ͨ_�û���Ϣ.����ʱ�� IS '����ʱ��';








CREATE TABLE ������ͨ_ƽ̨��������(
    ��ˮ�� VARCHAR2(50) NOT NULL,
    ƽ̨��ʶ VARCHAR2(50),
    ƽ̨���� VARCHAR2(50),
    �û�ID VARCHAR2(50),
    ��֤��Կ VARCHAR2(50),
    ҽԺID VARCHAR2(50),
    �������� VARCHAR2(50),
    url��ַ VARCHAR2(50),
    ���� VARCHAR2(50),
    ������ VARCHAR2(50),
    ֧����ʽ VARCHAR2(50),
    ������� NUMBER(18,3),
    ����ԤԼ���� VARCHAR2(1000),
    ��Ч״̬ VARCHAR2(50),
    ������ VARCHAR2(50),
    ����ʱ�� DATE,
    ������ VARCHAR2(50),
    ����ʱ�� DATE,
    CONSTRAINT PK_������ͨ_ƽ̨�������� PRIMARY KEY (��ˮ��)
);

COMMENT ON TABLE ������ͨ_ƽ̨�������� IS '������ͨ_ƽ̨��������';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.��ˮ�� IS '��ˮ��';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.ƽ̨��ʶ IS 'ƽ̨��ʶ';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.ƽ̨���� IS 'ƽ̨����';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.�û�ID IS '�û�ID';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.��֤��Կ IS '��֤��Կ';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.ҽԺID IS 'ƽ̨��ҽԺ��Ψһ��ʶ';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.�������� IS 'his��ҽԺ��Ψһ��ʶ';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.url��ַ IS 'url��ַ';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.���� IS '����';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.������ IS '������';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.֧����ʽ IS '֧����ʽ';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.������� IS 'his��ƽ̨����Ļ������';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.����ԤԼ���� IS '��������ƽ̨ԤԼ�Ŀ���';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.��Ч״̬ IS '1��Ч��0��Ч';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.������ IS '������';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.����ʱ�� IS '����ʱ��';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.������ IS '������';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.����ʱ�� IS '����ʱ��';






CREATE TABLE ������ͨ_ƽ̨��������(
    ��ˮ�� VARCHAR2(50) NOT NULL,
    ƽ̨��ʶ VARCHAR2(50),
    ���ܱ��� VARCHAR2(50),
    �������� VARCHAR2(50),
    ״̬ VARCHAR2(50),
    ��ע VARCHAR2(100),
    ������ VARCHAR2(50),
    ����ʱ�� DATE,
    ������ VARCHAR2(50),
    ����ʱ�� DATE,
    CONSTRAINT PK_������ͨ_ƽ̨�������� PRIMARY KEY (��ˮ��)
);

COMMENT ON TABLE ������ͨ_ƽ̨�������� IS '������ͨ_ƽ̨��������';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.��ˮ�� IS '��ˮ��';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.ƽ̨��ʶ IS 'ƽ̨��ʶ';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.���ܱ��� IS '���ܱ���';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.�������� IS '��������';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.״̬ IS '״̬';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.��ע IS '��ע';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.������ IS '������';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.����ʱ�� IS '����ʱ��';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.������ IS '������';
COMMENT ON COLUMN ������ͨ_ƽ̨��������.����ʱ�� IS '����ʱ��';








insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('1', '12320', '1001', '����ͨ�Ų���', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('2', '12320', '1002', '�û���Ϣע��', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('3', '12320', '1003', '�û���Ϣ��ѯ', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('4', '12320', '1004', 'ҽԺ��Ϣ��ѯ', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('5', '12320', '1005', '�û�����֤', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('6', '12320', '2001', '���Ҳ�ѯ', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('7', '12320', '2002', 'ҽ����ѯ', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('8', '12320', '2003', '�Ű���Ϣ��ѯ', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('9', '12320', '2004', '�Ű��ʱ��ѯ', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('10', '12320', '2005', '��Դ����', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('11', '12320', '2006', '������Դ����', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('12', '12320', '2007', 'ԤԼ�ҺŵǼ�', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('13', '12320', '2008', 'ԤԼ�Һ�֧��', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('14', '12320', '2009', 'ԤԼ�Һ�ȡ��', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('15', '12320', '2010', 'ԤԼ�Һ��˺�', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('16', '12320', '2011', 'ԤԼ�Һ�ȡ��', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('17', '12320', '2012', '�Һż�¼��ѯ', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('18', '12320', '2020', 'ҽ���������ݲ�ѯ', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('19', '12320', '3001', '�ɷѼ�¼��ѯ', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('20', '12320', '3002', '�ɷ���ϸ��ѯ', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('21', '12320', '3003', '���ɷѼ�¼֧��', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('22', '12320', '3004', '�ɷѶ�����ѯ', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('23', '12320', '4001', '�Ŷ��б��ѯ', '0', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('24', '12320', '8001', '���/�����б��ѯ', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('25', '12320', '8002', '���鱨���ѯ(��ͨ���)', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('26', '12320', '8003', '���鱨���ѯ(ҩ�����)', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('27', '12320', '8004', '��鱨���ѯ', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('28', '12320', '9003', 'ϵͳ������ѯ', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);

insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ���ܱ���, ��������, ״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('29', '12320', '5004', '����ȡ��', '1', '522633020000001', to_date('07-07-2020 12:12:23', 'dd-mm-yyyy hh24:mi:ss'), null, null);








insert into ������ͨ_ƽ̨�������� (��ˮ��, ƽ̨��ʶ, ƽ̨����, �û�ID, ��֤��Կ, ҽԺID, ��������, URL��ַ, ����, ������, ֧����ʽ, �������, ����ԤԼ����, ��Ч״̬, ������, ����ʱ��, ������, ����ʱ��)
values ('1', '12320', '12320����', 'ln_12320wx', '2098D32C4D1399EC', '1', '522633020000001', 'http://localhost:8001/APIService.asmx', 'APIService', 'PubService', '����֧��', 100.000, '1020,1018', '1', null, null, null, null);

commit;
