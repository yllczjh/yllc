CREATE OR REPLACE PROCEDURE Pr_�豸����_�豸����(str_��������         in varchar2,
                                         dat_�������         in date,
                                         str_�ɹ�����         in varchar2,
                                         str_������           in varchar2,
                                         str_�ɹ�Ա           in varchar2,
                                         str_����Ա           in varchar2,
                                         str_�ջ�Ա           in varchar2,
                                         str_���ڸ���         in varchar2,
                                         dat_��������         in date,
                                         str_��ע             in varchar2,
                                         str_Դ���ݺ�         in varchar2,
                                         str_�������         in varchar2,
                                         str_�豸����         in varchar2,
                                         str_�豸���ͱ���     in varchar2,
                                         str_��������         in varchar2,
                                         str_Ʒ���ͺ�         in varchar2,
                                         str_���             in varchar2,
                                         str_����             in varchar2,
                                         str_����             in varchar2,
                                         str_����             in varchar2,
                                         str_�豸���������ʶ in varchar2, --1.�豸  2.����  3.���
                                         str_�����ʲ��ű�־   in varchar2,
                                         str_���ڱ�־         in varchar2,
                                         str_����ʱ�¾����   in varchar2,
                                         str_�����������     in varchar2,
                                         str_�������Ƕ�λϵͳ in varchar2,
                                         dat_¼��ʱ��         in date,

                                         str_��ⵥ�� out varchar2,
                                         i_����ֵ     out integer,
                                         str_������Ϣ out varchar2) AS

  --str_��ⵥ�� varchar2(50);
  --str_�ʲ����� varchar2(50);
 -- i_����       integer;

  arr_�豸����         PP_ȫ�ֱ���.array_����;
  arr_�豸���ͱ���     PP_ȫ�ֱ���.array_����;
  arr_��������         PP_ȫ�ֱ���.array_����;
  arr_Ʒ���ͺ�         PP_ȫ�ֱ���.array_����;
  arr_���             PP_ȫ�ֱ���.array_����;
  arr_����             PP_ȫ�ֱ���.array_����;
  arr_����             PP_ȫ�ֱ���.array_����;
  arr_����             PP_ȫ�ֱ���.array_����;
  arr_�豸���������ʶ PP_ȫ�ֱ���.array_����;
  arr_�����ʲ��ű�־   PP_ȫ�ֱ���.array_����;
  arr_���ڱ�־         PP_ȫ�ֱ���.array_����;
  arr_����ʱ�¾����   PP_ȫ�ֱ���.array_����;
  arr_�����������     PP_ȫ�ֱ���.array_����;
  arr_�������Ƕ�λϵͳ PP_ȫ�ֱ���.array_����;

BEGIN

  BEGIN
    Pr_�豸����_��ⵥ��(str_�������� => str_��������,
                 str_��ⵥ�� => str_��ⵥ��,
                 i_����ֵ     => i_����ֵ,
                 str_������Ϣ => str_������Ϣ);

    arr_�豸����         := FU_Strsplit(str_�����ַ��� => str_�豸����,
                                    str_�ָ���     => ',');
    arr_�豸���ͱ���     := FU_Strsplit(str_�����ַ��� => str_�豸���ͱ���,
                                  str_�ָ���     => ',');
    arr_��������         := FU_Strsplit(str_�����ַ��� => str_��������,
                                    str_�ָ���     => ',');
    arr_Ʒ���ͺ�         := FU_Strsplit(str_�����ַ��� => str_Ʒ���ͺ�,
                                    str_�ָ���     => ',');
    arr_���             := FU_Strsplit(str_�����ַ��� => str_���,
                                      str_�ָ���     => ',');
    arr_����             := FU_Strsplit(str_�����ַ��� => str_����,
                                      str_�ָ���     => ',');
    arr_����             := FU_Strsplit(str_�����ַ��� => str_����,
                                      str_�ָ���     => ',');
    arr_����             := FU_Strsplit(str_�����ַ��� => str_����,
                                      str_�ָ���     => ',');
    arr_�豸���������ʶ := FU_Strsplit(str_�����ַ��� => str_�豸���������ʶ,
                                str_�ָ���     => ',');
    arr_�����ʲ��ű�־   := FU_Strsplit(str_�����ַ��� => str_�����ʲ��ű�־,
                                 str_�ָ���     => ',');
    arr_���ڱ�־         := FU_Strsplit(str_�����ַ��� => str_���ڱ�־,
                                    str_�ָ���     => ',');
    arr_����ʱ�¾����   := FU_Strsplit(str_�����ַ��� => str_����ʱ�¾����,
                                 str_�ָ���     => ',');
    arr_�����������     := FU_Strsplit(str_�����ַ��� => str_�����������,
                                  str_�ָ���     => ',');
    arr_�������Ƕ�λϵͳ := FU_Strsplit(str_�����ַ��� => str_�������Ƕ�λϵͳ,
                                str_�ָ���     => ',');

    str_������Ϣ := '�����豸��ⵥʧ��! ';
    insert into �豸����_��ⵥ
      (��������,
       ��ⵥ��,
       �������,
       �ɹ�����,
       ������,
       �ɹ�Ա,
       ����Ա,
       �ջ�Ա,
       ���ڸ���,
       ��������,
       ��ע,
       Դ���ݺ�,
       �������,
       ¼��ʱ��)
    values
      (str_��������,
       str_��ⵥ��,
       dat_�������,
       str_�ɹ�����,
       str_������,
       str_�ɹ�Ա,
       str_����Ա,
       str_�ջ�Ա,
       str_���ڸ���,
       dat_��������,
       str_��ע,
       str_Դ���ݺ�,
       str_�������,
       dat_¼��ʱ��);

    --�����ݲ��뵽��ⵥ��ϸ����
    for i in 1 .. arr_�豸����.count loop

      str_������Ϣ := '��ⵥ��ϸ����ʧ��! ';
      insert into �豸����_��ⵥ��ϸ
        (��������,
         ��ⵥ��,
         �豸����,
         ��������,
         Ʒ���ͺ�,
         ���,
         ����,
         ����,
         ����,
         �豸���������־,
         �����ʲ��ű�־,
         ���ڱ�־,
         ����ʱ�¾����,
         �����������,
         �������Ƕ�λϵͳ)
      values
        (str_��������,
         str_��ⵥ��,
         arr_�豸����(i),
         arr_��������(i),
         arr_Ʒ���ͺ�(i),
         arr_���(i),
         arr_����(i),
         arr_����(i),
         arr_����(i),
         arr_�豸���������ʶ(i),
         arr_�����ʲ��ű�־(i),
         arr_���ڱ�־(i),
         arr_����ʱ�¾����(i),
         arr_�����������(i),
         arr_�������Ƕ�λϵͳ(i));

     /* --���ӿ��
      str_������Ϣ := '����ѯʧ��! ';
      select count(*)
        into i_����
        from �豸����_����
       where �������� = str_��������
         and �豸���� = arr_�豸����(i);

      if i_���� > 0 then

        str_������Ϣ := '���¿��ʧ��! ';
        update �豸����_����
           set ����  =
               (to_number(����) + to_number(arr_����(i))),
               ����   = arr_����(i),
               ��׼�� =
               (to_number(����) * to_number(��׼��) +
               to_number(arr_����(i)) * to_number(arr_����(i))) /
               (to_number(����) + to_number(arr_����(i)))
         where �������� = str_��������
           and �豸���� = arr_�豸����(i);

      else

        str_������Ϣ := '�������ʧ��! ';
        insert into �豸����_����
          (��������, �豸����, ����, ����, ����, ��׼��, ��������)
        values
          (str_��������,
           arr_�豸����(i),
           arr_����(i),
           arr_����(i),
           arr_����(i),
           arr_����(i),
           0);

      end if;

      --�豸���������ʶ���� 1 ʱ,Ϊ�豸,

      if arr_�豸���������ʶ(i) = '1' then

        --������豸,�������Ϊ 1,��ֱ�Ӳ������豸��,������� 1,���������ѭ������
        if arr_����(i) = 1 then

          Pr_�豸����_�ʲ�����(str_��������     => str_��������,
                       str_�豸���ͱ��� => arr_�豸���ͱ���(i),
                       str_�ʲ�����     => str_�ʲ�����,
                       i_����ֵ         => i_����ֵ,
                       str_������Ϣ     => str_������Ϣ);

          str_������Ϣ := '�������豸ʧ��! ';
          insert into �豸����_���豸��
            (��������,
             �ʲ�����,
             �豸����,
             ��������,
             ���ڱ�־,
             ���򵥼�,
             ��������,
             ����,
             ��ⵥ��,
             ���ñ�־,
             ����ֵ,
             �۾����,
             �������,
             ����ʱ�¾����,
             �����������,
             �������Ƕ�λϵͳ)
          values
            (str_��������,
             str_�ʲ�����,
             arr_�豸����(i),
             arr_��������(i),
             arr_���ڱ�־(i),
             arr_����(i),
             dat_�������,
             arr_����(i),
             str_��ⵥ��,
             '��',
             arr_����(i),
             (select �۾����
                from �豸����_�豸Ŀ¼
               where �豸���� = arr_�豸����(i)
                 and �������� = str_��������),
             (select �������
                from �豸����_�豸Ŀ¼
               where �豸���� = arr_�豸����(i)
                 and �������� = str_��������),
             arr_����ʱ�¾����(i),
             arr_�����������(i),
             arr_�������Ƕ�λϵͳ(i));

        else

          for j in 1 .. to_number(arr_����(i)) loop

            PR_�豸����_�ʲ�����(str_��������     => str_��������,
                         str_�豸���ͱ��� => arr_�豸���ͱ���(i),
                         str_�ʲ�����     => str_�ʲ�����,
                         i_����ֵ         => i_����ֵ,
                         str_������Ϣ     => str_������Ϣ);

            str_������Ϣ := '�������豸ʧ��! ';
            insert into �豸����_���豸��
              (��������,
               �ʲ�����,
               �豸����,
               ��������,
               ���ڱ�־,
               ���򵥼�,
               ��������,
               ����,
               ��ⵥ��,
               ���ñ�־,
               ����ֵ,
               �۾����,
               �������,
               ����ʱ�¾����,
               �����������,
               �������Ƕ�λϵͳ)
            values
              (str_��������,
               str_�ʲ�����,
               arr_�豸����(i),
               arr_��������(i),
               arr_���ڱ�־(i),
               arr_����(i),
               dat_�������,
               arr_����(i),
               str_��ⵥ��,
               '��',
               arr_����(i),
               (select �۾����
                  from �豸����_�豸Ŀ¼
                 where �豸���� = arr_�豸����(i)
                   and �������� = str_��������),
               (select �������
                  from �豸����_�豸Ŀ¼
                 where �豸���� = arr_�豸����(i)
                   and �������� = str_��������),
               arr_����ʱ�¾����(i),
               arr_�����������(i),
               arr_�������Ƕ�λϵͳ(i));

          end loop;

        end if;

      end if;*/

    end loop;

  EXCEPTION
    WHEN OTHERS THEN
      GOTO �˳�;
  END;

  i_����ֵ     := 1;
  str_������Ϣ := '�������豸�ɹ�! ';
  RETURN;

  <<�˳�>>
  i_����ֵ     := 0;
  str_������Ϣ := str_������Ϣ || SQLERRM;
  RETURN;

END;

 
 
 
/
