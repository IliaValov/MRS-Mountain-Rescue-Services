import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import { MapContainer } from './components/components/map/MapContainer';
import { Login } from './components/components/user/Login';

export default () => (
  <Layout>
    <Route exact path='/' component={Home} />
    <Route exact path='/map' component={MapContainer} />
    <Route exact path='/login' component={Login} />
  </Layout>
);
