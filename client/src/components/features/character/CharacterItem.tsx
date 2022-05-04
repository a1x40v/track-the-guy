import { Character } from '../../../app/models/Character';

interface Props {
  character: Character;
}

export const CharacterItem: React.FC<Props> = ({ character }) => {
  return (
    <div style={{ minHeight: '300px', border: '1px solid black' }}>
      Nickname: {character.nickname} Race: {character.race}
    </div>
  );
};
