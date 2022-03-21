import { useAppDispatch, useAppSelector } from '../../../app/hooks/stateHooks';
import {
  transformUserRequest,
  useLoginMutation,
} from '../../../app/apiServices/authService';
import {
  selectCurrentUser,
  setCredentials,
} from '../../../app/state/slices/authSlice';

const AuthTest = () => {
  const dispatch = useAppDispatch();
  const user = useAppSelector(selectCurrentUser);
  const [login, { isLoading }] = useLoginMutation();

  const handleLogin = async () => {
    try {
      const userRequest = await login({
        email: 'new@test.com',
        password: 'Pa$$w0rd',
      }).unwrap();
      dispatch(
        setCredentials({
          token: userRequest.token,
          user: transformUserRequest(userRequest),
        })
      );
    } catch (err) {
      console.log(`ERR : ${err}`);
    }
  };

  return (
    <div>
      <h2>AuthTest</h2>
      <p>User: {user ? user.username : 'No user'} </p>
      {isLoading ? <p>Loading...</p> : null}
      <button onClick={handleLogin}>Login</button>
    </div>
  );
};

export default AuthTest;
