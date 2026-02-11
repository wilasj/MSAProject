namespace MSAProject.Application.Interfaces;

//Criei essa interface porque se não o middleware que faz o unit of work dependeria
//100% do NHibernate, e essa não é a ideia
public interface IUnitOfWork
{
    ITransacao ComecaTransacao();
}