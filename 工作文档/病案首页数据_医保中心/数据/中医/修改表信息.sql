update ��������_��Ŀ�ֶζ��� set �Ƿ�Ĭ��ֵ='False';

alter table ��������_��Ŀ�ֶζ��� add �Ƿ�Ĭ��ֵ varchar2(50);
alter table ��������_��Ŀ�ֶζ��� add Ĭ��ֵ varchar2(100);


ALTER TABLE ��ʱ��_������ҳ�� RENAME TO ��ʱ��_������ҳ��ҽ_����;

ALTER TABLE ��ʱ��_������ҳҽ�� RENAME TO ��ʱ��_������ҳ��ҽ_ҽ������;

update ��������_��Ŀ��Ϣ set �洢������='PR_������ҳ��ҽ_����' where ��Ŀ����='1';
update ��������_��Ŀ��Ϣ set �洢������='PR_������ҳ��ҽ_ҽ������' where ��Ŀ����='2';
