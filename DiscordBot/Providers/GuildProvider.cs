﻿/*
_________________________________________________________________________
|                                                                       |
|██████╗░░█████╗░████████╗  ██████╗░░█████╗░████████╗██╗░░░██╗░█████╗░  |
|██╔══██╗██╔══██╗╚══██╔══╝  ██╔══██╗██╔══██╗╚══██╔══╝╚██╗░██╔╝██╔══██╗  |
|██████╦╝██║░░██║░░░██║░░░  ██████╦╝██║░░██║░░░██║░░░░╚████╔╝░███████║  |
|██╔══██╗██║░░██║░░░██║░░░  ██╔══██╗██║░░██║░░░██║░░░░░╚██╔╝░░██╔══██║  |
|██████╦╝╚█████╔╝░░░██║░░░  ██████╦╝╚█████╔╝░░░██║░░░░░░██║░░░██║░░██║  |
|╚═════╝░░╚════╝░░░░╚═╝░░░  ╚═════╝░░╚════╝░░░░╚═╝░░░░░░╚═╝░░░╚═╝░░╚═╝  |
|______________________________________________________________________ |
|Author: Denis Voitenko.                                                |
|GitHub: https://github.com/DenchickPenchick                            |
|DEV: https://dev.to/denchickpenchick                                   |
|_____________________________Project__________________________________ |
|GitHub: https://github.com/DenchickPenchick/BotBotya                   |
|______________________________________________________________________ |
|© Denis Voitenko                                                       |
_________________________________________________________________________
 */

using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using DiscordBot.FileWorking;
using DiscordBot.Modules.FileManaging;

namespace DiscordBot.Providers
{
    public class GuildProvider
    {
        public SocketGuild Guild { get; }
        private SerializableGuild SerializableGuild { get => FilesProvider.GetGuild(Guild);}

        public GuildProvider(SocketGuild guild)
        {            
            Guild = guild;
        }

        public SocketVoiceChannel CreateRoomChannel() => Guild.GetVoiceChannel(SerializableGuild.SystemChannels.CreateRoomChannelId);
        public SocketTextChannel LinksTextChannel() => Guild.GetTextChannel(SerializableGuild.SystemChannels.LinksChannelId);
        public SocketTextChannel VideosTextChannel() => Guild.GetTextChannel(SerializableGuild.SystemChannels.VideosChannelId);        
        public SocketCategoryChannel RoomsCategoryChannel() => Guild.GetCategoryChannel(SerializableGuild.SystemCategories.VoiceRoomsCategoryId);
        public SocketCategoryChannel ContentCategoryChannel() => Guild.GetCategoryChannel(SerializableGuild.SystemCategories.ContentCategoryId);                

        public enum GetCategoryIdEnum { MainVoiceChannelsCategory, RoomVoiceChannelsCategory, MainTextChannelsCategory, ContentChannelsCategory, BotChannelsCategory }
        

        public async Task SendHelloMessageToGuild(DiscordSocketClient client)
        {
            await Guild.DefaultChannel.SendMessageAsync(embed: new EmbedBuilder
            {
                Title = $"👋 Спасибо, что пригласили меня на сервер {Guild.Name} 👋",
                Description = $"Меня зовут {client.CurrentUser.Username}. Я много что умею! Пропиши !Справка, чтобы узнать мой функционал.\n" +
                $"🤖 Мой сайт: https://botbotya.ru 🤖",
                Color = Color.Blue
            }.Build());
        }                

        public bool ExistChannelByName(SocketGuildChannel channel)
        {
            string name = channel.Name;
            var guild = channel.Guild;
            bool val = false;

            foreach (var ch in guild.Channels)
            {
                if (ch.Name == name)
                { 
                    val = true;
                    continue;
                }                 
            }

            return val;
        }
    }
}