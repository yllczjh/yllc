create or replace view v_lisresult as
select

t.Ψһid as lisid, --��������¼��ʶ String
s.������ as hiscardid, --���￨��  String
s.����id as hispatientid,  --His��ʶ String
'' as barcode, --����  String
s.����ʱ�� as testdate,  --��������  Date
s.��Ŀ���� as examcode,  --������Ŀ����  String
s.��Ŀ���� as examname,  --������Ŀ����    String
'' as samplyid,  --������ʶ  String
'' as sampltype --��������  String

    from ������_��� t,������_���� s
   where t.���뵥id = s.���뵥id and t.�������� = '222403100001' and s.���״̬='�ѱ���' and s.����='����'
;
