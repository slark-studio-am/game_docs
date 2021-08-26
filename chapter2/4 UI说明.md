# 4 UI说明

### 4.1 目录

- [<div>4.1 单位信息 Bottom/Unit_Attributes</div>](#41)
- [<div>4.1.1 各项目说明</div>](#411)
- [<div>4.1.2 信息说明的显示介绍</div>](#412)
- [<div>4.1.3 主动技能栏 - 补充介绍</div>](#413)
- [<div>4.1.4 被动技能栏-补充介绍</div>](#414)
- [<div>4.2 战场信息</div>](#42)
- [<div>4.3 战利品 - 数据关联7.4</div>](#43)

### 4.1 单位信息 Bottom/Unit_Attributes<div id="41">

- 默认选中最下方的友方单位

![image-20210825170410101](https://i.loli.net/2021/08/25/OFny9deMaXSqQbu.png)

### 4.1.1 各项目说明<div id="411">

| 序号 |   项目   |                             说明                             |                          关联路径                           |                       关联资源                        |
| :--: | :------: | :----------------------------------------------------------: | :---------------------------------------------------------: | :---------------------------------------------------: |
|  1   | 单位头像 |                      显示选中单位的头像                      |        `/`UI`/`Bottom_UI`/`Unit_Avatar`/`Unit_Image         | Assets`/`_`/`Unit`/`任意单位文件夹`/`Sprites`/`Avatar |
|  2   | 单位名称 |                      显示选中单位的名称                      |         `/`UI`/`Bottom_UI`/`Unit_Avatar`/`Unit_Name         |                     来自单位说明                      |
|  3   | 单位属性 |                  显示选中单位的所有属性及值                  | `/`UI`/`Bottom_UI`/`Unit_total_Attributes`/`Attributes_List |                     来自单位说明                      |
|  4   | 主动技能 | 显示选中单位所有主动技能的名称。补充说明见[<div>4.1.3</div>](#413) |              `/`UI`/`Bottom_UI`/`Active_Skills              |                     来自单位说明                      |
|  5   | 被动技能 | 显示选中单位所有被动技能的名称。补充说明见[<div>4.1.4</div>](#414) |             `/`UI`/`Bottom_UI`/`Passive_Skills              |                     来自单位说明                      |
|  6   | 信息说明 | 用于对所选技能/神器进行展开说明，详见[<div>4.1.2</div>](#412) |            `/`UI`/`Bottom_UI`/`Description_Text             |                     来自技能说明                      |
|  7   |  神器栏  |                   显示目前所获得的所有神器                   |                `/`UI`/`Bottom_UI`/`Artifact                 |                           -                           |



### 4.1.2 信息说明的显示介绍<div id="412">

- #### 默认显示被选单位的选中主动技能，以下是点击不同元素时的信息显示

|      标题大字      | 标题小字 |         图片         |         说明         |
| :----------------: | :------: | :------------------: | :------------------: |
| 被选中的主动技能名 | 主动技能 | 被选中的主动技能图标 | 被选中的主动技能介绍 |
| 被选中的被动技能名 | 被动技能 | 被选中的被动技能图标 | 被选中的被动技能介绍 |
|   被选中的神器名   |   神器   |   被选中的神器图标   |   被选中的神器介绍   |



### 4.1.3 主动技能栏-补充介绍<div id="413">

- #### 主动技能栏固定3个技能，如果单位少于3个主动技能，则缺失的部分显示“-----”，即5个 -

![image-20210825180247775](https://i.loli.net/2021/08/25/Lsgp1cw5BT9JWCi.png)

- #### 代码是否可以做到在已经被设置为主动技能的技能名前面加一个小三角形，如下图。主要是字体不支持unicode，不然有相应字符的。

![image-20210825194022867](https://i.loli.net/2021/08/25/1vkOwbAFrQtqfE7.png)

|       情况       |    文字内容     |           说明            |
| :--------------: | :-------------: | :-----------------------: |
|       正常       |    盛大登场     |        显示技能名         |
| 被选中为主动技能 | &#9656;盛大登场 | 显示【小三角】+【技能名】 |
|      空技能      |      -----      |       显示5个【-】        |

- #### 选中主动技能时，会弹出选项（选项框的路径-Assets/Prefabs/UI/Pixel_Ver/New_ToolTips，选项框跟随选项个数浮动调整高度），选项内容中如果包含color标签，则如数在代码中还原，TMP富文本控件可以支持color标签。表格内的标签由于会被markdown编码，所以多加了空格

![image-20210825192346814](https://i.loli.net/2021/08/25/2ZAkzl8gdsnF7pC.png)

| 选项  |            选项内容             |           点击后功能           |
| :---: | :-----------------------------: | :----------------------------: |
| 选项1 |         设置为主动技能          | 将所选技能设置为使用的主动技能 |
| 选项2 | < color=#B5B5B5 >取消< /color > |           隐藏选项框           |



### 4.1.4 被动技能栏-补充介绍<div id="414">

- #### 被动技能栏固定6个技能，如果单位少于6个被动技能，则缺失的部分显示“-----”，即5个 -

![image-20210825194201326](https://i.loli.net/2021/08/25/AxhjizRat8CE1lT.png)

- #### 代码是否可以做到在已经被设置为主动技能的技能名前面加一个小三角形，如下图。主要是字体不支持unicode，不然有相应字符的。

![image-20210825194245925](https://i.loli.net/2021/08/25/yOCYp68w57goa3A.png)

|       情况       |     文字内容     |                  说明                  |
| :--------------: | :--------------: | :------------------------------------: |
|       正常       |     盛大登场     |               显示技能名               |
| 被选中为被动技能 | &#9656;1盛大登场 | 显示【小三角】+【技能顺位】+【技能名】 |
|      空技能      |      -----       |              显示5个【-】              |



- #### 选中主动技能时，会弹出选项（选项框的路径-Assets/Prefabs/UI/Pixel_Ver/New_ToolTips，选项框跟随选项个数浮动调整高度），选项内容中如果包含color标签，则如数在代码中还原，TMP富文本控件可以支持color标签。表格内的标签由于会被markdown编码，所以多加了空格。

![image-20210825194742688](https://i.loli.net/2021/08/25/Cyrz16PqHXZWKEM.png)

| 选项  |            选项内容             |                        功能                         |
| :---: | :-----------------------------: | :-------------------------------------------------: |
| 选项1 |        设置为第1顺位被动        |          将所选技能设置为第1顺位的被动技能          |
| 选项2 |        设置为第2顺位被动        |          将所选技能设置为第2顺位的被动技能          |
| 选项3 |        设置为第3顺位被动        |          将所选技能设置为第3顺位的被动技能          |
| 选项4 | < color=red >删除技能< /color > | 将所选技能从被动技能中移除，被移除后的空位显示----- |
| 选项5 | < color=#B5B5B5 >取消< /color > |                     隐藏选项框                      |

- #### 被动技能栏的填充顺序如下表，该表用于表示被动技能栏中各位置的序号。当有技能被删除后，后面的向前填充，比如1号位的技能被删除了，原来的2就到1，后面的以此类推；获得新被动时，优先填充序号最低的空位。

| 序号 | 序号 |
| :--: | :--: |
|  1   |  2   |
|  3   |  4   |
|  5   |  6   |

- #### 对1个被动技能设置顺位的变换（玩家不设置技能顺位的话系统不会进行默认设置，可以存在无被动）

|          设置顺位前           |     设置顺位     |                   设置顺位后                    |
| :---------------------------: | :--------------: | :---------------------------------------------: |
| 顺位1 - 技能A ；顺位2 - 空缺  | 设置技能A为顺位2 |           顺位1 - 空缺；顺位2 - 技能A           |
| 顺位1 - 技能A ；顺位2 - 技能B | 设置技能A为顺位2 | 顺位1 - 空缺；顺位2 - 技能A；非选中技能 - 技能B |
|         顺位1 - 技能A         | 设置技能A为顺位1 |                  顺位1 - 技能A                  |



### 4.2 战场信息 Prefabs/Battle/Pixel_Ver/New_Battle_seat<div id="42">

![image-20210826140725306](https://i.loli.net/2021/08/26/KAl2H3ONnWiUx6Y.png)

#### 4.2.1 各项目说明<div id="421">

| 序号 |   项目   |                             说明                             |                           关联路径                           |
| :--: | :------: | :----------------------------------------------------------: | :----------------------------------------------------------: |
|  1   |  单位名  |                        显示单位的名称                        | Pixel_Ver`/`Unit_Attributes`/`Unit_Attributes`/`Base_Attributes`/`Text_static`/`Unit_Name |
|  2   |  生命值  |               显示生命值的名称/进度条/数值变化               | Pixel_Ver`/`Unit_Attributes`/`Unit_Attributes`/`Base_Attributes`/`Text_Dynamic`/`Text_dynamic_health |
|  3   |  法力值  |               显示法力值的名称/进度条/数值变化               | Pixel_Ver`/`Unit_Attributes`/`Unit_Attributes`/`Base_Attributes`/`Text_Dynamic`/`Text_dynamic_mage |
|  4   |  护盾值  |                     显示护盾值的数值变化                     | Pixel_Ver`/`Unit_Attributes`/`Unit_Attributes`/`Base_Attributes`/`Text_Dynamic`/`Text_dynamic_shield |
|  5   |   buff   |          显示该单位的现有BUFF。1行最多4个，最多3行           | Pixel_Ver`/`Unit_Attributes`/`Unit_Attributes`/`Base_Attributes`/`Buff |
|  6   | 英雄单位 |               非战斗阶段使用单位idle(待机)状态               |            Pixel_Ver`/`New_Unit_Left`/`Unit_image            |
|  7   | 敌方单位 | 非战斗阶段使用单位idle(待机)状态。目前已做水平对称处理，程序可根据需要自行用代码进行处理 |           Pixel_Ver`/`New_Unit_Right`/`Unit_image            |

- #### 在（6）和（7）的路径New_Unit_Left/New_Unit_Right中，还包含了**Unit_Stop_Image**，这个元素使用的精灵都是经过剪裁，在高度上贴近图片单位的实际高度，可用于确认特效点，之前文档中的上中下3个位置点就按静态图的图片中点、底边中点、顶部中点上方。

![image-20210826161820014](https://i.loli.net/2021/08/26/t243LwHk5ZIAuX7.png)



### 4.3 战利品 - 数据关联7.4 <div id="43">

#### 4.3.0  战斗胜利框 Assets/Prefabs/UI/AwardUI

![image-20210826203820428](https://i.loli.net/2021/08/26/cCOGliNYAep59Uw.png)

- 点击【领取】按键后，系统将自动弹出奖励选择框，一项一项奖励自动往下领取，类似火车的操作

#### 4.3.1  奖励选择框 Assets/Prefabs/UI/CardSelectorUI

- ##### 隐藏框体前

![image-20210826172608325](https://i.loli.net/2021/08/26/Ehvf8CDd3XxJMpO.png)

- ##### 隐藏框体后，Prefabs/UI/CardSelectorUI/Btn_Hide/Text_Btn_Hide的【隐藏】2字更改位【取消隐藏】，并如图所示隐藏起除了该按键之外的所有元素

![image-20210826173639708](https://i.loli.net/2021/08/26/fliIDWqRaThtHNz.png)



#### 4.3.2  神器挑选 Assets/Prefabs/UI/Choose_Artifact - 所用参数从神器表中获得

![image-20210826193740771](https://i.loli.net/2021/08/26/MBzgjOF2vbxuahR.png)

| 序号 |     项目     |      说明      |                           关联路径                           |
| :--: | :----------: | :------------: | :----------------------------------------------------------: |
|  1   |   神器图片   | 显示神器的图片 | Assets`/`Prefabs`/`UI`/`Choose_Artifact`/`Artifact_IMG`/`Choose_Artifact_IMG |
|  2   |   神器名称   | 显示神器的名称 |   Assets`/`Prefabs`/`UI`/`Choose_Artifact`/`Artifact_Name    |
|  3   | 神器效果说明 | 显示神器的效果 | Assets`/`Prefabs`/`UI`/`Choose_Artifact`/`Artifact_IMG`/`Artifact_instruction |



#### 4.3.3  技能挑选 Assets/Prefabs/UI/Choose_Skill - 所用参数从技能表中获得

![image-20210826194501369](https://i.loli.net/2021/08/26/2S4iYh1JAm5FQrC.png)

| 序号 |     项目     |            说明            |                           关联路径                           |
| :--: | :----------: | :------------------------: | :----------------------------------------------------------: |
|  1   |   技能图片   |       显示技能的图片       | Assets`/`Prefabs`/`UI`/`Choose_Skill`/`Skill_IMG`/`Choose_Skill_IMG |
|  2   |   技能名称   |       显示技能的名称       |      Assets`/`Prefabs`/`UI`/`Choose_Skill`/`Skill_Name       |
|  3   |   职业名称   | 显示该技能是属于哪个职业的 |    Assets`/`Prefabs`/`UI`/`Choose_Skill`/`Profession_Name    |
|  4   | 技能效果说明 |     显示技能的效果说明     |   Assets`/`Prefabs`/`UI`/`Choose_Skill`/`Skill_instruction   |



#### 4.3.4  技能挑选 Assets/Prefabs/UI/Associate

![image-20210826195253559](https://i.loli.net/2021/08/26/6au7zGENXk8g1Yd.png)

| 序号 |     项目     |                             说明                             |                        关联路径                         |
| :--: | :----------: | :----------------------------------------------------------: | :-----------------------------------------------------: |
|  1   |   属性提升   |                  显示所增强的属性及其变化值                  | Assets`/`Prefabs`/`UI`/`Associate`/`Attributes_Increase |
|  2   |   属性降低   |                  显示所降低的属性及其变化值                  |  Assets`/`Prefabs`/`UI`/`Associate`/`Attributes_Reduce  |
|  3   | 获得的新技能 |  显示将会获得的新技能。包含技能名称、技能图标、技能效果展示  |      Assets`/`Prefabs`/`UI`/`Associate`/`Skill_New      |
|  4   |  遗忘的技能  | 显示将会被遗忘的已有技能。包含技能名称、技能图标、技能效果展示 |    Assets`/`Prefabs`/`UI`/`Associate`/`Skill_Forget     |
|  5   |   职业名称   |                      此次转生的单位名称                      |   Assets`/`Prefabs`/`UI`/`Associate`/`Occupation_Name   |

