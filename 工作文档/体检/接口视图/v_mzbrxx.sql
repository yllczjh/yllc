create or replace view v_mzbrxx as
select
t.����id as hispatientid,  --His��ʶ	String	��ͯ��His�еı�ʶ������
t.���ﲡ���� as hiscardid,  --His����	String	��ͯ����His�еĿ��Ż�����ţ�����
b.���� as name,    --����	String	����
decode(b.�Ա�,'1','��','2','Ů','δ֪') as gender,   --�Ա�	String	����
b.�������� as birthday    --��������	Date	����

from �������_�ҺŵǼ� t,������Ŀ_������Ϣ b
where t.��������=b.�������� and t.����ID=b.����ID and t.��������='222403100001'
;
