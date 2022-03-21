import { ErrorMessage, Field, Form, Formik } from 'formik';
import * as Yup from 'yup';

import { useAppDispatch, useAppSelector } from '../../../app/hooks/stateHooks';
import {
  transformUserRequest,
  useRegisterMutation,
} from '../../../app/apiServices/authService';
import {
  selectCurrentUser,
  setCredentials,
} from '../../../app/state/slices/authSlice';
import localStorageService from '../../../app/services/localStorageService';

interface FormValues {
  email: string;
  password: string;
  username: string;
}

const validationSchema = Yup.object({
  email: Yup.string().email('Invalid email format').required(),
  password: Yup.string().required(),
  username: Yup.string().required(),
});

const initialValues: FormValues = {
  email: '',
  password: '',
  username: '',
};

const RegisterForm = () => {
  const dispatch = useAppDispatch();
  const [register, { isLoading }] = useRegisterMutation();

  const handleSubmit = async (values: FormValues) => {
    try {
      const userRequest = await register(values).unwrap();
      localStorageService.setAuthToken(userRequest.token);
      dispatch(
        setCredentials({
          token: userRequest.token,
          user: transformUserRequest(userRequest),
        })
      );
    } catch (err) {
      console.log(`ERROR: ${err}`);
    }
  };

  const user = useAppSelector(selectCurrentUser);

  return (
    <Formik
      initialValues={initialValues}
      validationSchema={validationSchema}
      onSubmit={handleSubmit}
    >
      {({ isSubmitting, isValid, dirty }) => (
        <Form>
          <p>User: {user ? user.username : 'No user'} </p>

          <div>
            <label htmlFor="register-email">Email:</label>
            <Field type="text" id="register-email" name="email" />
            <ErrorMessage name="email" />
          </div>

          <div>
            <label htmlFor="register-password">Password:</label>
            <Field type="password" id="register-password" name="password" />
            <ErrorMessage name="password" />
          </div>

          <div>
            <label htmlFor="register-username">Username:</label>
            <Field type="text" id="register-username" name="username" />
            <ErrorMessage name="username" />
          </div>

          <button type="submit" disabled={isSubmitting || !isValid || !dirty}>
            Submit
          </button>
        </Form>
      )}
    </Formik>
  );
};

export default RegisterForm;
