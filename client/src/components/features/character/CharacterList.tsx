import { Character } from '../../../app/models/Character';
import { CharacterItem } from './CharacterItem';

type Props = {
  items: Character[];
};

const CharacterList: React.FC<Props> = ({ items }) => {
  return (
    <ul className="flex flex-col">
      {items.map((char) => (
        <li key={char.id} className="border-b-2 border-black">
          <CharacterItem character={char} />
        </li>
      ))}
    </ul>
  );
};

export default CharacterList;
