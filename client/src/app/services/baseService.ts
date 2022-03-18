import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

export enum CacheTagType {
  Characters = 'Characters',
}

export const baseApi = createApi({
  reducerPath: 'applicationApi',
  tagTypes: [CacheTagType.Characters],
  baseQuery: fetchBaseQuery({ baseUrl: 'http://localhost:5077/api' }),
  endpoints: () => ({}),
});
