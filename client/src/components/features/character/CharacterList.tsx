import { Character } from '../../../app/models/Character';
import { CharacterItem } from './CharacterItem';

type Props = {
  items: Character[];
};

const CharacterList: React.FC<Props> = ({ items }) => {
  return (
    <ul>
      {items.map((char) => (
        <li key={char.id}>
          <CharacterItem character={char} />
        </li>
      ))}
    </ul>
  );
};

export default CharacterList;
