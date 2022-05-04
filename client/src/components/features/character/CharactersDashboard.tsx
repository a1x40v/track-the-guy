import { useState } from 'react';
import InfiniteScroll from 'react-infinite-scroller';

import { characterApi } from '../../../app/apiServices/characterService';
import { useAppDispatch } from '../../../app/hooks/stateHooks';
import { Character } from '../../../app/models/Character';
import CharacterList from './CharacterList';

export const CharactersDashboard: React.FC = () => {
  const dispatch = useAppDispatch();
  const [totalPages, setTotalPages] = useState<number | null>(null);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [characters, setCharacters] = useState<Character[]>([]);

  const handleGetNext = async (page: number) => {
    const result = await dispatch(
      characterApi.endpoints.getCharacters.initiate(page)
    );

    if (!result.data) return;

    if (totalPages === null) {
      setTotalPages(result.data!.pagination.totalPages);
    }

    setCharacters((chrs) => [...chrs, ...result.data!.characters]);
    setCurrentPage(page);
  };

  return (
    <InfiniteScroll
      initialLoad={true}
      hasMore={totalPages === null || currentPage < totalPages - 1}
      loadMore={handleGetNext}
    >
      <CharacterList items={characters} />
    </InfiniteScroll>
  );
};
