using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bot2Mod3.Formulario
{
    [Serializable]
    [Template(TemplateUsage.NotUnderstood, "Não entendi, selecione uma das opções!!")]
    public class Pedido
    {
        public Salgados Salgados { get; set; }
        public Bebidas Bebidas { get; set; }
        public TipoEntrega TipoEntrega { get; set; }
        public CPFnaNota CPFNaNota { get; set; }

        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }


        public static IForm<Pedido> BuildForm()
        {
            var form = new FormBuilder<Pedido>();
            form.Configuration.DefaultPrompt.ChoiceStyle = ChoiceStyleOptions.Buttons;

            form.Configuration.Yes = new string[] { "sim", "yes", "si", "s", "y", "hai", "pode faturar", "yep" };
            form.Configuration.No = new string[] { "nao", "no", "n", "cancela", "cancelar" };

            form.Message("Bem Vindo, será um prazer atende-lo.");


            form.OnCompletion(async (context, pedido) => 
            {
                //Salvar na base de dados
                //Gerar pedido
                //Integrar com servico xpto.
                await context.PostAsync("Seu pedido número 001 foi gerado e em instantes será entregue!");
            });

            return form.Build();
        }
    }

    [Describe("Tipo de Entrega")]
    public enum TipoEntrega
    {
        [Terms("r", "Retira", "Retirar no Local")]
        [Describe("Retirar no Local")]
        Retira =  1,

        [Terms("Entregar em casa", "em casa", "entregar", "entrega", "Motoboy", "moto", "motoca")]
        [Describe("Motoboy")]
        Motoboy
    }

    public enum Salgados
    {
        Esfirra = 1,

        [Terms("Kibe", "quibe")]
        Quibe,
        Coxinha,
        Pao_de_Queijo,
        Bolinho_de_Bacalhau,
        Empada,
        Pastel
    }

    public enum Bebidas
    {
        Agua =  1,
        Caldo_de_Cana,
        Suco,

        [Terms("Refri", "Guarana", "Coca-cola", "refrigerante")]
        Refrigerante,

        [Terms("breja", "beer", "heineken", "skol")]
        Cerveja
    }

    [Describe("CPF na Nota")]
    public enum CPFnaNota
    {
        [Terms("sim", "yes", "si", "s", "y", "hai", "yep")]
        Sim = 1,

        [Terms("nao", "no", "n")]
        Nao
    }
}