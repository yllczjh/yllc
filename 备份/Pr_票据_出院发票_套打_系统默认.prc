CREATE OR REPLACE Procedure Pr_Ʊ��_��Ժ��Ʊ_�״�_ϵͳĬ��(Str_����_�������� in varchar2,
                                               Str_����_Ʊ�ݺ�   in varchar2,
                                               cur_Ʊ����ϸ      out sys_refcursor,
                                               I_����ֵ          out integer,
                                               Str_������Ϣ      out varchar2) AS
  str_�Ƿ�ĸӤһ�廯 varchar2(50);
  str_סԺ������     varchar2(50);
  str_��������       varchar2(50);
  str_��������       varchar2(50);
  num_Ӧ���ܶ�       number(18, 3);
  num_�����ܶ�       number(18, 3);
  num_ʵ���ܶ�       number(18, 3);
  num_�ܲ����ܶ�     number(18, 3);
  num_Ԥ����         number(18, 3);
  num_�����ܶ�       number(18, 3);
  num_�˿��ܶ�       number(18, 3);
  num_�����ܶ�       number(18, 3);
  str_������������   varchar2(50);
  dat_����ʱ��       date;
  str_����Ա����     varchar2(50);
  dat_��Ժʱ��       date;
  dat_��Ժʱ��       date;
  str_��������       varchar2(200);
  int_�Ƿ��Ժ       integer;
  str_���˱��       varchar2(50);
  num_ͳ�ﲹ��       varchar2(200);
  num_��ͥ�˻�����   varchar2(200);
  num_��������       varchar2(200);
  num_���̱�       varchar2(200);
  num_��������       varchar2(200);
  num_���ײ���       varchar2(200);
  num_ҽ�Ʒ���       varchar2(200);
  num_ͳ��֧�����   number(18, 3); --ҽ��:���λ���ҽ�Ʊ������,ũ��:
  num_�����˻�֧��   number(18, 3); --ҽ��:�����˻�֧������,ũ��:��ͥ�˻�֧��
  --ҽ��:���δ�ҽ�Ʊ������,ũ��:������������+���̱�����+������������+���ײ�������+��׼Ŀ¼����+�¾�׼�Ÿ�����+�¾�׼��������+�¾�׼��ƶ����+�¾�׼���ز���
  num_�����������   number(18, 3);
  str_ҽ��֤��       varchar2(100);
  str_�α���������   varchar2(50);
  str_��Ⱥ���       varchar2(50);
  str_ҽ��ͳ��ǼǺ� varchar2(50);

  str_�Ա�            varchar2(10);
  str_��λ����        varchar2(10);
  str_����_סԺ������ varchar2(20);
  int_סԺ����        integer;

BEGIN
  --�ش�ӡ��־,ʵ���ܶ�,��д���Ϊ�����ֶ�

  select ��������
    into str_��������
    from ������Ŀ_��������
   where �������� = Str_����_��������
     and ɾ����־ = '0';

  select סԺ������,
         ��������,
         Ӧ�ս��,
         ������,
         ʵ�ս��,
         �ܲ������,
         Ԥ����,
         ���ս��,
         �˿���,
         �������,
         ������������,
         ����ʱ��,
         ����Ա����,
         ��Ժʱ��,
         ��Ժʱ��
    into str_סԺ������,
         str_��������,
         num_Ӧ���ܶ�,
         num_�����ܶ�,
         num_ʵ���ܶ�,
         num_�ܲ����ܶ�,
         num_Ԥ����,
         num_�����ܶ�,
         num_�˿��ܶ�,
         num_�����ܶ�,
         str_������������,
         dat_����ʱ��,
         str_����Ա����,
         dat_��Ժʱ��,
         dat_��Ժʱ��
    from סԺ����_��Ժ���˷�Ʊ�Ǽ�
   where �������� = Str_����_��������
     and ��Ʊ�� = Str_����_Ʊ�ݺ�;

  select count(סԺ������)
    into int_�Ƿ��Ժ
    from סԺ����_��Ժ������Ϣ
   where �������� = Str_����_��������
     and סԺ������ = str_סԺ������;

  if int_�Ƿ��Ժ = 0 then
    select ��������,
           (case �Ա�
             when '1' then
              '��'
             when '2' then
              'Ů'
             else
              'δ֪'
           end),
           (select ��λ����
              from סԺ����_���Ҵ�λ
             where �������� = a.��������
               and ��λ���� = a.��λ
               and rownum = 1),
           סԺ����
      into str_��������, str_�Ա�, str_��λ����, int_סԺ����
      from סԺ����_��Ժ������Ϣ a
     where �������� = Str_����_��������
       and סԺ������ = str_סԺ������;
  else
    select ��������,
           (case �Ա�
             when '1' then
              '��'
             when '2' then
              'Ů'
             else
              'δ֪'
           end),
           (select ��λ����
              from סԺ����_���Ҵ�λ
             where �������� = a.��������
               and ��λ���� = a.��λ
               and rownum = 1)
      into str_��������, str_�Ա�, str_��λ����
      from סԺ����_��Ժ������Ϣ a
     where �������� = Str_����_��������
       and סԺ������ = str_סԺ������;
  end if;

  BEGIN
    select ֵ
      into str_�Ƿ�ĸӤһ�廯
      from ������Ŀ_���������б�
     where �������� = '274'
       and �������� = Str_����_��������;
  EXCEPTION
    WHEN OTHERS THEN
      str_�Ƿ�ĸӤһ�廯 := '��';
  END;

  --�ӿڲ������

  if str_������������ = 'ũ��' then
    BEGIN
      select nvl(C_10, '0') as ͳ�ﲹ��,
             nvl(C_11, '0') as ��ͥ�˻�,
             nvl(C_13, '0') as ��������,
             nvl(C_14, '0') as ���̱�,
             nvl(C_15, '0') as ��������,
             nvl(C_16, '0') as ���ײ���,
             nvl(C_25, '0') as ҽ�Ʒ���,
             C_3 as ҽ��֤��
        into num_ͳ�ﲹ��,
             num_��ͥ�˻�����,
             num_��������,
             num_���̱�,
             num_��������,
             num_���ײ���,
             num_ҽ�Ʒ���,
             str_ҽ��֤��
        from �ӿڹ���_�ӿڲ�����Ϣ
       where �������� = Str_����_��������
         and �ӿ����� = 'ũ��'
         and �������� = 'סԺ'
         and ������ = str_סԺ������;
    EXCEPTION
      WHEN OTHERS THEN
        num_ͳ�ﲹ��     := '0';
        num_��ͥ�˻����� := '0';
        num_��������     := '0';
        num_���̱�     := '0';
        num_��������     := '0';
        num_���ײ���     := '0';
        num_ҽ�Ʒ���     := '0';
    END;
  
    num_ͳ��֧����� := to_number(num_ͳ�ﲹ��);
    num_�����˻�֧�� := to_number(num_��ͥ�˻�����);
    num_����������� := to_number(num_��������) + to_number(num_���̱�) +
                  to_number(num_��������) + to_number(num_���ײ���);
  end if;

  if str_������������ = 'ҽ��' then
    BEGIN
      ---��ҽ����ͨ ����������������ԱC_26,��֧����C_27,����C_25(�����ҵ�ձ���C_23,���ʹ����ҵ��ͬһ�ֶ�)���󲡾���C_29
      select to_number(nvl(C_26, '0')) + to_number(nvl(C_27, '0')) +
             to_number(nvl(C_25, '0')) + to_number(nvl(C_29, '0')) as �����������,
             to_number(nvl(C_24, '0')) as ͳ�ﲹ��,
             nvl(C_16, '0') as �����˻�֧��,
             C_5 AS ���֤��,
             C_3 as �α���������,
             C_9 as ��Ⱥ���,
             C_30 as ҽ��ͳ��ǼǺ�
        into num_�����������,
             num_ͳ��֧�����,
             num_�����˻�֧��,
             str_ҽ��֤��,
             str_�α���������,
             str_��Ⱥ���,
             str_ҽ��ͳ��ǼǺ�
        from �ӿڹ���_�ӿڲ�����Ϣ
       where �������� = Str_����_��������
         and �ӿ����� = 'ҽ��'
         and �������� = 'סԺ'
         and ������ = str_סԺ������;
    EXCEPTION
      WHEN OTHERS THEN
        num_ͳ��֧����� := 0;
        num_�����˻�֧�� := 0;
        num_����������� := 0;
        num_ҽ�Ʒ���     := 0;
    END;
  
  end if;

  select fu_ת��_סԺ������(Str_����_��������,
                     str_סԺ������,
                     '��',
                     str_�Ƿ�ĸӤһ�廯)
    into str_����_סԺ������
    from dual;

  OPEN cur_Ʊ����ϸ FOR
    select Str_����_Ʊ�ݺ� as Ʊ�ݺ�,
           str_�������� as ��������,
           str_���˱�� as ���˱��,
           str_סԺ������ as סԺ������,
           str_�������� as ��������,
           str_�Ա� as �Ա�,
           str_�������� as ��������,
           '' as ����,
           str_��λ���� as ��λ����,
           num_ͳ�ﲹ�� as ͳ�ﲹ��,
           num_��ͥ�˻����� as ��ͥ�˻�����,
           num_�������� as ��������,
           num_���̱� as ���̱�,
           num_�������� as ��������,
           num_���ײ��� as ���ײ���,
           num_ͳ��֧����� as ҽ��ͳ��,
           num_�����˻�֧�� as �����˻�֧��,
           num_����������� as �����������,
           num_ҽ�Ʒ��� as ҽ�Ʒ���,
           str_ҽ��֤�� as ҽ��֤��,
           str_�α��������� as �α���������,
           case str_�α���������
             when '21000000' then
              '����ʡ��ؽ���ƽ̨'
             when '21080103' then
              'Ӫ����ҽ�Ʊ�����ҵ��'
             when '21080403' then
              'Ӫ���о��ÿ�����ҽ�Ʊ��չ�������'
             when '21081103' then
              'Ӫ�����ϱ�ҽ�Ʊ��չ�������'
             when '21088103' then
              'Ӫ���и���ҽ�Ʊ��չ�������'
             when '21088203' then
              'Ӫ���д�ʯ��ҽ�Ʊ��չ�������'
             else
              str_�α���������
           end as �α�����,
           str_��Ⱥ��� as ��Ⱥ������,
           case str_��Ⱥ���
             when 'A' then
              '����ְ��'
             when 'B' then
              '�������'
           end as ��Ⱥ���,
           str_ҽ��ͳ��ǼǺ� as ҽ��ͳ��ǼǺ�,
           num_Ӧ���ܶ� as Ӧ���ܶ�,
           num_�����ܶ� as �����ܶ�,
           num_ʵ���ܶ� as ʵ���ܶ�,
           num_�ܲ����ܶ� as �ܲ����ܶ�,
           num_Ԥ���� as Ԥ����,
           num_�����ܶ� as �����ܶ�,
           num_�˿��ܶ� as �˿��ܶ�,
           num_ʵ���ܶ� - num_�ܲ����ܶ� as �Ը��ܶ�,
           num_�����ܶ� as �����ܶ�,
           str_������������ as ������������,
           dat_����ʱ�� as ����ʱ��,
           str_����Ա���� as ����Ա����,
           dat_��Ժʱ�� as ��Ժʱ��,
           dat_��Ժʱ�� as ��Ժʱ��,
           nvl(int_סԺ����, trunc(dat_��Ժʱ��) - trunc(dat_��Ժʱ��)) as סԺ����,
           FU_���_ת���ɴ�д(num_ʵ���ܶ�) as ��д���,
           FU_���_ת���ɴ�д(num_Ԥ����) as Ԥ�����д,
           FU_���_ת���ɴ�д(num_ʵ���ܶ� - num_�ܲ����ܶ�) as �Ը��ܶ��д,
           'ҽ�Ʒ���' as ��ҵ����,
           'סԺ' as ����,
           '' as �ش�ӡ��־,
           str_������������ || '����' as ������ʽ,
           /*nvl(sum(case ��������
                     when '33' then
                      �ܽ��
                   end),
               0) as ��λ��,
           nvl(sum(case ��������
                     when '20' then
                      �ܽ��
                   end),
               0) as ����,
           nvl(sum(case ��������
                     when '10' then
                      �ܽ��
                   end),
               0) as ����,
           nvl(sum(case ��������
                     when '19' then
                      �ܽ��
                   end),
               0) as ���Ʒ�,
           nvl(sum(case ��������
                     when '16' then
                      �ܽ��
                   end),
               0) as �����,
           nvl(sum(case ��������
                     when '5' then
                      �ܽ��
                   end),
               0) as ������,
           nvl(sum(case ��������
                     when '7' then
                      �ܽ��
                   end),
               0) as �����,
           nvl(sum(case ��������
                     when '1' then
                      �ܽ��
                   end),
               0) as ��ҩ��,
           nvl(sum(case ��������
                     when '2' then
                      �ܽ��
                   end),
               0) as �г�ҩ,
           nvl(sum(case ��������
                     when '3' then
                      �ܽ��
                   end),
               0) as �в�ҩ,
           nvl(sum(case ��������
                     when '18' then
                      �ܽ��
                   end),
               0) as ���ķ�,
           nvl(sum(case ��������
                     when '9' then
                      �ܽ��
                   end),
               0) as �����,
           nvl(sum(case ��������
                     when '30' then
                      �ܽ��
                   end),
               0) as ��Ѫ��,
           
           nvl(sum(case ��������
                     when '33' then
                      0
                     when '20' then
                      0
                     when '10' then
                      0
                     when '19' then
                      0
                     when '16' then
                      0
                     when '5' then
                      0
                     when '7' then
                      0
                     when '1' then
                      0
                     when '2' then
                      0
                     when '3' then
                      0
                     when '18' then
                      0
                     when '9' then
                      0
                     else
                      �ܽ��
                   end),
               0) as ������*/
           ��������,
           sum(�ܽ��) as ���ý��
      from (select nvl((select b.��������
                         from ������Ŀ_���ù��� b
                        where b.�������� = a.��������
                          and b.���ñ��� = a.�������
                          and b.ɾ����־ = '0'
                          and b.��� = 'סԺ��Ʊ��Ŀ����'),
                       a.�������) as ��������,
                   nvl((select x.����
                         from ������Ŀ_���ù��� g, ������Ŀ_�ֵ���ϸ x
                        where g.�������� = a.��������
                          and g.ɾ����־ = '0'
                          and g.��� = 'סԺ��Ʊ��Ŀ����'
                          and x.������� = 'GB_009001'
                          and g.���ñ��� = a.�������                             
                          and g.�������� = x.����),
                       '') as ��������,
                   (case
                     when num_Ӧ���ܶ� < 0 then
                      -a.�ܽ��
                     else
                      a.�ܽ��
                   end) as �ܽ��
              from סԺ����_��Ժ���˴��� a, ��ʱ��_������ c
             where a.����ʱ�� >= dat_��Ժʱ��
               and a.����ʱ�� <= dat_����ʱ��
               and a.�������� = Str_����_��������
               and a.סԺ������ = c.������
            
            union all
            select nvl((select b.��������
                         from ������Ŀ_���ù��� b
                        where b.�������� = a.��������
                          and b.���ñ��� = a.�������
                          and b.ɾ����־ = '0'
                          and b.��� = 'סԺ��Ʊ��Ŀ����'),
                       a.�������) as ��������,
                   nvl((select x.����
                         from ������Ŀ_���ù��� g, ������Ŀ_�ֵ���ϸ x
                        where g.�������� = a.��������
                          and g.ɾ����־ = '0'
                          and g.��� = 'סԺ��Ʊ��Ŀ����'
                          and x.������� = 'GB_009001'
                          and g.���ñ��� = a.�������                         
                          and g.�������� = x.����),
                       '') as ��������,
                   (case
                     when num_Ӧ���ܶ� < 0 then
                      -a.�ܽ��
                     else
                      a.�ܽ��
                   end) as �ܽ��
              from סԺ����_��Ժ���˴��� a, ��ʱ��_������ c
             where a.����ʱ�� >= dat_��Ժʱ��
               and a.����ʱ�� <= dat_����ʱ��
               and a.�������� = Str_����_��������
               and a.סԺ������ = c.������) g group by g.��������,g.��������;

  if sqlcode <> 0 then
    I_����ֵ     := 0;
    Str_������Ϣ := sqlerrm;
    return;
  end if;

  I_����ֵ     := 1;
  Str_������Ϣ := 'OK';
  commit;
END;
/
