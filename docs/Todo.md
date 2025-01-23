## Todo

- Description: string
- Finished: bool
- Date Begin: date only
- Time Begin: time only
- Date End: date only
- Time End: time only
- MenuId: guid
- UserId: guid
- Item: List<Item>

## Item
- Description: string
- Finished: bool:
- Sequencia: int


### Restrições

#### Adicionar Data ou Horario
(A mesma validação da Data, vale para o Horario)
1 - Data Inicial não pode ser menor que a Data Atual;
2 - Data Final não pode ser menor que a Data Atual;
3 - Data Final não pode ser menor que da Data Inicial;
4 - Quando adicionar Horario, deve truncar o segundo, por exemplo. 10:10:40, deve ficar 10:10:00, tirando os 40 segundos.