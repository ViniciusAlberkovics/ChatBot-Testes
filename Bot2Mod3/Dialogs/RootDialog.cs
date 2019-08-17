using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot2Mod3.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
            public int cont = 0;
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;


            if(cont == 0)
            {
                await context.PostAsync("**Eae Man! Kkk**");
                cont++;
            }

            var message = activity.CreateReply();


            if (activity.Text.Equals("herocard", StringComparison.InvariantCultureIgnoreCase))
            {
                var attachment = CreateHeroCard();
                message.Attachments.Add(attachment);
            }
            else if (activity.Text.Equals("videocard", StringComparison.InvariantCultureIgnoreCase))
            {
                var attachment = CreateVideoCard();
                message.Attachments.Add(attachment);
            }
            else if(activity.Text.Equals("audiocard", StringComparison.InvariantCultureIgnoreCase))
            {
                var attachment = CreateAudioCard();
                message.Attachments.Add(attachment);
            }
            else if (activity.Text.Equals("animationcard", StringComparison.InvariantCultureIgnoreCase))
            {
                var attachment = CreateAnimationCard();
                message.Attachments.Add(attachment);
            }
            else if (activity.Text.Equals("carousel"))
            {
                message.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                var audio = CreateAudioCard();
                var animation = CreateAnimationCard();

                message.Attachments.Add(audio);
                message.Attachments.Add(animation);
            }

            await context.PostAsync(message);

            context.Wait(MessageReceivedAsync);
        }

        private Attachment CreateAnimationCard()
        {
            var animationCard = new AnimationCard();
            animationCard.Title = "Olha o GIF";
            animationCard.Subtitle = "Subtitulo do GIPHY.baduntis";
            animationCard.Autostart = true;
            animationCard.Autoloop = false;
            animationCard.Media = new List<MediaUrl>
                {
                    new MediaUrl("http://78.media.tumblr.com/4163afe60a01a3a58332e45fc4efe060/tumblr_mw0i4maRMZ1r8bsyso1_250.gif")
                };

            return animationCard.ToAttachment();
        }

        private Attachment CreateAudioCard()
        {
            var audioCard = new AudioCard();
            audioCard.Title = "Que audio é esse?";
            audioCard.Image = new ThumbnailUrl("http://ovicio.com.br/wp-content/uploads/One-Piece.jpg", "Tumbnail One Piece");
            audioCard.Subtitle = "Subtitulo do audio";
            audioCard.Autostart = false;
            audioCard.Autoloop = false;
            audioCard.Media = new List<MediaUrl>
                {
                    new MediaUrl("http://www.wavlist.com/humor/001/911d.wav")
                };

            return audioCard.ToAttachment();
        }

        private Attachment CreateVideoCard()
        {
            var videoCard = new VideoCard();
            videoCard.Title = "Algum video ai man";
            videoCard.Subtitle = "Subtitulo do video";
            videoCard.Autostart = true;
            videoCard.Autoloop = false;
            videoCard.Media = new List<MediaUrl>
                {
                    new MediaUrl("https://www.youtube.com/watch?v=sNPBN3zlIwo")
                };

            return videoCard.ToAttachment();
        }

        private Attachment CreateHeroCard()
        {
            var heroCard = new HeroCard();//mensagem em cartao
            heroCard.Title = "Sei que é Titulo!";
            heroCard.Subtitle = "Deve ser o subtitulo então!!";
            heroCard.Images = new List<CardImage>
                {
                    new CardImage("http://ovicio.com.br/wp-content/uploads/One-Piece.jpg","One Piece", new CardAction(ActionTypes.OpenUrl, "Google", value:"https://www.google.com.br"))
                };

            heroCard.Buttons = new List<CardAction>
            {
                new CardAction
                {
                    Text = "botao herocard",
                    DisplayText = "Display de texto",
                    Title = "Titulo do botao",
                    Type = ActionTypes.PostBack,
                    Value = "carousel"
                }

            };


            return heroCard.ToAttachment();
        }
    }
}