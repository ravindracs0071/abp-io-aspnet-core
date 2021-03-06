USE [Demo]
GO
INSERT [dbo].[AppPropertySettings] ([Id], [TenantId], [Name], [Value], [ProviderName], [ProviderKey], [Visible], [RequiredRegEx], [RegExRule]) VALUES (N'534ed451-2500-494f-be66-04d56a9e0f16', NULL, N'Demo.Incidents.IncidentType', NULL, N'T', NULL, 0, 0, NULL)
GO
INSERT [dbo].[AppPropertySettings] ([Id], [TenantId], [Name], [Value], [ProviderName], [ProviderKey], [Visible], [RequiredRegEx], [RegExRule]) VALUES (N'02ccc602-2a04-40cf-9de5-1e6b85c7a54a', N'dd9f9de4-2b4d-be5f-ceb5-39fb2b1153dd', N'Demo.Incidents.IncidentType', NULL, N'T', NULL, 1, 0, NULL)
GO
INSERT [dbo].[AppPropertySettings] ([Id], [TenantId], [Name], [Value], [ProviderName], [ProviderKey], [Visible], [RequiredRegEx], [RegExRule]) VALUES (N'f333561f-a6d0-433f-b57b-37490b49fe9a', NULL, N'Demo.Incidents.OccurenceDate', NULL, N'T', NULL, 0, 0, NULL)
GO
INSERT [dbo].[AppPropertySettings] ([Id], [TenantId], [Name], [Value], [ProviderName], [ProviderKey], [Visible], [RequiredRegEx], [RegExRule]) VALUES (N'2bb7f20f-4a2d-4c76-b9df-45325456528c', N'dd9f9de4-2b4d-be5f-ceb5-39fb2b1153dd', N'Demo.Incidents.Status', NULL, N'T', NULL, 1, 1, N'^.{5,64}$')
GO
INSERT [dbo].[AppPropertySettings] ([Id], [TenantId], [Name], [Value], [ProviderName], [ProviderKey], [Visible], [RequiredRegEx], [RegExRule]) VALUES (N'c47555fd-7aa7-4224-a3d5-5e6968934689', NULL, N'Demo.Incidents.Reviews.EndDate', NULL, N'T', NULL, 0, 0, NULL)
GO
INSERT [dbo].[AppPropertySettings] ([Id], [TenantId], [Name], [Value], [ProviderName], [ProviderKey], [Visible], [RequiredRegEx], [RegExRule]) VALUES (N'8dce6891-6ecf-4e25-a2cd-607d5eecb5cf', N'dd9f9de4-2b4d-be5f-ceb5-39fb2b1153dd', N'Demo.Incidents.Reviews.IncidentStatus', NULL, N'T', NULL, 1, 1, N'^.{5,64}$')
GO
INSERT [dbo].[AppPropertySettings] ([Id], [TenantId], [Name], [Value], [ProviderName], [ProviderKey], [Visible], [RequiredRegEx], [RegExRule]) VALUES (N'c3bb0359-d1a5-4926-b17a-6870a388733b', N'dd9f9de4-2b4d-be5f-ceb5-39fb2b1153dd', N'Demo.Incidents.OccurenceDate', NULL, N'T', NULL, 1, 0, NULL)
GO
INSERT [dbo].[AppPropertySettings] ([Id], [TenantId], [Name], [Value], [ProviderName], [ProviderKey], [Visible], [RequiredRegEx], [RegExRule]) VALUES (N'977e43a7-ad1d-429f-af93-b37cc7f03941', N'dd9f9de4-2b4d-be5f-ceb5-39fb2b1153dd', N'Demo.Incidents.Reviews.EndDate', NULL, N'T', NULL, 1, 0, NULL)
GO
INSERT [dbo].[AppPropertySettings] ([Id], [TenantId], [Name], [Value], [ProviderName], [ProviderKey], [Visible], [RequiredRegEx], [RegExRule]) VALUES (N'fff2f39e-e5aa-4228-a5aa-c646ce31be30', NULL, N'Demo.Incidents.Status', NULL, N'T', NULL, 0, 0, NULL)
GO
INSERT [dbo].[AppPropertySettings] ([Id], [TenantId], [Name], [Value], [ProviderName], [ProviderKey], [Visible], [RequiredRegEx], [RegExRule]) VALUES (N'8c2c58c5-411e-48e5-87cd-e5d52e8983e2', NULL, N'Demo.Incidents.Reviews.IncidentStatus', NULL, N'T', NULL, 0, 0, NULL)
GO
INSERT [dbo].[AppPropertySettings] ([Id], [TenantId], [Name], [Value], [ProviderName], [ProviderKey], [Visible], [RequiredRegEx], [RegExRule]) VALUES (N'b94fc665-0dfa-491d-8a4a-ec983a1dddea', NULL, N'Demo.Incidents.ReportTo', NULL, N'T', NULL, 0, 0, NULL)
GO
INSERT [dbo].[AppPropertySettings] ([Id], [TenantId], [Name], [Value], [ProviderName], [ProviderKey], [Visible], [RequiredRegEx], [RegExRule]) VALUES (N'3c06e1cd-47d6-4064-8de0-fd14462ff23d', N'dd9f9de4-2b4d-be5f-ceb5-39fb2b1153dd', N'Demo.Incidents.ReportTo', NULL, N'T', NULL, 1, 1, N'^.{5,64}$')
GO
