create or replace view v_pacsresult as
select
s.������ as hiscardid, --���￨��  String
s.����id as hispatientid,  --His��ʶ String
t.���뵥id as applyNum,  --�������  String
'' as barcode, --����  String
t.����ʱ�� as testdate,  --��������  Date
s.��Ŀ���� as subtype,  --������  String  �������Ź���DR�ȵ�
s.��Ŀ���� as type, --�������  String  ����B�����ز�DR�ȵ�
s.��Ŀ���� as code,  --��Ŀ����  String
s.��Ŀ���� as name,  --��Ŀ����    String
s.ҽ������ as doctor,  --����ҽ��  String
t.���ֱ������� as Findings,  --�������  String
t.���ֱ������� as Impression,  --ӡ��  String
t.ͼƬ�����ļ� as Image --Ӱ��  Image

    from ������_��� t,������_���� s
    where t.���뵥id = s.���뵥id and s.���״̬='�ѱ���' and s.����='���' and s.��������='222403100001'
;
