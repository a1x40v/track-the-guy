const setAuthToken = (token: string) => {
  localStorage.setItem('jwt', token);
};

const getAuthToken = () => {
  return localStorage.getItem('jwt');
};

const localStorageService = {
  setAuthToken,
  getAuthToken,
};

export default localStorageService;
