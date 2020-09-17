import React, { useContext } from 'react';
import { Route, Redirect } from "react-router-dom";
import { store } from '../store';

function PrivateRoute({ component: Component, ...rest }) {
	const globalState = useContext(store);

	return <Route {...rest} render={(props) => (
		globalState.state.usuario
			? <Component {...props} />
			: <Redirect to='/' />
	)} />;
}

export default PrivateRoute;