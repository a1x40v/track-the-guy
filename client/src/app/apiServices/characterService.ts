import { GetCharacterListVm } from '../models/Character';
import { baseApi, CacheTagType } from './baseService';

const CHARACTER_PAGE_SIZE = 3;

export const characterApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    getCharacters: builder.query<GetCharacterListVm, number>({
      query: (pageNumber: number) => ({
        url: `characters`,
        params: {
          pageNumber,
          pageSize: CHARACTER_PAGE_SIZE,
        },
      }),
      providesTags: (result) =>
        result
          ? [
              ...result.characters.map(
                ({ id }) => ({ id, type: CacheTagType.Characters } as const)
              ),
              { type: CacheTagType.Characters, id: 'PARTIAL-LIST' },
            ]
          : [{ type: CacheTagType.Characters, id: 'PARTIAL-LIST' }],
    }),
  }),
});

export const { useGetCharactersQuery } = characterApi;
