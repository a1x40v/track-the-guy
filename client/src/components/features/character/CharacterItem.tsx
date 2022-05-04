import { Character, CharacterRace } from '../../../app/models/Character';

interface Props {
  character: Character;
}

export const CharacterItem: React.FC<Props> = ({ character }) => {
  const { fraction, ownFractionRating, enemyFractionRating } = character;
  const race = CharacterRace[character.race].toLowerCase();
  const imgSrc = `/assets/images/races/${race}-male.webp`;

  return (
    <div className="flex items-center m-4">
      <img src={imgSrc} className="w-5 h-5 mr-2" />
      <span className="mr-2">{character.nickname}</span>
      <span className="mr-2">ownRating: {ownFractionRating}</span>
      <span className="mr-2">enemyRating: {enemyFractionRating}</span>
    </div>
  );
};
