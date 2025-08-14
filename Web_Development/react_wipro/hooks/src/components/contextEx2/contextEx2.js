import React, {Component} from 'react';
import './contextEx2.css'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as contextEx2Actions from "../../store/contextEx2/actions";
export default class contextEx2 extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-context-ex2">Hello! component contextEx2</div>;
    }
  }
// export default connect(
//     ({ contextEx2 }) => ({ ...contextEx2 }),
//     dispatch => bindActionCreators({ ...contextEx2Actions }, dispatch)
//   )( contextEx2 );