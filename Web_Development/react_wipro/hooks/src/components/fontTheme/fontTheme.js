import React, {Component} from 'react';
import './fontTheme.css'
// import { connect } from "react-redux";
// import { bindActionCreators } from "redux";
// import * as fontThemeActions from "../../store/fontTheme/actions";
export default class fontTheme extends Component {
    // constructor(props) {
    //     super(props);
    //     this.state = {};
    // }
    render() {
      return <div className="component-font-theme">Hello! component fontTheme</div>;
    }
  }
// export default connect(
//     ({ fontTheme }) => ({ ...fontTheme }),
//     dispatch => bindActionCreators({ ...fontThemeActions }, dispatch)
//   )( fontTheme );