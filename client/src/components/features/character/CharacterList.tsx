import { useGetCharactersQuery } from '../../../app/apiServices/characterService';
import { CharacterItem } from './CharacterItem';

export const CharacterList = () => {
  const { data, error, isLoading } = useGetCharactersQuery();

  if (error) return <div>ERROR</div>;

  if (isLoading) return <div>Loading...</div>;

  if (data)
    return (
      <ul>
        {data.characters.map((char) => (
          <li key={char.id}>
            <CharacterItem character={char} />
          </li>
        ))}
      </ul>
    );

  return <div>CharacterList</div>;
};
