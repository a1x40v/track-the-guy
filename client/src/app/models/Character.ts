export enum CharacterFraction {
  Horde = 0,
  Alliance = 1,
}

export enum CharacterRace {
  Orc = 0,
  Troll = 1,
  Undead = 2,
  Tauren = 3,
  BloodElf = 4,
  Human = 5,
  Dwarf = 6,
  Gnome = 7,
  NightElf = 8,
  Draenei = 9,
}

export interface Character {
  id: string;
  nickname: string;
  ownFractionRating: number;
  enemyFractionRating: number;
  race: CharacterRace;
  fraction: CharacterFraction;
  creatorName: string;
}

export interface GetCharacterListVm {
  characters: Character[];
}
