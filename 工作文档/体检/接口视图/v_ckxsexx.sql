create or replace view v_ckxsexx as
select

t.סԺ������ as hiscardid, --���￨��  String  ��hisϵͳ��ͬ������������ڳ���ʱû�м�¼���������His���￨�� ����
t.����id as hispatientid,  --His��ʶ String  ��hisϵͳ��ͬ������������ڳ���ʱû�м�¼���������His��ʶ    ����
b.���� as name,  --����  String  ������������
decode(b.�Ա�,'1','��','2','Ů','δ֪') as gender,  --�Ա�  String  ���������Ա� ����
b.�������� as birthday  --��������  Date  �������ĳ������� ����

    from סԺ����_��Ժ������Ϣ t,������Ŀ_������Ϣ b
where t.��������=b.�������� and t.����id=b.����id and t.��������='222403100001' and t.ĸ�ײ����� is not null
;
