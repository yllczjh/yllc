--select * from cant_inp_patient_view;

/*--���˻�����Ϣ
select t.yydm as his_code,
       'Ӫ������ҽ���ҽԺ' as his_mc,
       t.zyh as mz_jz_zyh,--סԺ��
       t.brid as br_id,--���￨��
       t.zycs as lsh_num,-- ������ˮ��/סԺ����
       t.brxm as br_mc,--��������
       t.csrq as birthday,--��������
       t.xb as sex,--�Ա� 1  Ϊ����   2ΪŮ
       nvl(t.tz,0) as weight,--���� �޷�ȷ������0����λKG
       nvl(t.sg,0) as height,--��� �޷�ȷ������0����λCM
       '������' as cfh,--������
       '' as review_id,--HIS������Ψһʶ��
       t.ksdm as dept_code,--������ұ���
       t.ksmc as dept_mc,--�����������
       t.ysdm as doctor_code,--����ҽ������
       t.ysmc as doctor_mc,--����ҽ������
       t.gshcd as gz_tag,--����̶�
       t.buruzt as is_brq,--����
       t.rszt as is_rs,--����
       t.brzt as data_cat,--�������� 1 סԺ 2���� 3����
       t.rsztsj as rs_start_time,--���￪ʼʱ��
       t.sshcd as sz_tag,--����̶�
       '' as ybbr,--ҽ������
       '' as jun_ren ---��������
  from cant_inp_patient_view t;*/

--���������Ϣ
/*select t.brid,
     t.zycs,
     t.zddm as zddm, --��ϱ���
     row_number() over(partition by t.brid order by t.zddm) as zdxh, --������
     t.zdmc as zdmc, --�������
     '������' as cfh
from cant_inp_disease_view t;*/

--����������Ϣ
/*select t.brid,
     t.zycs,
     row_number() over(partition by t.brid order by t.ssdm) as operxh, --�������
     t.ssdm as oper_code, --��������
     t.ssmc as oper_mc, --��������
     t.ssqk as oper_qk, --�����п�����
     t.sskssj as oper_ks_time, --������ʼʱ��
     t.ssjssj as oper_js_time --��������ʱ��
from cant_inp_operation_view t;*/

--���˹�����Ϣ
/*select t.yydm,
     t.brid,
     t.zycs,
     row_number() over(partition by t.brid order by t.gmydm) as gmyxh, --����Դ���
     t.gmydm as gmy_code, --����Դ����
     t.gmymc as gmy_mc, --����Դ����
     t.gmzz as gmzz_ms --����֢״
from cant_inp_allergen_view t;*/

--���˼�����Ϣ
/*select t.yydm,
       t.brid,
       t.zycs,
       t.jyxmdm   as lab_code, --�������
       t.jyxmmc   as lab_name, --��������
       t.jyjg     as lab_result, --������
       t.jyjgbz   as result_flag, --�����־
       t.jyjgckfw as lab_value, --�ο���Χ
       t.dw       as lab_dw --��������λ
  from cant_inp_lab_view t;*/
  
  
  --����ҽ����Ϣ
  
  select t.yydm,
       t.brid,
       t.zycs,
       t.ksdm as dept_code,--�������ұ���
       t.ksmc as dept_mc,--������������
       t.ysdm as doctor_code,--����ҽ������
       t.ysmc as doctore_mc,--����ҽ������
       t.yzdm as ypdm,--ҩƷ����
       t.ypwydm as ypwydm,--ҩƷΨһ��
       t.yzmc as ypmc,--ҩƷ����
       t.dcjl as liang_ci,--ÿ�μ���
       t.kfgydw as gydw,--ÿ�μ�����λ
       0 as kf_num,--��������
       '' as kf_dw,--����������λ
       t.kssj as time_kz,--��ҩ��ʼʱ��
       0 as cxts,--��ҩ��������
       t.jssj as time_tz,--��ҩֹͣʱ��
       t.zxsj as time_zx,--��ҩִ��ʱ��
       t.pc as pinci,--��ҩƵ��
       t.czbj as tag,--�������
       '������' as cfh, --������
       0 as cf_type, --��������
       t.cqlsyz as sflsyy,--�Ƿ���ʱҽ��
       0 as ypxh,--ҩƷ���
       t.yzxh as yzxh,--ҽ�����
       0 as yzlx,--ҽ������
       t.gyfsdm as route_code,--��ҩ��ʽ����
       t.gyfsmc as route_mc,--��ҩ��ʽ����
       
       
       from cant_inp_orders_view t;


