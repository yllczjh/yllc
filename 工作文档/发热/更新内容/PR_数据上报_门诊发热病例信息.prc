create or replace procedure PR_�����ϱ�_���﷢�Ȳ�����Ϣ(STR_����          IN VARCHAR2,
                                             CUR_����_�б���Ϣ OUT SYS_REFCURSOR) IS

  STR_�������� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 1);
  /*DAT_��Ժʱ����ʼ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 2),
                             'yyyy-MM-dd hh24:mi:ss');
  DAT_��Ժʱ���ֹ DATE := TO_DATE(FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 3),
                             'yyyy-MM-dd hh24:mi:ss');
  STR_��������     VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 4);*/
  STR_��Ŀ���� VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 5);
  STR_��ˮ��   VARCHAR2(50) := FU_ͨ��_��ȡ�ַ���ֵ(STR_����, '|', 6);
BEGIN
  BEGIN
    OPEN CUR_����_�б���Ϣ FOR
      select G.�������� as P900,
             '��������' as P6891,
             null as P686, --ҽ�Ʊ����ֲ��,
             X.�������� as p800,
             '01' as P7501, -- ��������,
             G.���ﲡ���� as P7502, -- ���￨��,
             G.���ﲡ���� as P7000, -- ���������ˮ��,
             X.���� as p4,
             x.�Ա� as p5,
             X.�������� as p6,
             X.���� as p7,
             null as P12, --����,
             X.����ID as p11,
             X.����״�� as p8,
             B.ְҵ as p9,
             null as p7503, -- ע��֤�����ʹ���,
             X.���֤�� as p13,
             B.��ס_��ַ as p801,
             X.�ֻ����� as p802,
             B.��ס_�ʱ� as p803,
             B.������λ����ַ as P14,
             B.��λ�绰 as P15,
             null as P16, --������λ��������
             B.��ϵ��_���� as P18,
             B.��ϵ��_��ϵ as P19,
             B.��ϵ��_��ַ as P20,
             B.��ϵ��_�绰 as p21,
             null as p7505, --�������
             decode(G.�Ƿ���, '����', '��', '��') as P7520, --�Ƿ����
             null as p7521, --�Ƿ�ת��
             G.������ұ��� as P7504,
             (select a.��������
                from ������Ŀ_�������� A
               where a.�������� = G.��������
                 and a.���ұ��� = G.������ұ���) as P7522, --������Ҵ���
             G.�Һ�ʱ�� as P7506, --��������
             null as P7507, --����
             null as p7523, --�ֲ�ʷ
             null as p7524, --�����
             
             null       as p7525, --֢״����
             null       as p7526, --֢״����
             null       as p7527, --֢״����
             null       as p7528, --��������
             null       as p7529, --�Ƿ�����
             G.�������� as p28,
             g.�������� as p281
      
        from �������_�ҺŵǼ�     G,
             ������Ŀ_������Ϣ     X,
             ������Ŀ_���˲�����Ϣ B
       where G.�������� = X.��������
         and x.�������� = b.��������
         and G.����ID = X.����ID
         and x.����ID = b.����ID;
  
  end;

end PR_�����ϱ�_���﷢�Ȳ�����Ϣ;
/
