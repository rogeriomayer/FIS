using System.Data.Objects;

namespace FMC.Automator.DistribuicaoDesk.DAO
{
     public abstract class AbstractBLLContexto<TObjectContext>
        where TObjectContext : ObjectContext, new()
    {
        private bool controlaTransacao;
        private TObjectContext context;

        protected TObjectContext Context
        {
            get
            {
                return context;
            }
        }

        protected bool ControlaTransacao
        {
            get
            {
                return controlaTransacao;
            }
        }

        public AbstractBLLContexto()
        {

            this.context = new TObjectContext();
            this.context.CommandTimeout = 360;
            controlaTransacao = true;
        }

        public AbstractBLLContexto(TObjectContext context)
        {
            this.context = context;
            controlaTransacao = false;
        }

        protected void ReciclarContext()
        {
            if (controlaTransacao)
                this.context = new TObjectContext();
        }
    }
}
