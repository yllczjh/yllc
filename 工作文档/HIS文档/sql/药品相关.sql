--1.��ⵥ���˻������̵㵥��
select ���ݺ�,������������,��Դ����,��Դ����,Ŀ�ı���,Ŀ������,����״̬,����������,����ʱ��,С������,��������,��Ŀ����,��Ŀ����,���,��������,��λ����,����1 as ����,������,�����ܶ�,����,��Ч��,���
from ҩ��ҩ��_������ϸ t
where to_char(����ʱ��,'yyyymmdd') between '20191001' and '20191031'
order by ���ݺ�,���

--2.��ҩ��
select '����' as ����,t.���ﲡ����,nvl((select �������� from ������Ŀ_�������� where ��������=t.�������� and ���ұ���=t.�������ұ���),'') as ��������,
nvl((select ��Ա���� from ������Ŀ_��Ա���� where ��������=t.�������� and ��Ա����=t.����ҽ������),'') as ����ҽ��,�շ�ʱ��,t.��������,t.��Ŀ����,t.��Ŀ����,t.���,t.��������,t.��λ����,t.����,t.����,t.����,t.�ܽ��,t.���� as �����۸�,t.����*(t.����-t.����) as ë��,1 as ����С��
from ҩ��ҩ��_���﷢ҩ t
where to_char(t.�շ�ʱ��,'yyyymmdd') between '20191001' and '20191031'
union all
select 'סԺ' as ����,t.סԺ������,nvl((select �������� from ������Ŀ_�������� where ��������=t.�������� and ���ұ���=t.�������ұ���),'') as ��������,
nvl((select ��Ա���� from ������Ŀ_��Ա���� where ��������=t.�������� and ��Ա����=t.����ҽ������),'') as ����ҽ��,t.����ʱ��,t.��������,t.��Ŀ����,t.��Ŀ����,t.���,t.��������,t.��λ����,t.����,t.����,t.����,t.�ܽ��,t.���� as �����۸�,t.����*(t.����-t.����) as ë��,1 as ����С��
from ҩ��ҩ��_סԺ��ҩ t
where to_char(t.����ʱ��,'yyyymmdd') between '20191001' and '20191031'


--3.�����ϸ
select nvl((select �������� from ������Ŀ_�������� where ���ұ���=t.���ұ��� and ��������=t.��������),'') as ����,p.С������,p.��������,p.��������,p.ҩƷ����,p.ҩƷ����,p.���,p.С��λ����,p.��������,
p.��׼�ĺ�,
nvl((select ���� from ������Ŀ_�ֵ���ϸ t where t.�������='GB_KSF9090' and ����=��ҩ���� and nvl(ɾ����־,'0')='0' and ��Ч״̬='��Ч'),'') as ����������,
nvl((select ���� from ������Ŀ_�ֵ���ϸ t where t.�������='GB_009050' and ����=��ҩ���� and nvl(ɾ����־,'0')='0' and ��Ч״̬='��Ч'),'') as ��ҩ����,
t.����,t.��Ч��,t.���� as �������,t.����,t.����*t.���� as �����,1 as ����С��,
nvl((select ��Դ���� from ҩ��ҩ��_������ϸ m where ���κ�=t.���κ� and �������ͱ���='1' and ����״̬='�ѽ���' and not exists(select 1 from ҩ��ҩ��_������ϸ n where ���κ�=t.���κ� and �������ͱ���='1' and ����״̬='�ѽ���' and n.����ʱ��>m.����ʱ��)),'') as ������
from ҩ��ҩ��_������� t,������Ŀ_ҩƷ�ֵ� p
where t.��Ŀ����=p.ҩƷ���� and t.��������=p.�������� and t.����<>0
and p.ƴ���� like '%'||'amxl'||'%'


--4.��ѯ20191001��20191020֮��Ľ��������
select С������,ҩƷ����,ҩƷ����,���,��������,��׼�ĺ�,��������,��������,ƴ����,С��λ����,��Ч״̬,

nvl((select sum(����С����*�������) from ҩ��ҩ��_�����־ t where ��Ŀ����=p.ҩƷ���� and ����ʱ��<to_date('20191001','yyyymmdd')),0.00) as �ڳ�����,

nvl((select sum(����С����*�������) from ҩ��ҩ��_�����־ t where ��Ŀ����=p.ҩƷ���� and to_date(to_char(����ʱ��,'yyyymmdd'),'yyyymmdd') between to_date('20191001','yyyymmdd') and to_date('20191020','yyyymmdd') and ���������� in('1')),0.00) as ��������,

-nvl((select sum(����С����*�������) from ҩ��ҩ��_�����־ t where ��Ŀ����=p.ҩƷ���� and to_date(to_char(����ʱ��,'yyyymmdd'),'yyyymmdd') between to_date('20191001','yyyymmdd') and to_date('20191020','yyyymmdd') and ���������� not in('1')),0.00) as ��������,

nvl((select sum(����С����*�������) from ҩ��ҩ��_�����־ t where ��Ŀ����=p.ҩƷ���� and to_date(to_char(����ʱ��,'yyyymmdd'),'yyyymmdd')<=to_date('20191020','yyyymmdd')),0.00) as ��ĩ����,


nvl((select sum(�����ܶ�*�������) from ҩ��ҩ��_�����־ t where ��Ŀ����=p.ҩƷ���� and ����ʱ��<to_date('20191001','yyyymmdd')),0) as �ڳ����,

nvl((select sum(�����ܶ�*�������) from ҩ��ҩ��_�����־ t where ��Ŀ����=p.ҩƷ���� and to_date(to_char(����ʱ��,'yyyymmdd'),'yyyymmdd') between to_date('20191001','yyyymmdd') and to_date('20191020','yyyymmdd') and ���������� in('1')),0.00) as �������,
-nvl((select sum(�����ܶ�*�������) from ҩ��ҩ��_�����־ t where ��Ŀ����=p.ҩƷ���� and to_date(to_char(����ʱ��,'yyyymmdd'),'yyyymmdd') between to_date('20191001','yyyymmdd') and to_date('20191020','yyyymmdd') and ���������� not in('1')),0.00) as ���۳ɱ�,

nvl((select sum(�����ܶ�*�������) from ҩ��ҩ��_�����־ t where ��Ŀ����=p.ҩƷ���� and to_date(to_char(����ʱ��,'yyyymmdd'),'yyyymmdd')<=to_date('20191020','yyyymmdd')),0.00) as ��ĩ���,1 as ����С��,
nvl((select sum(�����ܶ�) from ҩ��ҩ��_�����־ t where ��Ŀ����=p.ҩƷ���� and to_date(to_char(����ʱ��,'yyyymmdd'),'yyyymmdd') between to_date('20191001','yyyymmdd') and to_date('20191020','yyyymmdd') and ���������� not in('1')),0.00) as ���۽��,
nvl((select sum(�����ܶ�-�����ܶ�) from ҩ��ҩ��_�����־ t where ��Ŀ����=p.ҩƷ���� and to_date(to_char(����ʱ��,'yyyymmdd'),'yyyymmdd') between to_date('20191001','yyyymmdd') and to_date('20191020','yyyymmdd') and ���������� not in('1')),0.00) as ����ë��,
nvl((select max(�����ܶ�/����С����) from ҩ��ҩ��_�����־ t where ��Ŀ����=p.ҩƷ���� and to_date(to_char(����ʱ��,'yyyymmdd'),'yyyymmdd') between to_date('20191001','yyyymmdd') and to_date('20191020','yyyymmdd') and �������ͱ���='1' and ���������� in('1') and ����С����<>0),0.00) as ��߽���,
nvl((select min(�����ܶ�/����С����) from ҩ��ҩ��_�����־ t where ��Ŀ����=p.ҩƷ���� and to_date(to_char(����ʱ��,'yyyymmdd'),'yyyymmdd') between to_date('20191001','yyyymmdd') and to_date('20191020','yyyymmdd') and �������ͱ���='1' and ���������� in('1') and ����С����<>0),0.00) as ��ͽ���
from ������Ŀ_ҩƷ�ֵ� p

