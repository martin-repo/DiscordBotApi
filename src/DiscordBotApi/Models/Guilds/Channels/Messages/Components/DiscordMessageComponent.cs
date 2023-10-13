// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageComponent.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

public abstract record DiscordMessageComponent
{
	internal static DiscordMessageComponentDto ConvertToDto(DiscordMessageComponent model)
	{
		switch (model)
		{
			case DiscordMessageActionRow actionRow:
				return new DiscordMessageActionRowDto(model: actionRow);
			case DiscordMessageButton button:
				return new DiscordMessageButtonDto(model: button);
			default:
				throw new NotSupportedException(
					message: $"{typeof(DiscordMessageComponent)} {model.GetType().Name} is not supported");
		}
	}
}