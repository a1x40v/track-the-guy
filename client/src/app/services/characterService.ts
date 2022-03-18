import { Character } from '../models/Character';
import { baseApi, CacheTagType } from './baseService';

const characterApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    getCharacters: builder.query<Character[], void>({
      query: () => 'characters',
      providesTags: (result) =>
        result
          ? [
              ...result.map(
                ({ id }) => ({ id, type: CacheTagType.Characters } as const)
              ),
              { type: CacheTagType.Characters, id: 'LIST' },
            ]
          : [{ type: CacheTagType.Characters, id: 'LIST' }],
    }),
  }),
});

export const { useGetCharactersQuery } = characterApi;
