﻿// -------------------------------------------------------------------------------------------------
// <copyright file="SourceCodeGenerator.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;

using DiscordBotApi.Interface;
using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;

namespace DiscordBotApi.Builders.Generator;

public class SourceCodeGenerator
{
	private const string TargetNamespace = "DiscordBotApi.Interface.Models";

	public void Main()
	{
		var assembly = typeof(IDiscordBotClient).Assembly;

		var modelTypes = assembly
			.GetTypes()
			.Where(
				predicate: v =>
					v is { IsPublic: true, Namespace: not null } &&
					v.Namespace.StartsWith(value: TargetNamespace, comparisonType: StringComparison.Ordinal) &&
					HasPublicConstructor(type: v)
			)
			.ToImmutableArray();

		var globalUsingsBuilder = new StringBuilder();
		globalUsingsBuilder.AppendLine(
			value: """
			global using System.Collections.Immutable;
			global using System.Drawing;
			"""
		);
		globalUsingsBuilder.AppendLine();
		var modelNamespaceNames = assembly
			.GetTypes()
			.Where(
				predicate: v =>
					v.Namespace is not null &&
					v.Namespace.StartsWith(value: TargetNamespace, comparisonType: StringComparison.Ordinal)
			)
			.Select(selector: v => v.Namespace)
			.Distinct()
			.Order()
			.ToImmutableArray();
		globalUsingsBuilder.AppendLine(
			value: string.Join(
				separator: Environment.NewLine,
				values: modelNamespaceNames.Select(selector: v => $"global using {v};")
			)
		);
		globalUsingsBuilder.AppendLine();
		globalUsingsBuilder.Append(
			value: string.Join(
				separator: Environment.NewLine,
				values: modelTypes
					.Select(selector: v => $"global using DiscordBotApi.Builders{v.Namespace![TargetNamespace.Length..]};")
					.Distinct()
					.Order()
			)
		);

		var assemblyLocation = Assembly.GetExecutingAssembly().Location;
		var assemblyDirectory = new DirectoryInfo(
			path: Path.GetDirectoryName(path: assemblyLocation) ??
			throw new InvalidOperationException(message: "Invalid location")
		);
		var baseFolder = assemblyDirectory.Parent?.Parent?.Parent?.Parent?.FullName ??
			throw new InvalidOperationException(message: "Invalid location");
		var rootFolder = Path.Combine(path1: baseFolder, path2: "DiscordBotApi.Builders");

		File.WriteAllText(
			path: Path.Combine(path1: rootFolder, path2: "GlobalUsings.cs"),
			contents: globalUsingsBuilder.ToString()
		);

		foreach (var type in modelTypes)
		{
			var subNamespace = type.Namespace![TargetNamespace.Length..].Trim(trimChar: '.');
			var folders = subNamespace.Split(separator: '.').ToImmutableArray();
			if (folders.IsEmpty)
			{
				throw new InvalidOperationException();
			}

			var folderPath = rootFolder;
			foreach (var folder in folders)
			{
				folderPath = Path.Combine(path1: folderPath, path2: folder);
			}

			Directory.CreateDirectory(path: folderPath);

			var filename = $"{type.Name}Builder.cs";
			var codeBuilder = new StringBuilder();
			codeBuilder.AppendLine(
				handler: $$"""
				// -------------------------------------------------------------------------------------------------
				// <copyright file="{{filename}}" company="kpop.fan">
				//   Copyright (c) 2025 kpop.fan. All rights reserved.
				// </copyright>
				// -------------------------------------------------------------------------------------------------

				namespace DiscordBotApi.Builders.{{subNamespace}};

				// WARNING! This file was generated by a tool.
				//          Any changes made to this file will be lost if the code is regenerated.
				public class {{type.Name}}Builder
				{
				"""
			);

			var properties = type.GetProperties(bindingAttr: BindingFlags.Public | BindingFlags.Instance);
			var propertyDatas = properties.Select(selector: GetPropertyData).ToImmutableArray();

			foreach (var data in propertyDatas)
			{
				var readOnly = data is { IsNullable: false, IsEnumerable: true } ? " readonly" : null;
				var defaultValue = data.IsNullable
					? null
					: data.IsEnumerable
						? " = new()"
						: " = default!";
				codeBuilder.AppendLine(handler: $"	private{readOnly} {data.TypeName} {data.FieldName}{defaultValue};");
			}

			foreach (var data in propertyDatas)
			{
				if (data.IsEnumerable)
				{
					var listConstruction = data.IsNullable
						? $"{Environment.NewLine}		{data.FieldName} ??= new List<{data.GenericTypeName}>();"
						: null;
					if (data.GenericType == typeof(DiscordMessageComponent))
					{
						codeBuilder.AppendLine(
							handler: $$"""

								public {{type.Name}}Builder AddActionRow(Action<DiscordMessageActionRowBuilder> builderAction)
								{
									var builder = new DiscordMessageActionRowBuilder();
									builderAction(obj: builder);{{listConstruction}}
									{{data.FieldName}}.Add(item: builder.Build());
									return this;
								}
							"""
						);
						if (type == typeof(DiscordMessageActionRow))
						{
							codeBuilder.AppendLine(
								handler: $$"""

									public DiscordMessageActionRowBuilder AddButton(Action<DiscordMessageButtonBuilder> builderAction)
									{
										var builder = new DiscordMessageButtonBuilder();
										builderAction(obj: builder);{{listConstruction}}
										{{data.FieldName}}.Add(item: builder.Build());
										return this;
									}

									public DiscordMessageActionRowBuilder AddButton(DiscordMessageButton button)
									{
										{{data.FieldName}}.Add(item: button);
										return this;
									}

									public DiscordMessageActionRowBuilder AddSelectMenu(Action<DiscordMessageSelectMenuBuilder> builderAction)
									{
										var builder = new DiscordMessageSelectMenuBuilder();
										builderAction(obj: builder);{{listConstruction}}
										{{data.FieldName}}.Add(item: builder.Build());
										return this;
									}

									public DiscordMessageActionRowBuilder AddSelectMenu(DiscordMessageSelectMenu selectMenu)
									{
										{{data.FieldName}}.Add(item: selectMenu);
										return this;
									}

									public DiscordMessageActionRowBuilder AddTextInput(Action<DiscordMessageTextInputBuilder> builderAction)
									{
										var builder = new DiscordMessageTextInputBuilder();
										builderAction(obj: builder);{{listConstruction}}
										{{data.FieldName}}.Add(item: builder.Build());
										return this;
									}

									public DiscordMessageActionRowBuilder AddButton(DiscordMessageTextInput textInput)
									{
										{{data.FieldName}}.Add(item: textInput);
										return this;
									}
								"""
							);
						}
					}
					else if (data.GenericType?.IsValueType == true || data.GenericType == typeof(string))
					{
						var argumentName = GetSingularName(pluralName: data.ArgumentName);
						codeBuilder.AppendLine(
							handler: $$"""

								public {{type.Name}}Builder Add{{GetSingularName(pluralName: data.PropertyName)}}({{data.GenericTypeName}} {{argumentName}})
								{{{listConstruction}}
									{{data.FieldName}}.Add(item: {{argumentName}});
									return this;
								}
							"""
						);
					}
					else
					{
						codeBuilder.AppendLine(
							handler: $$"""

								public {{type.Name}}Builder Add{{GetSingularName(pluralName: data.PropertyName)}}(Action<{{data.GenericTypeName}}Builder> builderAction)
								{
									var builder = new {{data.GenericTypeName!}}Builder();
									builderAction(obj: builder);{{listConstruction}}
									{{data.FieldName}}.Add(item: builder.Build());
									return this;
								}

								public {{type.Name}}Builder Add{{GetSingularName(pluralName: data.PropertyName)}}({{data.GenericTypeName}} item)
								{{{listConstruction}}
									{{data.FieldName}}.Add(item: item);
									return this;
								}
							"""
						);
					}
				}
				else
				{
					var argumentName = data.ArgumentName switch
					{
						"default" => "@default",
						_ => data.ArgumentName
					};
					codeBuilder.AppendLine(
						handler: $$"""

							public {{type.Name}}Builder With{{data.PropertyName}}({{data.TypeName}} {{argumentName}})
							{
								{{data.FieldName}} = {{argumentName}};
								return this;
							}
						"""
					);
				}
			}

			codeBuilder.AppendLine(
				handler: $$"""

					public {{type.Name}} Build() =>
						new()
						{
				"""
			);
			foreach (var data in propertyDatas)
			{
				var conversion = !data.IsEnumerable
					? null
					: data.IsNullable
						? "?.ToImmutableArray()"
						: ".ToImmutableArray()";
				codeBuilder.AppendLine(handler: $"			{data.PropertyName} = {data.FieldName}{conversion},");
			}

			codeBuilder.AppendLine(value: "		};");

			codeBuilder.Append(value: '}');

			var filePath = Path.Combine(path1: folderPath, path2: filename);
			File.WriteAllText(path: filePath, contents: codeBuilder.ToString());

			Console.WriteLine(value: type.FullName);
		}

		return;

		static bool HasPublicConstructor(Type type)
		{
			var constructors = type.GetConstructors(bindingAttr: BindingFlags.Public | BindingFlags.Instance);
			return constructors.Length > 0;
		}
	}

	private static PropertyData GetPropertyData(PropertyInfo property)
	{
		var (propertyType, isNullable) = GetTypeInfo(property: property);

		var genericType = propertyType.GetGenericArguments().SingleOrDefault();

		var type = genericType ?? propertyType;

		var isEnumerable = propertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(c: propertyType);

		var argumentName = $"{char.ToLower(c: property.Name[index: 0])}{property.Name[1..]}";
		var fieldName = $"_{argumentName}";

		var typeName = GetTypeName(type: type, isNullable: isNullable, isEnumerable: isEnumerable);
		var genericTypeName = genericType is not null
			? GetTypeName(type: genericType, isNullable: false, isEnumerable: false)
			: null;

		return new PropertyData(
			PropertyName: property.Name,
			Type: type,
			GenericType: genericType,
			TypeName: typeName,
			GenericTypeName: genericTypeName,
			FieldName: fieldName,
			ArgumentName: argumentName,
			IsNullable: isNullable,
			IsEnumerable: isEnumerable
		);

		static (Type Type, bool IsNullable) GetTypeInfo(PropertyInfo property)
		{
			var propertyType = property.PropertyType;

			// Check for nullable value types
			var nullableType = Nullable.GetUnderlyingType(nullableType: propertyType);
			if (nullableType is not null)
			{
				return (nullableType, true);
			}

			// Check for nullable reference types
			var nullabilityContext = new NullabilityInfoContext();
			var nullabilityInfo = nullabilityContext.Create(propertyInfo: property);
			return (propertyType, nullabilityInfo.ReadState == NullabilityState.Nullable);
		}

		static string GetTypeName(Type type, bool isNullable, bool isEnumerable)
		{
			var typeName = type.FullName switch
			{
				"System.Boolean" => "bool",
				"System.Double" => "double",
				"System.Int32" => "int",
				"System.Object" => "object",
				"System.String" => "string",
				"System.UInt64" => "ulong",
				_ => type.Name
			};
			if (isEnumerable)
			{
				typeName = $"List<{typeName}>";
			}

			if (isNullable)
			{
				typeName = $"{typeName}?";
			}

			return typeName;
		}
	}

	private static string GetSingularName(string pluralName) =>
		pluralName.EndsWith(value: "ies", comparisonType: StringComparison.OrdinalIgnoreCase)
			? $"{pluralName[..^3]}y"
			: $"{pluralName[..^1]}";

	private record PropertyData(
		string PropertyName,
		Type Type,
		Type? GenericType,
		string TypeName,
		string? GenericTypeName,
		string FieldName,
		string ArgumentName,
		bool IsNullable,
		bool IsEnumerable
	);
}