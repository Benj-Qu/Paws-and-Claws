Players & flags & blocks z position is -1

Player:
1. gold_spike_scene 里有gameobject player_1 and player_2
2. player must have tag "Player"
3. Player must have box collider and rigidbody

Block:
1. Block switch: 选中某个block时要call blockController的SelectBlock(i)才能移动该block
2. Block selection: 玩家抢到某个block时要call blockController的Player1GetBlock/Player2GetBlock来mark
3. 每个block有自己的public block_id需要手动输入
3. block must have box collider and rigidbody

All Blocks:
1. When implementing blocks: Add block image(sprite) to Resources/Sprites folder, and add block_id, sprite name pair into the dict reside in script AllCards.cs.

TODO:
1. Revise the border condition of block position according to different size of the block
2. Combine two scene
3. Amount of each block displayed and applied when player are placing blocks.
4. Instantiate a new block when one block is set if this block amount > 0.
5. Add image of each block (Like Clash Royale).
6. Add block selection animation: block selected fly to the inventory bar.

Bugs:
1. Should blocks from the two players collide with each other when they are not set (not put down on the map yet)? not sure.
