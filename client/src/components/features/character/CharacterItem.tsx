import { Character } from '../../../app/models/Character';

interface Props {
  character: Character;
}

export const CharacterItem = ({ character }: Props) => {
  return (
    <div>
      Nickname: {character.nickname} Race: {character.race}
    </div>
  );
};
