select (select ��Ա���� from ������Ŀ_��Ա���� where ��Ա���� = ����ҽ������) as ҽ������,
       �Һ��˴�,
       �������˴�,
       �������˴�,
       һ������ as "1�������˴�",
       (case
         when �������˴� > 0 then
          ROUND(һ������ / �������˴�, 2) * 100 || '%'
         else
          null
       end) as "1�������˴�ռ��",
       �������� as "2�������˴�",
       (case
         when �������˴� > 0 then
          ROUND(�������� / �������˴�, 2) * 100 || '%'
         else
          null
       end) as "2�������˴�ռ��",
       �������� as "3�������˴�",
       (case
         when �������˴� > 0 then
          ROUND(�������� / �������˴�, 2) * 100 || '%'
         else
          null
       end) as "3�������˴�ռ��",
       �Ĵ����� as "4�������˴�",
       (case
         when �������˴� > 0 then
          ROUND(�Ĵ����� / �������˴�, 2) * 100 || '%'
         else
          null
       end) as "4�������˴�ռ��",
       ������� as "5�������˴�",
       �������� as "6�������˴�",
       �ߴ����� as "7�������˴�",
       �˴����� as "8�������˴�",
       �Ŵ����� as "9�������˴�",
       ʮ������ as "10�������˴�����"

  from (select ����ҽ������,
               sum(case
                     when ���Ѵ��� >= 0 then
                      1
                     else
                      0
                   end) as �Һ��˴�,
               sum(case
                     when ���Ѵ��� = 0 then
                      1
                     else
                      0
                   end) as �������˴�,
               sum(case
                     when ���Ѵ��� >= 1 then
                      1
                     else
                      0
                   end) as �������˴�,
               sum(case
                     when ���Ѵ��� = 1 then
                      1
                     else
                      0
                   end) as һ������,
               sum(case
                     when ���Ѵ��� = 2 then
                      1
                     else
                      0
                   end) as ��������,
               sum(case
                     when ���Ѵ��� = 3 then
                      1
                     else
                      0
                   end) as ��������,
               sum(case
                     when ���Ѵ��� = 4 then
                      1
                     else
                      0
                   end) as �Ĵ�����,
               sum(case
                     when ���Ѵ��� = 5 then
                      1
                     else
                      0
                   end) as �������,
               sum(case
                     when ���Ѵ��� = 6 then
                      1
                     else
                      0
                   end) as ��������,
               sum(case
                     when ���Ѵ��� = 7 then
                      1
                     else
                      0
                   end) as �ߴ�����,
               sum(case
                     when ���Ѵ��� = 8 then
                      1
                     else
                      0
                   end) as �˴�����,
               sum(case
                     when ���Ѵ��� = 9 then
                      1
                     else
                      0
                   end) as �Ŵ�����,
               sum(case
                     when ���Ѵ��� >= 10 then
                      1
                     else
                      0
                   end) as ʮ������
          from (select ����ҽ������, �Һ����, count(��Ʊ���) ���Ѵ���
                  from (select g.����ҽ������, g.�Һ����, c.��Ʊ���
                          from �������_�ҺŵǼ� g
                          left join �������_���ﴦ�� c
                            on g.�������� = c.��������
                           and g.���ﲡ���� = c.���ﲡ����
                           and g.�Һ���� = c.�Һ����
                         where g.����״̬ = '��ɽ���'
                           and g.�˺ű�־ = '��'
                           and g.�Һ�ʱ�� between
                               to_date('2020-05-01', 'yyyy-MM-dd') and
                               to_date('2020-06-01', 'yyyy-MM-dd')
                         group by g.����ҽ������, g.�Һ����, c.��Ʊ���)
                 group by ����ҽ������, �Һ����) a
         group by a.����ҽ������);
