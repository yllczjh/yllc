select t.��������,
       t.סԺ������,
       t.�������ͱ���,
       t.������������,
       t.��������,
       --TO_NUMBER(REGEXP_REPLACE(T.����, '[^-0-9.]', '')) ����,
	   t.����,
       decode(t.�Ա�, '1', '��', '2', 'Ů') �Ա�,
       t.��������,
       t.���֤��,
       t.����״��,
       t.��ϵ�绰,
       t.��ͥ��ַ,
       t.������λ,
       t.��Ժʱ��,
       t.��Ժʱ��,
       t.���˿��ұ���,
       t.סԺҽ������,
       t.���λ�ʿ����,
       t.�ܴ�ҽ������,
       t.��Ժ��ʽ����,
       t.��Ժ�������,
       t.��������,
       t.��������,
       t.��Ժ��ϱ���,
       t.��Ժ�������,
       t1.*

  from סԺ����_��Ժ������Ϣ t,
       (SELECT T.סԺ������,
               SUM(T.������) AS �ܽ��,
               
               NVL(SUM(CASE T.�������
                         WHEN '1' THEN
                          T.������
                       END),
                   0) AS ��ҩ��,
               NVL(SUM(CASE T.�������
                         WHEN '2' THEN
                          T.������
                       END),
                   0) AS ��ҩ��,
               NVL(SUM(CASE T.�������
                         WHEN '3' THEN
                          T.������
                       END),
                   0) AS ��ҩ��,
               NVL(SUM(CASE T.�������
                         WHEN '4' THEN
                          T.������
                       END),
                   0) AS ע���,
               NVL(SUM(CASE T.�������
                         WHEN '5' THEN
                          T.������
                       END),
                   0) AS ������,
               NVL(SUM(CASE T.�������
                         WHEN '6' THEN
                          T.������
                       END),
                   0) AS �����,
               NVL(SUM(CASE T.�������
                         WHEN '7' THEN
                          T.������
                       END),
                   0) AS �����,
               NVL(SUM(CASE T.�������
                         WHEN '8' THEN
                          T.������
                       END),
                   0) AS B����,
               NVL(SUM(CASE T.�������
                         WHEN '9' THEN
                          T.������
                       END),
                   0) AS �����,
               NVL(SUM(CASE T.�������
                         WHEN '10' THEN
                          T.������
                       END),
                   0) AS ����,
               NVL(SUM(CASE T.�������
                         WHEN '16' THEN
                          T.������
                       
                       END),
                   0) AS �����,
               NVL(SUM(CASE T.�������
                         WHEN '18' THEN
                          T.������
                       END),
                   0) AS ���Ϸ�,
               NVL(SUM(CASE T.�������
                         WHEN '19' THEN
                          T.������
                       END),
                   0) AS ���Ʒ�,
               NVL(SUM(CASE T.�������
                         WHEN '20' THEN
                          T.������
                       END),
                   0) AS ����,
               NVL(SUM(CASE T.�������
                         WHEN '28' THEN
                          T.������
                       END),
                   0) AS ��ҩ��,
               NVL(SUM(CASE T.�������
                         WHEN '33' THEN
                          T.������
                       END),
                   0) AS ��λ��,
               NVL(SUM(CASE T.�������
                         WHEN '40' THEN
                          T.������
                       END),
                   0) AS �����,
               NVL(SUM(CASE T.�������
                         WHEN '42' THEN
                          T.������
                       END),
                   0) AS �໤��,
               NVL(SUM(CASE T.�������
                         WHEN '48' THEN
                          T.������
                       END),
                   0) AS �ĵ�ͼ,
               NVL(SUM(CASE T.�������
                         WHEN '49' THEN
                          T.������
                       END),
                   0) AS ��������,
               NVL(SUM(CASE T.�������
                         WHEN '50' THEN
                          T.������
                       END),
                   0) AS ������,
               NVL(SUM(CASE T.�������
                         WHEN '55' THEN
                          T.������
                       END),
                   0) AS һ�����Ʒ�,
               NVL(SUM(CASE T.�������
                         WHEN '56' THEN
                          T.������
                       END),
                   0) AS ���ͼ����,
               NVL(SUM(CASE T.�������
                         WHEN '57' THEN
                          T.������
                       END),
                   0) AS ��ҩ����,
               NVL(SUM(CASE T.�������
                         WHEN '1' THEN
                          0
                         WHEN '2' THEN
                          0
                         WHEN '3' THEN
                          0
                         WHEN '4' THEN
                          0
                         WHEN '5' THEN
                          0
                         WHEN '6' THEN
                          0
                         WHEN '7' THEN
                          0
                         WHEN '8' THEN
                          0
                         WHEN '9' THEN
                          0
                         WHEN '10' THEN
                          0
                         WHEN '16' THEN
                          0
                         WHEN '18' THEN
                          0
                         WHEN '19' THEN
                          0
                         WHEN '20' THEN
                          0
                         WHEN '28' THEN
                          0
                         WHEN '33' THEN
                          0
                         WHEN '40' THEN
                          0
                         WHEN '42' THEN
                          0
                         WHEN '48' THEN
                          0
                         WHEN '49' THEN
                          0
                         WHEN '50' THEN
                          0
                         WHEN '55' THEN
                          0
                         WHEN '56' THEN
                          0
                         WHEN '57' THEN
                          0
                         else
                          T.������
                       END),
                   0) AS ������
        
          FROM (select a.סԺ������,
                       c.����,
                       a.�������,
                       sum(a.�ܽ��) as ������
                  from סԺ����_��Ժ���˴��� a, ������Ŀ_�ֵ���ϸ c
                 where a.������� = c.����
                   and c.������� = 'GB_009001'
                   and a.סԺ������ in (select סԺ������
                                     from סԺ����_��Ժ���˷�Ʊ�Ǽ�
                                    where �������� = a.��������)
                 group by a.�������, c.����, a.סԺ������
                 order by a.סԺ������, c.����) T,
               סԺ����_��Ժ������Ϣ TT
         WHERE T.סԺ������ = TT.סԺ������
         group by t.סԺ������) t1
 where t.סԺ������ = t1.סԺ������
   --and t.סԺ������ = '20190000021';
