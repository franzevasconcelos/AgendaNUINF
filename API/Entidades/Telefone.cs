namespace AgendaNUINF.API.Entidades {
    public class Telefone : EntidadeBase {
        public virtual string DDD { get; set; }
        public virtual string Numero { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}