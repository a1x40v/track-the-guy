import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { AppUser } from '../../models/AppUser';
import { RootState } from '../store';

type AuthState = {
  user: AppUser | null;
  token: string | null;
};

const initialState: AuthState = {
  user: null,
  token: null,
};

const slice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    setCredentials: (
      state,
      {
        payload: { user, token },
      }: PayloadAction<{ user: AppUser; token: string }>
    ) => {
      state.user = user;
      state.token = token;
    },
  },
});

export const { setCredentials } = slice.actions;

export default slice.reducer;

export const selectCurrentUser = (state: RootState) => state.auth.user;
