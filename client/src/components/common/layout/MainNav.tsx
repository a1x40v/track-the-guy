import { NavLink } from 'react-router-dom';

type ClassNameFn = (props: { isActive: boolean }) => string;

type NavPath = {
  to: string;
  label: string;
  className?: ClassNameFn;
};

const classNameFn: ClassNameFn = ({ isActive }) =>
  isActive ? 'underline' : '';

export const MainNav = () => {
  const paths: NavPath[] = [
    { to: '/', label: 'Home', className: classNameFn },
    { to: '/characters', label: 'Characters', className: classNameFn },
  ];

  return (
    <nav className="flex justify-between py-4 text-xl">
      <ul className="flex">
        {paths.map(({ to, label, className }) => (
          <li className="mr-2" key={to}>
            <NavLink to={to} className={className}>
              {label}
            </NavLink>
          </li>
        ))}
      </ul>
      <ul className="flex">
        <li>
          <NavLink to="/register">Register</NavLink>
        </li>
        <li>
          <NavLink to="/login">Login</NavLink>
        </li>
      </ul>
    </nav>
  );
};
