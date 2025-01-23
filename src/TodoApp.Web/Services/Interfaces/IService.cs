namespace TodoApp.Web.Services.Interfaces;

public interface IService
{
    abstract Task<TModel?> GetByIdAsync<TModel>(Guid id);
    abstract Task<List<TModel>?> GetAll<TModel>();
    abstract Task<HttpResponseMessage> Post<TModel>(TModel model);
    abstract Task<HttpResponseMessage> Put<TModel>(Guid id, TModel model);
    abstract Task<HttpResponseMessage> Delete(Guid id);
}