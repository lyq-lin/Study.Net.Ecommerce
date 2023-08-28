
COPY public."T_Category" ("Id", "Name", "Url", "Deleted", "Visible") FROM stdin;
38b9307b-f6e2-4953-8a43-0292031058e1	耳机	AirPods	f	t
9c814d4b-e5f3-4e12-87b8-cfc54a1da080	电脑	MacBooks	f	t
a0a08e81-9138-4dda-afd1-dea4fb02433b	手表	Watchs	f	t
c2a8fb00-26f3-4b3f-a558-2b22e5ac7614	平板	iPads	f	t
ea2f54ff-d9cf-471a-ba4e-0fc3f16d89b8	手机	iPhones	f	t
\.


COPY public."T_Product" ("Id", "Title", "Description", "ImageUrl", "CategoryId", "Featured", "Deleted", "Visible") FROM stdin;
4029fb11-c928-44fe-a451-842502e41e0a	MacBook Air	8 核中央处理器 10 核图形处理器 8GB 统一内存 256GB 固态硬盘 脚注¹ 16 核神经网络引擎 15.3 英寸 Liquid 视网膜显示屏，支持原彩显示³ 1080p FaceTime 高清摄像头  MagSafe 3 充电端口 两个雷雳 / USB 4 端口 带有触控 ID 的妙控键盘 力度触控板 35W 双 USB-C 端口小型电源适配器	https://www.gizmochina.com/wp-content/uploads/2020/11/MacBook-Air-featured.jpg	9c814d4b-e5f3-4e12-87b8-cfc54a1da080	f	f	t
fc7a333a-32f5-4f43-9858-43af1079b605	11 英寸 iPad 深空灰色	M2 芯片。 M2 是新一代 Apple 芯片，它的 8 核中央处理器提速最高达 15%，10 核图形处理器提速最高达 35%1；神经网络引擎也提速 40%，让机器学习任务执行得更快；再配合增大 50% 的内存带宽，M2 为 iPad Pro 注入澎湃性能和众多新功能。因此你可以尽情创作极具真实感的三维画面，构建复杂的增强现实模型，或是以高帧率畅玩主机级画质的游戏。同时仍能拥有出色的电池续航，可从早用到晚2。	https://img2.ch999img.com/pic/product/800x800/20190107140039272.jpg	c2a8fb00-26f3-4b3f-a558-2b22e5ac7614	f	f	t
b07b851b-45b4-4323-bc33-d2abe640b810	iPhone 14	8 核中央处理器 10 核图形处理器 8GB 统一内存 256GB 固态硬盘 脚注¹ 16 核神经网络引擎 15.3 英寸 Liquid 视网膜显示屏，支持原彩显示³ 1080p FaceTime 高清摄像头  MagSafe 3 充电端口 两个雷雳 / USB 4 端口 带有触控 ID 的妙控键盘 力度触控板 35W 双 USB-C 端口小型电源适配器	https://tse3-mm.cn.bing.net/th/id/OIP-C.PyYoIZQs1YVXOO1Oh0b3iwHaHa?pid=ImgDet&rs=1	ea2f54ff-d9cf-471a-ba4e-0fc3f16d89b8	t	f	t
ec2a9343-e438-460d-a8df-701cd099e153	AppWatch Ultra	Apple Watch 坚固的巅峰，实力的顶峰。强韧的钛金属表壳，精准的双频 GPS，最长 36 小时的电池续航1，还有便利的蜂窝网络功能2，满载一身。更搭配三款专用表带，随时迎接各种挑战。	https://tse4-mm.cn.bing.net/th/id/OIP-C.gdzuQh7D-UOANqSdAd50xQHaHa?pid=ImgDet&rs=1	a0a08e81-9138-4dda-afd1-dea4fb02433b	t	f	t
ea91363a-22d5-4030-a429-40ab76be89bf	iPhone 13	浑身都出彩。RMB 4699 起 6.1 英寸或 5.4 英寸 超视网膜 XDR 显示屏 脚注¹ SOS 紧急联络 双摄系统 主摄 1200 万像素 | 超广角 原深感前置摄像头 视频播放最长可达 19 小时 脚注³ A15 仿生芯片 配备 4 核图形处理器 面容 ID 超快的 5G 蜂窝网络 脚注⁴	https://m.360buyimg.com/babel/jfs/t1/197206/35/9565/184484/614bdea2E03c07a71/4d07f79f2b372acd.jpg	ea2f54ff-d9cf-471a-ba4e-0fc3f16d89b8	f	f	t
299087f6-ff59-41e4-983c-12023c499bef	AirPods 第三代	音乐，为你量耳定制。自适应均衡功能可自动调整你耳中音乐的效果。内向式麦克风会检测你聆听的内容，并以此调整低频和中频音域，为你实时定制音效，使每一首歌曲都能呈现出丰富的细节。	https://gfs17.gomein.net.cn/T1hwxIBgLT1RCvBVdK_360.jpg	38b9307b-f6e2-4953-8a43-0292031058e1	t	f	t
\.


COPY public."T_ProductType" ("Id", "Name") FROM stdin;
21a06e08-69dc-42fc-b5e3-5e3a267d7561	Ultra
84ba1752-917e-412e-96d5-8896a206c590	Pro Max
d9c06b61-0c64-4bb5-a4e7-7642907dbaaa	Pro
e84893a3-cad7-4ef3-8f7d-58e090c7f128	Default
\.


COPY public."T_ProductVariant" ("ProductId", "ProductTypeId", "Id", "Price", "OriginalPrice", "Deleted", "Visible") FROM stdin;
299087f6-ff59-41e4-983c-12023c499bef	d9c06b61-0c64-4bb5-a4e7-7642907dbaaa	ed664522-8efe-435a-88c7-268eb7d1ede2	1299.98	1499.99	f	t
299087f6-ff59-41e4-983c-12023c499bef	e84893a3-cad7-4ef3-8f7d-58e090c7f128	0af7bff8-57dc-47ea-b2d7-f6d459e18507	999.89	1299.65	f	t
4029fb11-c928-44fe-a451-842502e41e0a	d9c06b61-0c64-4bb5-a4e7-7642907dbaaa	a355a369-d899-4041-afff-d487d08d910f	19999.99	23999.89	f	t
4029fb11-c928-44fe-a451-842502e41e0a	e84893a3-cad7-4ef3-8f7d-58e090c7f128	b517ce32-1482-4fbb-b20b-71b134c98cab	12999.99	16999.99	f	t
ec2a9343-e438-460d-a8df-701cd099e153	21a06e08-69dc-42fc-b5e3-5e3a267d7561	bc0eeed2-f35c-485e-82eb-3022a3c7ce4f	1099.98	1499.99	f	t
ec2a9343-e438-460d-a8df-701cd099e153	e84893a3-cad7-4ef3-8f7d-58e090c7f128	17a57b7e-d2e9-4f22-8f87-867bbbe73d02	899.89	1099.65	f	t
fc7a333a-32f5-4f43-9858-43af1079b605	d9c06b61-0c64-4bb5-a4e7-7642907dbaaa	c130c7d4-b6b5-4ca5-b30d-7dad3c3e256e	6999.99	8999.89	f	t
fc7a333a-32f5-4f43-9858-43af1079b605	e84893a3-cad7-4ef3-8f7d-58e090c7f128	eef01cf9-1409-41b8-a5f0-713c549738e6	5999.99	6999.99	f	t
b07b851b-45b4-4323-bc33-d2abe640b810	84ba1752-917e-412e-96d5-8896a206c590	ea0106a5-3785-4cc2-9156-611637bf4661	14299.98	16299.99	f	t
b07b851b-45b4-4323-bc33-d2abe640b810	d9c06b61-0c64-4bb5-a4e7-7642907dbaaa	69bfe3d1-1e8a-41dd-ae47-ef9cc645b2a2	12399.98	14399.99	f	t
b07b851b-45b4-4323-bc33-d2abe640b810	e84893a3-cad7-4ef3-8f7d-58e090c7f128	1f2dd3ef-400e-45e8-bb4f-941149ccc63d	6999.89	81299.65	f	t
ea91363a-22d5-4030-a429-40ab76be89bf	84ba1752-917e-412e-96d5-8896a206c590	4adbf0f8-3b3e-478d-8c10-c6a501348b09	8299.98	12499.99	f	t
ea91363a-22d5-4030-a429-40ab76be89bf	d9c06b61-0c64-4bb5-a4e7-7642907dbaaa	7c0ec2c5-5667-485c-9482-fa600b7931d4	6299.98	9499.99	f	t
ea91363a-22d5-4030-a429-40ab76be89bf	e84893a3-cad7-4ef3-8f7d-58e090c7f128	633a3aa6-0fdb-4d63-8a08-84d701303c27	4999.89	6299.65	f	t
\.


-- Active: 1689663771033@@43.143.170.48@5432@myShop@public

INSERT INTO
    public."T_Category" (
        "Id",
        "Name",
        "Url",
        "Deleted",
        "Visible"
    )
VALUES (
        '38b9307b-f6e2-4953-8a43-0292031058e1',
        '耳机',
        'AirPods',
        false,
        true
    ), (
        '9c814d4b-e5f3-4e12-87b8-cfc54a1da080',
        '电脑',
        'MacBooks',
        false,
        true
    ), (
        'a0a08e81-9138-4dda-afd1-dea4fb02433b',
        '手表',
        'Watchs',
        false,
        true
    ), (
        'c2a8fb00-26f3-4b3f-a558-2b22e5ac7614',
        '平板',
        'iPads',
        false,
        true
    ), (
        'ea2f54ff-d9cf-471a-ba4e-0fc3f16d89b8',
        '手机',
        'iPhones',
        false,
        true
    );



INSERT INTO public."T_Product" ("Id", "Title", "Description", "ImageUrl", "CategoryId", "Featured", "Deleted", "Visible")
VALUES ('4029fb11-c928-44fe-a451-842502e41e0a', 'MacBook Air', '8 核中央处理器 10 核图形处理器 8GB 统一内存 256GB 固态硬盘 脚注¹ 16 核神经网络引擎 15.3 英寸 Liquid 视网膜显示屏，支持原彩显示³ 1080p FaceTime 高清摄像头  MagSafe 3 充电端口 两个雷雳 / USB 4 端口 带有触控 ID 的妙控键盘 力度触控板 35W 双 USB-C 端口小型电源适配器', 'https://www.gizmochina.com/wp-content/uploads/2020/11/MacBook-Air-featured.jpg', '9c814d4b-e5f3-4e12-87b8-cfc54a1da080', false, false, true);

INSERT INTO
    public."T_Product" (
        "Id",
        "Title",
        "Description",
        "ImageUrl",
        "CategoryId",
        "Featured",
        "Deleted",
        "Visible"
    )
VALUES (
        'fc7a333a-32f5-4f43-9858-43af1079b605',
        '11 英寸 iPad 深空灰色',
        'M2 芯片。 M2 是新一代 Apple 芯片，它的 8 核中央处理器提速最高达 15%，10 核图形处理器提速最高达 35%1；神经网络引擎也提速 40%，让机器学习任务执行得更快；再配合增大 50% 的内存带宽，M2 为 iPad Pro 注入澎湃性能和众多新功能。因此你可以尽情创作极具真实感的三维画面，构建复杂的增强现实模型，或是以高帧率畅玩主机级画质的游戏。同时仍能拥有出色的电池续航，可从早用到晚2',
        'https://img2.ch999img.com/pic/product/800x800/20190107140039272.jpg',
        'c2a8fb00-26f3-4b3f-a558-2b22e5ac7614',
        false,
        false,
        true
    );

INSERT INTO
    public."T_Product" (
        "Id",
        "Title",
        "Description",
        "ImageUrl",
        "CategoryId",
        "Featured",
        "Deleted",
        "Visible"
    )
VALUES (
        'b07b851b-45b4-4323-bc33-d2abe640b810',
        'iPhone 14',
        '8 核中央处理器 10 核图形处理器 8GB 统一内存 256GB 固态硬盘 脚注¹ 16 核神经网络引擎 15.3 英寸 Liquid 视网膜显示屏，支持原彩显示³ 1080p FaceTime 高清摄像头  MagSafe 3 充电端口 两个雷雳 / USB 4 端口 带有触控 ID 的妙控键盘 力度触控板 35W 双 USB-C 端口小型电源适配器',
        'https://tse3-mm.cn.bing.net/th/id/OIP-C.PyYoIZQs1YVXOO1Oh0b3iwHaHa?pid=ImgDet&rs=1',
        'ea2f54ff-d9cf-471a-ba4e-0fc3f16d89b8',
        true,
        false,
        true
    );

INSERT INTO
    public."T_Product" (
        "Id",
        "Title",
        "Description",
        "ImageUrl",
        "CategoryId",
        "Featured",
        "Deleted",
        "Visible"
    )
VALUES (
        'ec2a9343-e438-460d-a8df-701cd099e153',
        'AppWatch Ultra',
        'Apple Watch 坚固的巅峰，实力的顶峰。强韧的钛金属表壳，精准的双频 GPS，最长 36 小时的电池续航1，还有便利的蜂窝网络功能2，满载一身。更搭配三款专用表带，随时迎接各种挑战。',
        'https://tse4-mm.cn.bing.net/th/id/OIP-C.gdzuQh7D-UOANqSdAd50xQHaHa?pid=ImgDet&rs=1',
        'a0a08e81-9138-4dda-afd1-dea4fb02433b',
        true,
        false,
        true
    );

INSERT INTO
    public."T_Product" (
        "Id",
        "Title",
        "Description",
        "ImageUrl",
        "CategoryId",
        "Featured",
        "Deleted",
        "Visible"
    )
VALUES (
        'ea91363a-22d5-4030-a429-40ab76be89bf',
        'iPhone 13',
        '浑身都出彩。RMB 4699 起 6.1 英寸或 5.4 英寸 超视网膜 XDR 显示屏 脚注¹ SOS 紧急联络 双摄系统 主摄 1200 万像素 | 超广角 原深感前置摄像头 视频播放最长可达 19 小时 脚注³ A15 仿生芯片 配备 4 核图形处理器 面容 ID 超快的 5G 蜂窝网络 脚注⁴',
        'https://m.360buyimg.com/babel/jfs/t1/197206/35/9565/184484/614bdea2E03c07a71/4d07f79f2b372acd.jpg',
        'ea2f54ff-d9cf-471a-ba4e-0fc3f16d89b8',
        false,
        false,
        true
    );

INSERT INTO
    public."T_Product" (
        "Id",
        "Title",
        "Description",
        "ImageUrl",
        "CategoryId",
        "Featured",
        "Deleted",
        "Visible"
    )
VALUES (
        '299087f6-ff59-41e4-983c-12023c499bef',
        'AirPods 第三代',
        '音乐，为你量耳定制。自适应均衡功能可自动调整你耳中音乐的效果。内向式麦克风会检测你聆听的内容，并以此调整低频和中频音域，为你实时定制音效，使每一首歌曲都能呈现出丰富的细节。',
        'https://gfs17.gomein.net.cn/T1hwxIBgLT1RCvBVdK_360.jpg',
        '38b9307b-f6e2-4953-8a43-0292031058e1',
        true,
        false,
        true
    );


INSERT INTO public."T_ProductType" ("Id", "Name")
VALUES
   ('21a06e08-69dc-42fc-b5e3-5e3a267d7561', 'Ultra'),
   ('84ba1752-917e-412e-96d5-8896a206c590', 'Pro Max'),
   ('d9c06b61-0c64-4bb5-a4e7-7642907dbaaa', 'Pro'),
   ('e84893a3-cad7-4ef3-8f7d-58e090c7f128', 'Default');


INSERT INTO public."T_ProductVariant" ("ProductId", "ProductTypeId", "Id", "Price", "OriginalPrice", "Deleted", "Visible")
VALUES
   ('299087f6-ff59-41e4-983c-12023c499bef', 'd9c06b61-0c64-4bb5-a4e7-7642907dbaaa', 'ed664522-8efe-435a-88c7-268eb7d1ede2', 1299.98, 1499.99, false, true),
   ('299087f6-ff59-41e4-983c-12023c499bef', 'e84893a3-cad7-4ef3-8f7d-58e090c7f128', '0af7bff8-57dc-47ea-b2d7-f6d459e18507', 999.89, 1299.65, false, true),
   ('4029fb11-c928-44fe-a451-842502e41e0a', 'd9c06b61-0c64-4bb5-a4e7-7642907dbaaa', 'a355a369-d899-4041-afff-d487d08d910f', 19999.99, 23999.89, false, true),
   ('4029fb11-c928-44fe-a451-842502e41e0a', 'e84893a3-cad7-4ef3-8f7d-58e090c7f128', 'b517ce32-1482-4fbb-b20b-71b134c98cab', 12999.99, 16999.99, false, true),
   ('ec2a9343-e438-460d-a8df-701cd099e153', '21a06e08-69dc-42fc-b5e3-5e3a267d7561', 'bc0eeed2-f35c-485e-82eb-3022a3c7ce4f', 1099.98, 1499.99, false, true),
   ('ec2a9343-e438-460d-a8df-701cd099e153', 'e84893a3-cad7-4ef3-8f7d-58e090c7f128', '17a57b7e-d2e9-4f22-8f87-867bbbe73d02', 899.89, 1099.65, false, true),
   ('fc7a333a-32f5-4f43-9858-43af1079b605', 'd9c06b61-0c64-4bb5-a4e7-7642907dbaaa', 'c130c7d4-b6b5-4ca5-b30d-7dad3c3e256e', 6999.99, 8999.89, false, true),
   ('fc7a333a-32f5-4f43-9858-43af1079b605', 'e84893a3-cad7-4ef3-8f7d-58e090c7f128', 'eef01cf9-1409-41b8-a5f0-713c549738e6', 5999.99, 6999.99, false, true),
   ('b07b851b-45b4-4323-bc33-d2abe640b810', '84ba1752-917e-412e-96d5-8896a206c590', 'ea0106a5-3785-4cc2-9156-611637bf4661', 14299.98, 16299.99, false, true),
   ('b07b851b-45b4-4323-bc33-d2abe640b810', 'd9c06b61-0c64-4bb5-a4e7-7642907dbaaa', '69bfe3d1-1e8a-41dd-ae47-ef9cc645b2a2', 12399.98, 14399.99, false, true),
   ('b07b851b-45b4-4323-bc33-d2abe640b810', 'e84893a3-cad7-4ef3-8f7d-58e090c7f128', '1f2dd3ef-400e-45e8-bb4f-941149ccc63d', 6999.89, 81299.65, false, true),
   ('ea91363a-22d5-4030-a429-40ab76be89bf', '84ba1752-917e-412e-96d5-8896a206c590', '4adbf0f8-3b3e-478d-8c10-c6a501348b09', 8299.98, 12499.99, false, true),
   ('ea91363a-22d5-4030-a429-40ab76be89bf', 'd9c06b61-0c64-4bb5-a4e7-7642907dbaaa', '7c0ec2c5-5667-485c-9482-fa600b7931d4', 6299.98, 9499.99, false, true),
   ('ea91363a-22d5-4030-a429-40ab76be89bf', 'e84893a3-cad7-4ef3-8f7d-58e090c7f128', '633a3aa6-0fdb-4d63-8a08-84d701303c27', 4999.89, 6299.65, false, true);


