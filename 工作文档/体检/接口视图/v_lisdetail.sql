create or replace view v_lisdetail as
select

t.���浥id as  lisid, --��������¼��ʶ String  ������������¼
t.ϸ������ as itemcode,  --����ϸ�����  String
t.ϸ������ as itemname,  --����ϸ������  String
t.ϸ��ֵ as result,  --���  String
t.��λ as unit,  --��λ  String
t.�ο�ֵ���� as range --��Χ  String

    from ������_���_��ϸ t
   where t.�������� = '222403100001'
;
