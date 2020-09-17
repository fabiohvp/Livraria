import React, { createContext, useReducer } from 'react';

const usuarioJson = localStorage.getItem('usuario');
let usuario = undefined;

if (usuarioJson) {
	usuario = JSON.parse(usuarioJson);
}

const initialState = { usuario };
const store = createContext(initialState);
const { Provider } = store;

const StateProvider = ({ children }) => {
	const [state, dispatch] = useReducer((state, action) => {
		switch (action.type) {
			case 'login':
				const loggedInState = { ...state, usuario: action.payload };
				localStorage.setItem('usuario', JSON.stringify(action.payload));
				return loggedInState;
			case 'logout':
				const loggedOutState = { ...state, usuario: undefined };
				localStorage.removeItem('usuario');
				return loggedOutState;
			default:
				throw new Error();
		};
	}, initialState);

	return <Provider value={{ state, dispatch }}>{children}</Provider>;
};

export { store, StateProvider }
