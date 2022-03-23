import { GetCharacterListVm } from '../models/Character';
import { baseApi, CacheTagType } from './baseService';

const characterApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    getCharacters: builder.query<GetCharacterListVm, void>({
      query: () => 'characters',
      providesTags: (result) =>
        result
          ? [
              ...result.characters.map(
                ({ id }) => ({ id, type: CacheTagType.Characters } as const)
              ),
              { type: CacheTagType.Characters, id: 'LIST' },
            ]
          : [{ type: CacheTagType.Characters, id: 'LIST' }],
    }),
  }),
});

export const { useGetCharactersQuery } = characterApi;
