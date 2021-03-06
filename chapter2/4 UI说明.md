# 4 UI说明

### 4.1 新版分区

#### （0）目录

| 编号  |            名称            |          跳转           |
| :---: | :------------------------: | :---------------------: |
| 4.1.1 | 预制体 /battle/unit.prefab | [<div>点击</div>](#411) |
| 4.1.2 |    各预制体的sort layer    | [<div>点击</div>](#412) |

#### （1）4.1.1 预制体 /battle/unit.prefab<div id="411">

[![RNEZf1.png](https://z3.ax1x.com/2021/06/28/RNEZf1.png)](https://imgtu.com/i/RNEZf1)

|   结构树   |                           要点说明                           |
| :--------: | :----------------------------------------------------------: |
| unit_image | 图片的锚点被我定到了bottom，坐标也采取00，unit_image有值时要把三条给向上撑开 |
|   health   | 生命条，最大生命值1000为目前长度，每少100点最大生命值，生命条缩短5% |
|    mana    | 法力条，法力值上限100为目前长度，每少10点蓝上限，法力条缩短5% |
|   speed    |     攻速条，攻速条上限4为目前长度，每少1，攻速条缩短10%      |
|   buff区   | 目前buff区内只有一个示例buff，当有两个以上的buff时，buff横向延申并居中显示，<br />每一行最多8个buff，当多于8个时，往上多加一行，以此类推。<br />感觉buff显示数字还是太不明显了，如果可以的话，看有没办法弄那种跟时钟一样的倒计时 |
|   shadow   | 阴影的锚点也被定到bottom，英雄的影子是统一的，怪物的影子跟随模型 |



#### （2）4.1.2 各预制体的sort layer<div id="412">

|              预制体               |                     object                      | sorting layer | order in layer |
| :-------------------------------: | :---------------------------------------------: | :-----------: | :------------: |
| /Battle/Background/SunshineForest |                     Layer2                      |  Background   |       5        |
| /Battle/Background/SunshineForest |                     Layer1                      |  Background   |       1        |
| /Battle/Background/SunshineForest |                     Layer0                      |  Background   |       0        |
|        /Battle/Battle_Seat        |              /Hero_Seat/所有shadow              |  Background   |       1        |
|        /Battle/Battle_Seat        |    /Hero_Seat/Hero_001 & 002/unit/unit_image    |  Background   |       2        |
|        /Battle/Battle_Seat        |    /Hero_Seat/Hero_003 & 004/unit/unit_image    |  Background   |       3        |
|        /Battle/Battle_Seat        |    /Hero_Seat/Hero_005 & 006/unit/unit_image    |  Background   |       4        |
|        /Battle/Battle_Seat        |            /Monster_Seat/所有shadow             |  Background   |       1        |
|        /Battle/Battle_Seat        | /Monster_Seat/Monster_001 & 002/unit/unit_image |  Background   |       2        |
|        /Battle/Battle_Seat        | /Monster_Seat/Monster_003 & 004/unit/unit_image |  Background   |       3        |
|        /Battle/Battle_Seat        | /Monster_Seat/Monster_005 & 006/unit/unit_image |  Background   |       4        |













### 4.2 旧版分区，方便查看旧控件

![image-20210611162233732](https://i.loli.net/2021/06/11/AfWZJVDGhFPBwLc.png)

#### （1）关卡标题

|   字段   |                           显示内容                           |
| :------: | :----------------------------------------------------------: |
|  关卡数  |                   第X关，其中X为此时关卡数                   |
| 阶段状态 | 分为【战斗阶段】和【战备阶段】两个阶段。<br />在【战备阶段】点击开战后进入【战斗阶段】。<br />【战斗阶段】中战斗胜利后，转换到【战备阶段】 |

#### （2）神器展示区

- 神器卡打出后将在此处显示。
- 长按展示区的神器将显示神器的说明，说明框的左上角对着目标神器的中心

[![2hsMjK.png](https://z3.ax1x.com/2021/06/11/2hsMjK.png)](https://imgtu.com/i/2hsMjK)

- 显示靠右几个神器的说明时，说明框的右上角对着目标神器中心

[![2hrrf1.png](https://z3.ax1x.com/2021/06/11/2hrrf1.png)](https://imgtu.com/i/2hrrf1)

#### （3）速度调节按键

- 目前开放速度1（原速），速度2（二倍速）两种战斗速度
- 此按键无论是在战备阶段或是战斗阶段都可以点击，影响各种战斗速度

#### （4）开战/暂停按键

- 在战备阶段，此处显示**“开战”**，玩家做好战斗准备后点击该按键，即可触发战斗
- 在战备阶段，此处显示**“暂停”**，点击该按键后，战斗将停滞，按键显示为**“继续”**，再次点击后，战斗继续

#### （5）设置集合按键

- 在任何阶段点击此按键将展开所有设置按键，目前先采取最土的办法，直接从右往左弹出面板

[![2h6X7D.png](https://z3.ax1x.com/2021/06/11/2h6X7D.png)](https://imgtu.com/i/2h6X7D)

- 展开面板时，再次碰触屏幕的非面板区域，面板将会从左往右收回。如果点击的是面板上的相应按键，将会按照点击的按键触发提示或弹框，并保持面板展开状态直到点击非面板区域。
- 点击【重新开始】，弹出提示“确定失去所有游戏进度，从头开始游戏吗？”，并给出【确认】【取消】两个选项
- 点击【保存退出】，弹出提示“是否要退出游戏，您的游戏记录将被保存并上传云端”，并给出【确认】【取消】两个选项
- 点击【游戏帮助】，弹出关于游戏内容的说明文本弹框
- 点击【系统设置】，调用之前做的那个系统设置

#### （6）敌方卡组区

- 怪物死亡时，卡面将渐渐淡化，有一个渐进消失的过程
- 怪物理想进场效果是返回战备阶段时，从空中砸向卡槽，造成战场震动，攻击越高的怪造成的震动就越大；目前局限技术力就直接淡入进场吧，从无到有慢慢显示一张卡牌

#### （7）我方卡组区

- 英雄死亡时，将停止一切动画，并蒙上一层灰色遮罩；召唤物死亡时，卡面将渐渐淡化，有一个渐进消失的过程
- 英雄卡从手牌进入战场时，需要有进场动画，应该不会有移动的，搞个光效和bgm啥的，跟炉石橙卡进场一样

#### （8）手牌区

- 手牌的打出方式参考**2.0.4 卡牌出牌类型表**
- 获得新的卡牌时，无论是战斗中还是战备时获得，都是从右侧向左平移进入，补充在手牌队列的末尾，然后整个手牌队列做一次对手牌区的居中
- 当手牌数量超过N张时，卡牌将会堆叠显示，左侧在上，堆叠的罩盖程度根据卡牌的数量决定，N待测算最大承载量，测试阶段可按十张算
- 手牌区的卡牌被触摸选中或者指针移动到卡面时，边框发光，并放大卡面1.3倍，如果有堆叠卡牌的情况，该选中卡牌将处于上层。





[![2hgWi4.png](https://z3.ax1x.com/2021/06/11/2hgWi4.png)](https://imgtu.com/i/2hgWi4)

### 卡牌说明（卡牌说明现在改成长按卡面2秒后触发全屏遮罩，内容显示在中间，松手后遮罩消失。电脑版的逻辑是停放卡面显示，鼠标移出卡面就消失）

##### （1）共有 - 卡牌展示

| 卡牌位置 |                           显示效果                           |
| :------: | :----------------------------------------------------------: |
|  战斗区  | 原始的带技能说明的单位卡，如果被选中的是敌方战区的卡牌，也显示绿颜色的原始单位卡框 |
|  手牌区  |                           原样显示                           |
|  遗物栏  |                    显示选中遗物的原始卡牌                    |

##### （2）共有 - 卡牌基础信息

| 卡牌类型 |                           显示效果                           |
| :------: | :----------------------------------------------------------: |
|  单位卡  | “[(随从/英雄/装备)卡 `|` XX职业 `|` 稀有度]“ + ”单位介绍“ + ”主动技能介绍“ + ”被动技能介绍“ |
|  事件卡  |                  ”[ 事件卡 ]“ + ”事件说明“                   |
|  神器卡  |  ”[ 神器卡 \| (普通;BOSS) ]“ + ”神器介绍“ + ”神器效果说明“   |
|  强化卡  |           ”[ 强化卡 `|`稀有度 ]“ + ”强化效果说明“            |

##### （3）单位卡专用 - 单位属性

##### （4）单位卡专用 - 强化效果：当单位卡被强化过后，此处会添加一个强化属性的信息展示的组件，用于显示强化效果名和强化的效果

##### （5）战斗中的单位卡专用 - BUFF信息：此处未作修改，目前有足够的位置可以将所有buff的图标加说明一起展示出来，不需要再通过点击图标显示信息的方式
